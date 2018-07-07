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
    public class SuppliersController : Controller
    {
        private OnlineOrder db = new OnlineOrder();

        // GET: Suppliers
        public ActionResult SuppliersList()
        {
            return View(db.Suppliers.ToList());
        }

    }
}
