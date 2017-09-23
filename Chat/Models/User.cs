using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string Message { get; set; }

    }
}
