using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chat.Models;

namespace Chat.Controllers
{
    public class AdminPageController : Controller
    {
        private ChatDBEntities2 db = new ChatDBEntities2();

        // GET: AdminPage
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
