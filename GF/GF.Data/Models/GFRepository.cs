using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Data.Models
{
    public class GFRepository
    {
        //RPH Temporatily work with in-membory objects
        //GFDBEntities context = new GFDBEntities();

        GFDBInMemoryEntities context = new GFDBInMemoryEntities();
        
        public List<OrderRoll> GetOrderRolls(int CustomerID)
        {
            
               // RPH temp comment
               // return context.OrderRolls.Where(p => p.CutomerID == CustomerID).ToList<OrderRoll>();

               return context.getOrderRolls().Where(p => p.CutomerID == CustomerID).ToList<OrderRoll>();
        }

      

    }
}
