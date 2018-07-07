using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineDelivery.Models;


namespace OnlineDelivery.Controllers
{
    public class ProductiteamsController : Controller
    {
        private OnlineOrder db = new OnlineOrder();

        // GET: Productiteams
        public ActionResult ProductList()
        {
            return View(db.Productiteams.ToList());
        }

        
        public ActionResult Add(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            var itemproduct = db.Productiteams.Where(p => p.Product_ID == id).First();
            cart.Add(itemproduct);

            return RedirectToAction("ShowCart", "Productiteams");
        }

        
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            return RedirectToAction("ProductList", "Productiteams");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult Remove(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);
            return RedirectToAction("ShowCart", "Productiteams"); 
        }

        public ActionResult Update(FormCollection form )
        {
            Cart cart = Session["Cart"] as Cart;
            int idpr = int.Parse(form["Product_ID"]);
            int sln = int.Parse(form["Product_Quantity"]);
            cart.Update(idpr, sln);
            return RedirectToAction("ShowCart", "Productiteams");
        }

        //public PartialViewResult Shop()
        //{
        //    int totalitem = 0;
        //    Cart cart = Session["Cart"] as Cart;
        //    totalitem = cart.Product_Quantity();
        //      ViewBag.inforcart = totalitem;
        //    return PartialView("shop");
        //}

        public ActionResult Checkout()
        {
            return View("Checkout");
        }

        public ActionResult saveorder(FormCollection fc)
        {
            Cart cart = Session["Cart"] as Cart;

            //shopkepper Address

            Shopkeeper shopkeeper = new Shopkeeper();

            shopkeeper.Shopkeeper_Name = fc["Shopkeeper_Name"];
            shopkeeper.Shopkeeper_Location = fc["Shopkeeper_Location"];
            shopkeeper.Shopkeeper_Area = fc["Shopkeeper_Area"];
            shopkeeper.Shopkeeper_Phone = int.Parse(fc["Shopkeeper_Phone"]);
            db.Shopkeepers.Add(shopkeeper);
            db.SaveChanges();

            //Payment
            Payment payment = new Payment();

            payment.Payment_Type = fc["Payment_Type"];
            payment.Payment_Phone = int.Parse(fc["Payment_Phone"]);
            payment.Payment_Code = int.Parse(fc["Payment_Code"]);
            payment.Payment_Amount = int.Parse(fc["Payment_Amount"]);
            db.Payments.Add(payment);
            db.Payments.Add(payment);

            //order main

            OrderMain orderMain = new OrderMain();

            orderMain.Shopkeeper_ID = shopkeeper.Shopkeeper_ID;
            orderMain.Payment_ID = payment.Payment_ID;
            //supplier id ---
            orderMain.Delivery_Address = fc["Delivery_Address"];
            orderMain.Order_Date = int.Parse(fc["Order_Date"]);
            orderMain.Delivery_Date = int.Parse(fc["Delivery_Date"]);

            foreach (var item in cart.Items)
            {
                OrderSub orderSub = new OrderSub();
                double Total = item.PQuantity * item.Productiteams.Product_Price.Value;
                orderSub.OrderMain_ID = orderMain.OrderMain_ID;
                orderSub.Product_ID = item.Productiteams.Product_ID;
                orderSub.Quantity = item.PQuantity;
                orderSub.Total_price = (int)Total;
                db.OrderSubs.Add(orderSub);
            }

            db.Payments.Add(payment);
            cart.Clear();
            return View("Thanks");
        }
    }
}
