using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreBlog.Models;

using PagedList.Mvc;

namespace TheatreBlog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(int? page)
        {
            var posts = db.Posts.Include("Comments").ToList().ToPagedList(page?? 1,3);
            return View(posts);
        }

        [HttpPost, ActionName("index")]
        public ActionResult Index(Comment com)
        {

            com.Author = User.Identity.Name;
            com.CommentDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                //update database
                db.Comments.Add(com);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Where(p => p.PostID == id)
                                .Include("Comments")
                                .FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Title,PostBody,Author,PublishDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Where(p => p.PostID == id)
                                 .Include("Comments")
                                 .FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult Edit([Bind(Include = "PostID,Title,PostBody,Author,PublishDate")] Post post)
        {
            post.Author = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteComment")]
        public ActionResult DeleteComment(Comment cmt)
        {
            
                var cmtToDelete = db.Comments.Find(cmt.CommentID);
                var postId = cmt.PostID;
                db.Comments.Remove(cmtToDelete);
                db.SaveChanges();
                return RedirectToAction("Edit", "Posts", new { @id = cmtToDelete.PostID});
           
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Where(p => p.PostID == id)
                                .Include("Comments")
                                .FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Where(p => p.PostID == id)
                                 .Include("Comments")
                                 .FirstOrDefault();
            db.Posts.Remove(post);
            db.SaveChanges();
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
