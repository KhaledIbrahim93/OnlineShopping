using DMSTask.Command.Request;
using DMSTask.Repo;
using DMSTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMSTask.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        
        private IManageItems repository;
        public ItemsController()
        {
            repository = new ManageItems();
        }
        public ItemsController(IManageItems _repository)
        {
            this.repository = _repository;
        }
        public ActionResult Index()
        {
            try
            {
                var items = repository.GetAllItems();
                return View(items);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
      
        [HttpGet]
        public ActionResult GetItem(int id)
        {
            try
            {
                var item = repository.Getone(id);
                ViewBag.GetTax = repository.GetTax(id);
                ViewBag.GetDiscount = repository.GetDiscount(id);
                return View(item);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
               
           
        }
        [HttpPost]
        public JsonResult AddItemsToCart(OrderRequest orderRequest)
        {
            try
            {
                var insertItemsToCart = repository.AddToCart(orderRequest);
                if (insertItemsToCart)
                {
                       return Json(new { Result =true}, JsonRequestBehavior.AllowGet);
                }
                return Json(false, "Error......");
            }
            catch (Exception ex)
            {

                return Json(false, "Error......");
            }
        }

    }
}
