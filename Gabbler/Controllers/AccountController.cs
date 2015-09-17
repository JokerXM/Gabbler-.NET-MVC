using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gabbler.Models;

namespace Gabbler.Controllers
{
    public class AccountController : Controller
    {
        private GabblerDBContext db = new GabblerDBContext();

        //
        // GET: /Account/


        public ActionResult Chooseavater()
        {
            return View(db.Avatar.ToList());
        }
        public ActionResult AApply(int ?id)
        {
            int uid = Convert.ToInt32(Session["ID"]);
            User u = db.User.Find(uid);
            u.AvatarID = Convert.ToInt32(id);
            u.Avatar = db.Avatar.Find(id);
            Session["avatar"] = u.Avatar.APath;
            db.SaveChanges();
            return RedirectToAction("Profile", "Gab");
        }
        public ActionResult Choosestyle()
        {
            return View(db.Background.ToList());
        }
        public ActionResult BApply(int? id)
        {
            int uid = Convert.ToInt32(Session["ID"]);
            User u = db.User.Find(uid);
            u.BackgroundID = Convert.ToInt32(id);
            u.Background = db.Background.Find(id);
            db.SaveChanges();
            Session["Background"] = u.Background.BPath;
            return RedirectToAction("Profile", "Gab");
        }


        public ActionResult Login(object sender, EventArgs e)
        {

            string username = Request.Form["username"];
            string password = Request.Form["password"];

            var user = from m in db.User
                       select m;
            user = user.Where(s => s.Username.Equals(username));
            foreach (User u in user)
            {
                if (password.Equals(u.Password))
                {
                    Session["username"] = u.Username;
                    Session["ID"] = u.U_ID;
                    Session["Background"] = u.Background.BPath;
                    Session["avatar"] = u.Avatar.APath;

                    return RedirectToAction("MainPage","Gab");
                    

                }

            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
           return  RedirectToAction("Login", "Home");
        }

        //
        // GET: /Account/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Account/Create

        public ActionResult Create()
        {
            ViewBag.AvatarID = new SelectList(db.Avatar, "ID", "APath");
            ViewBag.BackgroundID = new SelectList(db.Background, "ID", "BPath");
            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                Background b = new Background();
                b = db.Background.Find(user.BackgroundID);
                user.Background = b;
                Avatar a = new Avatar();
                a = db.Avatar.Find(user.AvatarID);
                user.Avatar = a;
                db.User.Add(user);
                db.SaveChanges();
                Session["ID"] = user.U_ID;
                Session["username"] = user.Username;
                Session["Background"] = user.Background.BPath;
                Session["avatar"] = user.Avatar.APath;
                return RedirectToAction("MainPage","Gab");
            }
            ViewBag.AvatarID = new SelectList(db.Avatar, "ID", "APath", user.AvatarID);
            ViewBag.BackgroundID = new SelectList(db.Background, "ID", "BPath", user.BackgroundID);
            return View(user);
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AvatarID = new SelectList(db.Avatar, "ID", "APath", user.AvatarID);
            ViewBag.BackgroundID = new SelectList(db.Background, "ID", "BPath", user.BackgroundID);
            return View(user);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MainPage", "Gab");
            
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}