using DMSTask.Repo;
using DMSTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMSTask.Controllers
{
    public class CartController : Controller
    {
        private IManageCart repository;
        public CartController()
        {
            repository = new ManageCart();
        }
        public CartController(IManageCart _repository)
        {
            this.repository = _repository;
        }
        // GET: Cart
        public ActionResult Index()
        {
            try
            {
                var items = repository.AllItemsInCart(out int count);
                ViewBag.ItemInCart = count;
                return View(items);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult DeleteItemFromCart(int id)
        {
            try
            {
                var deletedEntity = repository.DeleteOne(id);
                if (deletedEntity)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult AddOrder(int customerId)
        {
            try
            {
                repository.AddOrder();
                var orderInfo = repository.ViewOrder(customerId);
                return View(orderInfo);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
  
    }
}
