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
using System.IO;

namespace Chat.Controllers
{
    public class TopicsController : Controller
    {
        private ChatDBEntities2 db = new ChatDBEntities2();

        // GET: Topics
        public async Task<ActionResult> Index()

        {
            //List<Topics> lst = await db.Topics.OrderByDescending(x => x.countLikes).ToListAsync();
            //List<Topics> lst = await db.Topics.OrderByDescending(x => x.countLikes).AsNoTracking().ToListAsync();
            List<Topics> lst = await db.Topics.OrderByDescending(x => x.id).AsNoTracking().ToListAsync();

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
        public ActionResult Create([Bind(Include = "id,title,imagetwo")] Topics topics, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

          
                if (topics.title is null)
                {
                    ViewBag.Error = "Topic can not be empty";
                    return View(topics);
                }
                else if (topics.title.Count() < 10)
                {
                    ViewBag.Error = "Topic has to be longer than 10 characters";
                    return View(topics);
                }
                topics.countLikes = 0;
                //if (image != null && image.ContentLength > 0)
                //{

                //    if (image.ContentType.ToLower() != "image/jpg" &&
                //      image.ContentType.ToLower() != "image/jpeg" &&
                //       image.ContentType.ToLower() != "image/pjpeg" &&
                //       image.ContentType.ToLower() != "image/gif" &&
                //       image.ContentType.ToLower() != "image/x-png" &&
                //       image.ContentType.ToLower() != "image/png")
                //    {
                //        ViewBag.Error = "File is not a picture.";
                //        return View();
                //    }
                    try
                 {

                    if (image.ContentType.ToLower() != "image/jpg" &&
                      image.ContentType.ToLower() != "image/jpeg" &&
                       image.ContentType.ToLower() != "image/pjpeg" &&
                       image.ContentType.ToLower() != "image/gif" &&
                       image.ContentType.ToLower() != "image/x-png" &&
                       image.ContentType.ToLower() != "image/png")
                    {
                        ViewBag.Error = "You try to upload not image";
                        return View();
                    }
                    else
                    {
                        string path = Path.Combine(Server.MapPath("~/img"),
                                                       Path.GetFileName(image.FileName));
                        image.SaveAs(path);
                        ViewBag.Error = "File uploaded successfully";
                        topics.imageone = path;
                    }
                 }
                    catch (Exception )
                    {
                    //ViewBag.Error = "ERROR:" + ex.Message.ToString();
                    ViewBag.Error = "Image can not be empty";
                    return View();
                    }
                
                //else
                //{
                //    ViewBag.Error = "You have not specified a file.";
                    
                //}

                db.Topics.Add(topics);
                db.SaveChanges();
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
