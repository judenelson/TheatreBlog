using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreBlog.Models;
using TheatreBlog.ViewModels;

namespace TheatreBlog.Controllers
{
    public class UserViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserViewModels
        public ActionResult Index()
        {
            List<UserViewModel> theUsers= new List<UserViewModel>();
            foreach (ApplicationUser usr in db.Users)
            {
                UserViewModel u = new UserViewModel();
                u.Address = usr.Address;
                u.Email = usr.Email;
                u.IsAdmin = usr.IsAdmin;
                u.IsSuspended = usr.IsSuspended;
                u.UserName = usr.UserName;
                u.Id = usr.Id;

                theUsers.Add(u);


            }

            return View(theUsers);
        }

        // GET: UserViewModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var au = db.Users.Find(id);
            UserViewModel usr = new UserViewModel {Id =au.Id,IsAdmin =au.IsAdmin, IsSuspended=au.IsSuspended,FullName=au.FullName,Address=au.Address};
         
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // GET: UserViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsAdmin,Address,FullName,IsSuspended,Email,UserName")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: UserViewModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var au = db.Users.Find(id);
            UserViewModel userViewModel = new UserViewModel { Id = au.Id, IsAdmin = au.IsAdmin, IsSuspended = au.IsSuspended, FullName = au.FullName, Address = au.Address };

            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(au);
        }

        // POST: UserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsAdmin,Address,FullName,IsSuspended,Email,UserName")] UserViewModel uvm)
        {
             if (ModelState.IsValid)
            {
              ApplicationUser au = db.Users.Find(uvm.Id);
                au.FullName = uvm.FullName;
                au.Address = uvm.Address;
                au.Email = uvm.Email;
                au.UserName = uvm.UserName;
                au.IsAdmin = uvm.IsAdmin;
                au.IsSuspended = uvm.IsSuspended;



                db.Entry(au).State = EntityState.Modified;

                var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));

                //Now add the code in the EditAction Result to add / remove the user to the limited role as requested below the code for the admin role

                if ((au.IsSuspended) && (!UserManager.IsInRole(au.Id, "Restricted")))
                        UserManager.AddToRole(au.Id, "Restricted");
                  else if ((!au.IsSuspended) && (UserManager.IsInRole(au.Id, "Restricted")))
                        UserManager.RemoveFromRoles(au.Id, "Restricted");

                db.SaveChanges();
                return RedirectToAction("Index");

            }
                return View(uvm);
        }

        // GET: UserViewModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser au = db.Users.Find(id);


            if (au == null)
            {
                return HttpNotFound();
            }
            UserViewModel usr = new UserViewModel { Id = au.Id, IsAdmin = au.IsAdmin, IsSuspended = au.IsSuspended, FullName = au.FullName, Address = au.Address };

            return View(usr);
        }

        // POST: UserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser usr = db.Users.Find(id);
            db.Users.Remove(usr);
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
