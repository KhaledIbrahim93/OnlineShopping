using DMSTask.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSTask.Models;
using DMSTask.Command.Response;
using DMSTask.Command.Request;

namespace DMSTask.Services
{
    public class ManageItems : IManageItems
    {
        private ShoppingEntities Context;
        public ManageItems()
        {
            Context = new ShoppingEntities();
        }
        public ManageItems(ShoppingEntities _context)
        {
            Context = _context;
        }

        public bool DeleteOne(int id)
        {
            var entity = Context.Items.Find(id);
            if (entity.Equals(null))
            {
                return false;
            }
            Context.Items.Remove(entity);
            return true;
        }

        public IEnumerable<ItemResponse> GetAllItems()
        {
            return Context.Items.Select(e => new ItemResponse()
            {
                Id = e.IId,
                Description = e.IDescription,
                Image = e.Image,
                Name = e.IName,
                Price = e.Price,
                Qty = e.Qty
            });
        }

        public bool AddToCart(OrderRequest request)
        {
            var total = (request.Quntity * request.Price)+ request.Tax -request.Discount;
            var newOrder = new OrderDetail()
            {
                ItemId = request.ItemId,
                CustomerId=1,
                OrderQty = request.Quntity,
                Total = total,
            };
            var realQunttity = Context.Items.Where(e => e.IId == request.ItemId).Select(s => s.Qty).FirstOrDefault();
            var newqunt = realQunttity - request.Quntity;
            var updateQuntity = Context.Items.Where(e => e.IId == request.ItemId).FirstOrDefault();
            updateQuntity.Qty = newqunt;
            Context.OrderDetails.Add(newOrder);
            Context.SaveChanges();
            return true;
        }

        public ItemResponse Getone(int id)
        {
            return Context.Items.Where(e => e.IId == id).Select(e => new ItemResponse()
            {
                Id = e.IId,
                Description = e.IDescription,
                Image = e.Image,
                Name = e.IName,
                Price = e.Price,
                Qty = e.Qty
            }).FirstOrDefault();
        }
        public decimal GetTax(int itemId)
        {
            var DiscountTaxId = Context.Items.Where(e => e.IId == itemId).Select(s => s.DiscountTaxID).FirstOrDefault();
            if (DiscountTaxId==null)
            {
                return 0;
            }
            return Context.TaxDiscounts.Where(e => e.ID == DiscountTaxId).Select(e => e.Tax.Value).FirstOrDefault();
        }
        public decimal GetDiscount(int itemId)
        {
            var DiscountTaxId = Context.Items.Where(e => e.IId == itemId).Select(s => s.DiscountTaxID).FirstOrDefault();
            if (DiscountTaxId == null)
            {
                return 0;
            }
            return Context.TaxDiscounts.Where(e => e.ID == DiscountTaxId).Select(e => e.Discount.Value).FirstOrDefault();
        }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}