using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAPPWithDBFirst.Models;
namespace MVCAPPWithDBFirst.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brands
        public ActionResult Index()
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
    }
    
}