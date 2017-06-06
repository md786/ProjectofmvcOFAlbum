using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        public ActionResult Index()
        {
            var query = db.Albums.ToList();
            //For Album category like rock ,pop
            var category = from m in query
                        orderby m.Albumid 
                          select m.CategoryName;
            ViewBag.cat = category.Distinct();
            //For Album Top 5 Album Name
            var top = from s in query
                      orderby s.AlbumTitle
                      select s;
          


            return View(top.Distinct().Take(5));
        }
        public ActionResult Browse(string strCaegory)
        {
            ViewBag.cat = strCaegory;
            var fi = from m in db.Albums.ToList()
                     where m.CategoryName == strCaegory
                     select m;
            return View(fi);
        }
        public ActionResult ShowAll()
        {
            var album = from m in db.Albums.ToList()
                        select m;

            return View(album);
        }
        public ActionResult Details(int id)
        {
            Album album = db.Albums.Find(id);
           
            return View(album);
        }
        [HttpPost]
        public ActionResult Details(int Albumid,int qty)
        {
            
            return View();
        }
       




    }
}