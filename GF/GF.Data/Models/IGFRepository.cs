using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Data.Models
{
    public interface IGFRepository
    {
        IList<OrderRoll> GetOrderRolls(int CustomerID);
        IList<MaterialAvailability> GetMaterialAvailability(int CustomerID);
    }
}
