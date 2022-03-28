using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAPPWithDBFirst.Models;
using System.IO;



namespace MVCAPPWithDBFirst.Controllers
{
    [HandleError]
    public class ProductsController : Controller
    {
       
        // GET: Products
       // [Authorize]
       [RequireHttps]
        public ActionResult Index(string search = "", string sortColName="ProductName", string iconClass="fa-sort-down")
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            List<Product> Products = db.Products.Where(p => p.ProductName.Contains(search)).ToList();

            ViewBag.sortcol = sortColName;
            ViewBag.iconClass = iconClass;
            if (ViewBag.sortcol == "ProductName")
            {
                if(ViewBag.iconClass == "fa-sort-up")
                {
                    Products=Products.OrderBy(p => p.ProductName).ToList();
                }
                else
                {
                    Products = Products.OrderByDescending(p => p.ProductName).ToList();
                }
            }
            return View(Products);

        }
        
        [HandleError]
        public ActionResult Details(int? id)
        {

            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            Product product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imageBytes = new Byte[file.ContentLength];
                file.InputStream.Read(imageBytes, 0, file.ContentLength);
                var base64String = Convert.ToBase64String(imageBytes, 0, file.ContentLength);
                product.photo = base64String;
            }

            return View(product);
        }
        public ActionResult Create()
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            ViewBag.cats = db.Categories.ToList();
            ViewBag.brds = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {

            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            if(Request.Files.Count>=1)
            {
                var file = Request.Files[0];
                var imageBytes = new Byte[file.ContentLength];
               file.InputStream.Read(imageBytes, 0,file.ContentLength);
                var base64String = Convert.ToBase64String(imageBytes, 0, file.ContentLength);
                product.photo= base64String;
            }
           
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            Product exist = db.Products.Where(p => p.ProductId == id).FirstOrDefault();

            return View(exist);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imageBytes = new Byte[file.ContentLength];
                file.InputStream.Read(imageBytes, 0, file.ContentLength);
                var base64String = Convert.ToBase64String(imageBytes, 0, file.ContentLength);
                product.photo = base64String;
            }
            Product exist = db.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            exist.ProductName = product.ProductName;
            exist.AvailabilityStatus = product.AvailabilityStatus;
            exist.DateofPurchase = product.DateofPurchase;
            exist.Price = product.Price;
            exist.CategoryId = product.CategoryId;
            exist.BrandId = product.BrandId;
            exist.Active = product.Active;
            exist.photo= product.photo;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            Product protodel = db.Products.Where(m => m.ProductId == id).FirstOrDefault();
            return View(protodel);
        }
            [HttpPost]
        public ActionResult Delete(Product product)
        {
            EFDataBaseFirstApproachEntities db = new EFDataBaseFirstApproachEntities();
            Product protodel=db.Products.Where(m=>m.ProductId == product.ProductId).FirstOrDefault();
            db.Products.Remove(protodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}