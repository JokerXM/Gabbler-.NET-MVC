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
    public class FollowController : Controller
    {
        private GabblerDBContext db = new GabblerDBContext();

        //
        // GET: /Follow/

        public ActionResult Index()
        {
            return View(db.Follow.ToList().Where(f => f.UserID == int.Parse(Session["ID"].ToString())));
        }

        //
        // GET: /Follow/Details/5
        public ActionResult Search()
        {

            return View();
        }

        //
        // POST: /AccountU/Login

        [HttpPost]
        public ActionResult Result(object sender, EventArgs e)
        {

            string follow = Request.Form["FollowName"];
            List<User> exist = new List<User>();
            int uid = Convert.ToInt32(Session["ID"]);
            var fexist = from f in db.Follow where f.UserID == uid && f.UFollow.Username == follow select f;
            if (fexist.Count() == 1)
            {
                Session["exist"] = 1;
                Session["existid"] = fexist.First().F_ID;
            }
            else
            {
                Session["exist"] = 0;
            }

            //string name = db.User.Username;
            //var exist = from u in db.User where u.Username == follow select u;
            if (follow == "all")
            {
                var exist1 = from u in db.User select u;
                return View(exist1);
            }
            else
            {
                var exist2 = from u in db.User where (u.Username.Contains(follow)) select u;
                return View(exist2);
            }
           
           


        }

        //
        // GET: /Follow/Details/5

        public ActionResult Details(int id = 0)
        {
            Follow follow = db.Follow.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        //
        // GET: /Follow/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateF(int? ufollow)
        {
            Follow follow = new Follow();

            //follow.UFollowID = Convert.ToInt32(ufollow);
            follow.UserID = int.Parse(@Session["ID"].ToString());
            follow.UFollowID = Convert.ToInt32(ufollow);
     
            db.Follow.Add(follow);
            db.SaveChanges();
            return RedirectToAction("Index");
            // return View(follow);

        }

        //
        // GET: /Follow/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Follow follow = db.Follow.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        //
        // POST: /Follow/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Follow follow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(follow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(follow);
        }

        //
        // GET: /Follow/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Follow follow = db.Follow.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        //
        // POST: /Follow/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Follow follow = db.Follow.Find(id);
            db.Follow.Remove(follow);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}