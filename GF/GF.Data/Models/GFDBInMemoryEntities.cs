using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.Data.Helpers;

namespace GF.Data.Models
{
    public class GFDBInMemoryEntities : IGFRepository
    {

        public IList<MaterialAvailability> GetMaterialAvailability(int CustomerID)
        {
            var result = new List<MaterialAvailability>();

            for (int i = 1; i < 20000; i++)
            {
                result.Add(getMaterialAvailability(1, i, i + 1));
            }

            return result;
        }

        private static IList<OrderRoll> _orderRolls;
        public IList<OrderRoll> GetOrderRolls(int CutomerID)
        {
            if (_orderRolls == null)
            {
                _orderRolls = new List<OrderRoll>();
                for (int i = 1; i < 20000; i++)
                {
                    _orderRolls.Add(getOrderRoll(1, i, i + 1));
                }
            }
            return _orderRolls;
        }


        private static OrderRoll getOrderRoll(int val1, int val2, int val3)
        {
            string num = "Z" + val1 + val2 + val3;

            return new OrderRoll()
            {
                CutomerID = val1,
                RollNumber = "RN" + num,
                CustomerPartNumber = "CPN" + num + "39",
                RollWeight = val1 + val3,
                OrderDate = getDate(-val2),
                ShippedDate = getDate(2 - val2),
                ReceivedDate = getDate(5 - val2),
                LastPhysicalDate = getDate(0),
                CustomerPONumber = "CPO" + num
            };
        }

        private static MaterialAvailability getMaterialAvailability(int val1, int val2, int val3)
        {
            string num = "M" + (val1 % 2) + (val2 % 3) + (val3 % 4);

            return new MaterialAvailability()
            {
               MaterialNumber = num,
               QuanityInStock = val1 % 10,
               RollWeight = val2 % 9 
            };
        }

        private static DateTime getDate(double dayoffset)
        { 
            return DateTime.Now.AddDays(dayoffset);
            
        }
    }
}
