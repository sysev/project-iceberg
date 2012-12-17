using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;
using System.Security.Cryptography;

namespace GF.Web.Models
{
    public class InventoryAgingViewModel : ObjectItemViewModel<EmptyCriteria, MaterialAvailability>
    {
        public InventoryAgingViewModel() { }
        public InventoryAgingViewModel(string key, string Cols)
        {
            this.ViewKey = key;
            this.Cols = Cols;
        }

        public EmptyCriteria criteria { get; set; }

        public static int RandNumber(int Low, int High)
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int rnd = rndNum.Next(Low, High);
            return rnd;
        }

        private static IList<ColumnDef<MaterialAvailability>> ColumnDefs {
            
            get{
                string greencheck = "<img src=\"/Images/green-check.png\"/>";
                string yellowcheck = "<img src=\"/Images/yellow-check.png\"/>";
                string redcheck = "<img src=\"/Images/red-check.png\"/>";
                int daysSinceReceived = 0;
                List<ColumnDef<MaterialAvailability>> list = new List<ColumnDef<MaterialAvailability>>() {
                    new ColumnDef<MaterialAvailability>("Material Num", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<MaterialAvailability>("Roll Num", "RollNumber", p => "5"),
                    new ColumnDef<MaterialAvailability>("Customer Part Num", "CustomerPartNumber", p => "???"),
                    new ColumnDef<MaterialAvailability>("Material Desc", "MaterialDescription", p => "???"),
                    new ColumnDef<MaterialAvailability>("Weight", "Weight", p => "30"),
                    new ColumnDef<MaterialAvailability>("Days Since Received", "DaysSinceReceived", p => 
                        {daysSinceReceived = RandNumber(0, 250);
                          return daysSinceReceived.ToString();
                        }),
                    new ColumnDef<MaterialAvailability>("0-30 Days", "TimeRange1", p => (daysSinceReceived < 31) ? greencheck : ""),
                    new ColumnDef<MaterialAvailability>("31-60 Days", "TimeRange2", p => (daysSinceReceived > 30 && daysSinceReceived < 61) ? greencheck : ""),
                    new ColumnDef<MaterialAvailability>("61-90 Days", "TimeRange3", p => (daysSinceReceived > 60 && daysSinceReceived < 91) ? yellowcheck : ""),
                    new ColumnDef<MaterialAvailability>("91-180 Days", "TimeRange4", p => (daysSinceReceived > 90 && daysSinceReceived < 181) ? redcheck : ""),
                    new ColumnDef<MaterialAvailability>("> 180 Days", "TimeRange5", p => (daysSinceReceived > 180) ? redcheck : "")
                };
                return list;
            }
        }

        public override IList<ColumnDef<MaterialAvailability>> Columns { get { return ColumnDefs; } }
  
    }
     
}