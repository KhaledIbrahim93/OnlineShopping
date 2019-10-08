using DMSTask.Command.Request;
using DMSTask.Command.Response;
using DMSTask.Models;
using DMSTask.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSTask.Services
{
    public class ManageCart : IManageCart
    {
        private ShoppingEntities Context;
        public ManageCart()
        {
            Context = new ShoppingEntities();
        }
        public ManageCart(ShoppingEntities _context)
        {
            Context = _context;
        }
        public IEnumerable<Dictionary<ItemResponse,int>> AllItemsInCart(out int Itemcount)
        {
            Itemcount = 0;
            var itemDictionary = new Dictionary<ItemResponse, int>();
            var items = new List<Dictionary<ItemResponse, int>>();
           var itemsInCart = (from o in Context.OrderDetails
                               join s in Context.Items
                               on o.ItemId equals s.IId
                               select new {s,o.ODID}).ToList();
            Itemcount = itemsInCart.Count;
            foreach (var item in itemsInCart)
            {
                var itemResponse = new ItemResponse()
                {
                    Id = item.s.IId,
                    Description = item.s.IDescription,
                    Image = item.s.Image,
                    Name = item.s.IName,
                    Price = item.s.Price,
                    Qty = item.s.Qty
                };
                itemDictionary.Add(itemResponse, item.ODID);
                items.Add(itemDictionary);
            }
            return items;
        }
        public bool DeleteOne(int id)
        {
            var item = Context.OrderDetails.Where(e => e.ODID == id).FirstOrDefault();
            if (item.Equals(null))
            {
                return false;
            }
            Context.OrderDetails.Remove(item);
            Context.SaveChanges();
            return true;
        }
        public void AddOrder()
        {
            var newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                RequestDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(2),
                Satuts = "Open",
                CustomerId = 1

            };
            Context.Orders.Add(newOrder);
            Context.SaveChanges();
            var OrderItems= Context.OrderDetails.Where(e => e.CustomerId == 1).ToList();
            foreach (var item in OrderItems)
            {
                item.OID = newOrder.OID;
            }
            Context.SaveChanges();

        }
        public OrderResponse ViewOrder(int customerId)
        {
            var order = Context.Orders.Where(e => e.CustomerId == customerId).FirstOrDefault();
            var totualPrice = Context.OrderDetails.Where(s => s.CustomerId == customerId).Sum(s => s.Total);
            var CustomerName = Context.Customers.Where(e => e.CustomerCode == customerId).Select(s => s.DesAr).FirstOrDefault();
            var orderResponse = new OrderResponse()
            {
                RequestDate = order.RequestDate,
                OrderDate = order.OrderDate,
                DueDate = order.DueDate,
                CustomerName = CustomerName,
                Satuts = order.Satuts,
                TotualPrice = totualPrice
            };
            return orderResponse;
        }
    }
}