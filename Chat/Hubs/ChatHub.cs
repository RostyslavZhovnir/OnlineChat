using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chat.Models;
using System.Web.Mvc;
using Chat.Controllers;
using System.Web.Security;
using System.Threading.Tasks;
namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();
        static List<ConversationHistory> MessageList = new List<ConversationHistory>();
        private ChatDBEntities2 x = new ChatDBEntities2();
        //static int counter = 0;
        static bool alreadyCalled;
        //static string group;
        // Send message
        public void Send(string name, string message, string group, string date)
        {
            //Clients.All.addMessage(name, message);
            Clients.Caller.clearError();
            if (message.Length > 0)
            {
                if (group is null)
                {
                    Clients.Caller.errorEmptyMessage(name, "Error!! First join to some group");
                    return;
                }
                Clients.Group(group).addMessage(name, message);
                Clients.Group(group).addHeader(name);
                var y = new ConversationHistory
                { UserName = name, Message = message, UserGroup = group, ConnID = Context.ConnectionId, Date = date };
                x.ConversationHistory.Add(y);
                x.SaveChanges();
            }
            else
            {
                Clients.Caller.errorEmptyMessage(name, "Error!! Message can not be empty");
            }
        }
        // New user
        public void Connect(LogOnModel model)
        {
            var id = Context.ConnectionId;
            if (!Users.Any(x => x.ConnID == id))
            {
                string userName = Membership.GetUser().UserName;
                Users.Add(new User { ConnID = id, UserName = userName });
                // Send message to current user
                Clients.Caller.onConnected(id, userName, Users);
                // Send message to all users except current
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
        // Disconect user
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnID == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);
            }
            return base.OnDisconnected(stopCalled);
        }
        //UserRomms
        public void JoinGroup(string groupName)
        {
            this.Groups.Add(this.Context.ConnectionId, groupName);
            Clients.Group(groupName).userGroupOnline(Context.User.Identity.Name + " join");
            Clients.Caller.clearError();
            ShowHistory(groupName);
        }
        //Likes
        public  async Task Likes(string groupName)
        {
            string userName = Context.User.Identity.Name.ToString();
            var z = x.Likes.FirstOrDefault(x => x.UserName == userName && x.UserGroup == groupName);
            var totallikes = x.Topics.FirstOrDefault(x => x.title == groupName);


            if (z is null)
            {
               int counter=1;
                
                var newLikes = new Likes()
                { UserName = userName, UserGroup = groupName, count = counter };
                x.Likes.Add(newLikes);
                


                if (totallikes.countLikes ==0)
                {
                    totallikes.countLikes =+1;
                    Clients.All.Addlike(true, groupName);
                }
                else
                {
                    totallikes.countLikes++;
                    Clients.All.Addlike(true, groupName);
                }
               
                await x.SaveChangesAsync();
            }


            else if (z.count==1)
            {
                //alreadyCalled = false;
                z.count = 0;
                totallikes.countLikes --;
                await x.SaveChangesAsync();
                Clients.All.Addlike(false,groupName);
                //alreadyCalled = true;
            }

            else if (z.count==0)
            {
                //alreadyCalled = true;
                z.count =1;
                totallikes.countLikes++;
                await x.SaveChangesAsync();
                Clients.All.Addlike(true,groupName);
                //alreadyCalled = false;
               
            }
        }
        //Show History
        public void ShowHistory(string groupName)
        {
            var y = x.ConversationHistory.ToList();
            var z = y.Where(x => x.UserGroup == groupName);
            foreach (var item in z)
            {
                Clients.Caller.addMessageHistory(item.UserName + " : ", item.Message, item.Date);
            }
        }
        public void LeaveGroup(string groupName)
        {
            Groups.Remove(this.Context.ConnectionId, groupName);
        }
    }
}