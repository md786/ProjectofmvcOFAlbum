using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ShoppingCartController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        // GET: ShoppingCart
        [HttpGet]
         public ActionResult Index()
        {
            if (Session["cartid"] != null)
            {
              string str  =Session["cartid"].ToString();
                var q = from m in db.Carts.ToList()
                        where m.Cartid == str
                        select m;
                return View(q);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Index(int albumid,int qty)
        {
            Cart obj = new Cart();
            obj.Cartid = GetCartId();
            obj.Albumid = albumid;
            obj.DateCreated = DateTime.Now;
            obj.Quantity = qty;
            db.Carts.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        public string GetCartId()
        {
            if (Session["cardid"] == null)
            {
                Session["cartid"] = Guid.NewGuid().ToString();
            }
            return Session["cartid"].ToString();
        }
        public ActionResult Second()
        {
            return View();
        }
        public ActionResult Confirm()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Confirm( Customer obj)
        {
            
            obj.OraderDate = DateTime.Now;
                obj.Total = (int)Session["total"];
            db.Customers.Add(obj);
            db.SaveChanges();
            TempData["custmerid"] = obj.CustomerId;
            //2.move items from cart to orderdetails
            string str = Session["cartid"].ToString();
            var q = from m in db.Carts.ToList()
                    where m.Cartid == str
                    select m;
            foreach (var item in q)
            {
                OrderDetail ord = new OrderDetail();
                ord.CustomerId = obj.CustomerId;
                ord.Albumid = item.Albumid;
                ord.Quantity = item.Quantity;
                ord.UnitPrice = item.Album.UnitPrice;
                db.OrderDetails.Add(ord);
                db.SaveChanges();
            }
            //3.step is delete item from cart 
            foreach (var item in q)
            {
                db.Carts.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("Complete");
        }
        public ActionResult Delete(int id)
        {
           Cart obj= db.Carts.Find(id);
            db.Carts.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     public ActionResult Complete()
        {

            return View(db.OrderDetails.ToList());
        }

    }
}