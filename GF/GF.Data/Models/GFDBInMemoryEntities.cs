using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Data.Models
{
    public class GFDBInMemoryEntities
    {
        public List<OrderRoll> getOrderRolls()
        {
            var result = new List<OrderRoll>();

            for (int i = 1; i < 100; i++) {
                result.Add(getOrderRoll(1, i, i+1));
            }

            return result;
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

        private static DateTime getDate(double dayoffset)
        { 
            return DateTime.Now.AddDays(dayoffset);
            
        }
    }
}
