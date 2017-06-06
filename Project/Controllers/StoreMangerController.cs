using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class StoreMangerController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        public ActionResult Index()
        {
            List<Album> alubum = db.Albums.ToList();
            return View(alubum);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }
        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (TryUpdateModel(album))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(album).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(Album album,int id)
        {
            album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}