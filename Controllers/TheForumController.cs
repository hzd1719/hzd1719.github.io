using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheForum.Controllers
{
    public class TheForumController : Controller
    {

        private TheForum.Models.TheForumContext _db = new TheForum.Models.TheForumContext();
        // GET: TheForum
        public ActionResult Index()
        {
            var mostRecentEntries =
                (from entry in _db.Entries
                 orderby entry.DateAdded descending
                 select entry).Take(20);
            ViewBag.Entries = mostRecentEntries.ToList();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TheForum.Models.TheForumEntry entry)
        {
            entry.DateAdded = DateTime.Now;
            _db.Entries.Add(entry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult Show(int id)
        {
            var entry = _db.Entries.Find(id);
            bool hasPermission = User.Identity.Name == entry.Name;
            ViewData["hasPermission"] = hasPermission;
            return View(entry);
        }
    }
}