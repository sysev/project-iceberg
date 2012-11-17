using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Data.Models
{
    public class GFRepository : IGFRepository
    {
        //RPH Temporatily work with in-membory objects
        GFDBEntities context = new GFDBEntities();

       // GFDBInMemoryEntities context = new GFDBInMemoryEntities();

        public IList<MaterialAvailability> GetMaterialAvailability(int CustomerID)
        {
            return context.MaterialAvailabilities.ToList<MaterialAvailability>();
        }

        public IList<OrderRoll> GetOrderRolls(int CustomerID)
        {
            
               return context.OrderRolls.Where(p => p.CutomerID == CustomerID).ToList<OrderRoll>();
        }

      

    }
}
