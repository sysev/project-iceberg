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
        GFDBEntities context = new GFDBEntities();
        
        public List<OrderRoll> GetOrderRolls(int CustomerID)
        {
            
               // context.Database.Connection.Open();
                return context.OrderRolls.Where(p => p.CutomerID == CustomerID).ToList<OrderRoll>();
            
        }

      

    }
}
