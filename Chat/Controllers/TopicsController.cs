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
    public class TopicsController : Controller
    {
        private ChatDBEntities2 db = new ChatDBEntities2();

        // GET: Topics
        public async Task<ActionResult> Index()

        {
            List<Topics> lst = await db.Topics.OrderByDescending(x => x.countLikes).ToListAsync();
           
            return View(lst);
        }

        // GET: Topics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topics topics = await db.Topics.FindAsync(id);
            if (topics == null)
            {
                return HttpNotFound();
            }
            return View(topics);
        }

        // GET: Topics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,title")] Topics topics)
        {
            if (ModelState.IsValid)
            {
                if (topics.title is null)
                {
                    ViewBag.Error = "Topics can not be empty";
                    return View(topics);
                }
                else if (topics.title.Count() < 20)
                {
                    ViewBag.Error = "Topics has to be at least 20 symbols lenght";
                    return View(topics);
                }
                topics.countLikes = 0;
                db.Topics.Add(topics);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(topics);
        }

        // GET: Topics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topics topics = await db.Topics.FindAsync(id);
            if (topics == null)
            {
                return HttpNotFound();
            }
            return View(topics);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,title,imageone,imagetwo")] Topics topics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topics).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topics);
        }

        // GET: Topics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topics topics = await db.Topics.FindAsync(id);
            if (topics == null)
            {
                return HttpNotFound();
            }
            return View(topics);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Topics topics = await db.Topics.FindAsync(id);
            db.Topics.Remove(topics);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
