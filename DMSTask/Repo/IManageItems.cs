using DMSTask.Command.Request;
using DMSTask.Command.Response;
using DMSTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSTask.Repo
{
   public interface IManageItems
    {
        IEnumerable<ItemResponse> GetAllItems();
        bool AddToCart(OrderRequest request);
        ItemResponse Getone(int id);
        decimal GetTax(int itemId);
        decimal GetDiscount(int itemId);
        
        bool DeleteOne(int id);
        void Save();
    }
}
