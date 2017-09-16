using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chat.Models;
using System.Web.Mvc;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();

        // Send message
        public void Send(string name, string message)
        {
            if (message.Length>0)
            {
                Clients.All.addMessage(name, message);
                Clients.Others.addHeader(name);
            }
            else
            {

                Clients.Caller.errorEmptyMessage(name,"Error!! Message can not be empty");

            }
           
        }

        // New user
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Send message to current user
                Clients.Caller.onConnected(id, userName, Users);

                // Send message to all users except current
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Disctonect user
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}