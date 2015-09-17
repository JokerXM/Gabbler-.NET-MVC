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
    public class GabController : Controller
    {
        private GabblerDBContext db = new GabblerDBContext();


        public ActionResult Profile()
        {
            List<Gab> mygabs = new List<Gab>();
            int uid = Convert.ToInt32(Session["ID"]);
            var mygab = from e in db.Gab where (e.UserID == uid) select e;

            foreach (Gab g in mygab)
            {
                mygabs.Add(g);
            }
            var timelistgabs = (from g in mygabs select g).OrderByDescending(g => g.Date);
            return View(timelistgabs);
        }
        
        public ActionResult SeeProfile(int ?id)
        {
            //follow.UFollowID = Convert.ToInt32(ufollow);
            int uid = Convert.ToInt32(id);
            //int uid = 2;
            List<Gab> mygabs = new List<Gab>();
            var mygab = from e in db.Gab where (e.UserID == uid) select e;
            User he = db.User.Find(uid);
            ViewBag.username = he.Username;
            ViewBag.avatar = he.Avatar.APath;
            ViewBag.background = he.Background.BPath;


            foreach (Gab g in mygab)
            {
                mygabs.Add(g);
            }
            var timelistgabs = (from g in mygabs select g).OrderByDescending(g => g.Date);
            return View(timelistgabs);
        }

        public ActionResult MainPage()
        {
            int uid = Convert.ToInt32(Session["ID"]);
            List<Gab> mygabs = new List<Gab>();
            var follows = from f in db.Follow where (f.UserID == uid) select f;
            var mygab1 = from e in db.Gab where (e.UserID == uid) select e;

            foreach (Gab g in mygab1)
            {
                mygabs.Add(g);
            }
            foreach(Follow follow in follows)
            {
                var mygab2 = from e in db.Gab where ((e.UserID == follow.UFollowID )) select e;

                foreach (Gab g in mygab2)
                {
                    mygabs.Add(g);
                }
                

            }
            var timelistgabs = (from g in mygabs select g).OrderByDescending(g=>g.Date);
            return View(timelistgabs);
            
        }

        public ActionResult Like(int ?id)
        {
            User u = new User();
            int userid = Convert.ToInt32(Session["ID"]);
            u = db.User.Find(userid);
            Gab gab = db.Gab.Find(id);
            gab.likenumber++;
            gab.LikeUsers.Add(u);
            db.SaveChanges();
            return RedirectToAction("MainPage");
        }

        public ActionResult Unlike(int? id)
        {
            User u = new User();
            int userid = Convert.ToInt32(Session["ID"]);
            u = db.User.Find(userid);
            Gab gab = db.Gab.Find(id);
            gab.likenumber--;
            gab.LikeUsers.Remove(u);
            db.SaveChanges();
            return RedirectToAction("MainPage");
        }
        //
        // GET: /Gab/

        public ActionResult Index()
        {
            return View(db.Gab.ToList());
        }

        //
        // GET: /Gab/Details/5

        public ActionResult Details(int id = 0)
        {
            Gab gab = db.Gab.Find(id);
            if (gab == null)
            {
                return HttpNotFound();
            }
            return View(gab);
        }

        //
        // GET: /Gab/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Gab/Create

        [HttpPost]
        public ActionResult Create(object sender, EventArgs e)
        {
                Gab gab = new Gab();
                string content = Request.Form["content"];               
                gab.Date = DateTime.Now;
                gab.UserID = Convert.ToInt32(Session["ID"]);
                gab.User = db.User.Find(gab.UserID);
                gab.content = content;
                db.Gab.Add(gab);
                db.SaveChanges();
                return RedirectToAction("MainPage");
        }

        //
        // GET: /Gab/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Gab gab = db.Gab.Find(id);
            if (gab == null)
            {
                return HttpNotFound();
            }
            return View(gab);
        }

        //
        // POST: /Gab/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gab gab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gab);
        }

        //
        // GET: /Gab/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Gab gab = db.Gab.Find(id);
            if (gab == null)
            {
                return HttpNotFound();
            }
            return View(gab);
        }

        //
        // POST: /Gab/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gab gab = db.Gab.Find(id);
            if (gab!=null)
            {
                db.Gab.Remove(gab);
                db.SaveChanges();
            }

            return RedirectToAction("Profile");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}