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
   public interface IManageCart
    {
        IEnumerable<Dictionary<ItemResponse,int>>AllItemsInCart(out int Itemcount);
        bool DeleteOne(int id);
        void AddOrder();
        OrderResponse ViewOrder(int customerId);
    }
}
