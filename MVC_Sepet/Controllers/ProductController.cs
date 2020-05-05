using MVC_Sepet.Models;
using MVC_Sepet.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Sepet.Controllers
{
    public class ProductController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            Products eklenecek = db.Products.Find(id);
            CartItem ci = new CartItem();
            ci.ID = eklenecek.ProductID;
            ci.Price = eklenecek.UnitPrice;
            ci.Name = eklenecek.ProductName;
            c.AddItem(ci);
            Session["scart"] = c;
            TempData["cart"] = $"{eklenecek.ProductName} sepete eklendi";
            return RedirectToAction("Index");
        }

        public ActionResult MyCart()
        {
            if (Session["scart"] != null)
            {
                Cart cart = Session["scart"] as Cart;
                return View(cart);
            }
            else
            {
                TempData["emptyCart"] = "Sepetiniz Boş. Harika ürünlerimizden bir tanesini ekleyip tekrar gelebilirsiniz :p";
                return RedirectToAction("Index");
            }

        }


        public ActionResult DeleteFromCart(int productId)
        {
            Cart cart = Session["scart"] as Cart;
            cart.Delete(productId);
            return RedirectToAction("MyCart");



        }
        public ActionResult Adress()
        {
          
            OrderModel order = new OrderModel();
            order.Cart = Session["scart"] as Cart;
            return View(order);


        }

        [HttpPost]
        public ActionResult Adress(OrderModel orderModel)
        {
            orderModel.Cart= Session["scart"] as Cart;
            Session.Abandon();
            Session.Clear();
            return View();


        }
       
        private void ClearCart()
        {
            throw new NotImplementedException();
        }

       

        public PartialViewResult _PartialOrder()
        {
            Cart cart = Session["scart"] as Cart;
            return PartialView(cart);
    }

       
}
}
