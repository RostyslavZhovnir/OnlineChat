﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public partial class User
    {
        public string ConnID { get; set; }
        public string UserName { get; set; }
        public string UserGroup { get; set; }
        public string Message { get; set; }

    }
}



