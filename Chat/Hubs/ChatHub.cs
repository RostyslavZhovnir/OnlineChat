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
        static long counter = 0;
        //static string group;

        // Send message
        public void Send(string name, string message, string group)
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

                //    //Clients.All.addMessage(name, message);
                //    //Clients.OthersInGroup(group).addMessage(name, message);
                //    Clients.Caller.clearError();
                Clients.Group(group).addMessage(name, message);
                Clients.Others.addHeader(name);
                var y = new ConversationHistory
                { UserName = name, Message = message, UserGroup = group, ConnID = Context.ConnectionId };
                x.ConversationHistory.Add(y);
                x.SaveChanges();

                //var dog = Users.Where(d => d.UserName == name).FirstOrDefault();
                //if (dog != null) { dog.UserGroup = group; dog.Message = message; }

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

                //Count total Users Online
                counter = counter + 1;
                Clients.All.UpdateCounter(counter);

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

                //Count total Users Online
                counter = counter - 1;
                Clients.All.UpdateCounter(counter);

                Clients.All.onUserDisconnected(id, item.UserName);
            }

            return base.OnDisconnected(stopCalled);
        }

        //UserRomms

        public void JoinGroup(string groupName)
        {

            this.Groups.Add(this.Context.ConnectionId, groupName);
            //Clients.Group(groupName).addMessage(Context.User.Identity.Name + " joined group" + groupName);
            Clients.Group(groupName).addMessage(Context.User.Identity.Name + " joined");
            Clients.Caller.clearError();

            var y = x.ConversationHistory.ToList();
            var z= y.Where(x => x.UserGroup == groupName);
            foreach (var item in z)
            {
                Clients.Group(groupName).addMessage(item.UserName+" : "+item.Message);
            }
            //var dog = Users.Where(d => d.Name == Context.User.Identity.Name).FirstOrDefault();

            //if (dog != null) { dog.GroupName = groupName; }
            //var z = x.ConversationHistory.f;
            //foreach (var item in z)
            //{
            //    Clients.Group(groupName).addMessage();
            //}


        }

        public void LeaveGroup(string groupName)
        {
            Groups.Remove(this.Context.ConnectionId, groupName);
        }
    }
}