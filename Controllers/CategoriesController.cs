using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAPPWithDBFirst.Models;
using System.Linq.Expressions;

namespace MVCAPPWithDBFirst.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
           List<Category>categories= db.Categories.ToList();
            return View(categories);
        }
    }
}