using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

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
         
        private static IList<ColumnDef<MaterialAvailability>> ColumnDefs =
             new List<ColumnDef<MaterialAvailability>>(){
                    new ColumnDef<MaterialAvailability>("Material Num", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<MaterialAvailability>("Roll Num", "RollNumber", p => "5"),
                    new ColumnDef<MaterialAvailability>("Customer Part Num", "CustomerPartNumber", p => "???"),
                    new ColumnDef<MaterialAvailability>("Material Desc", "MaterialDescription", p => "???"),
                    new ColumnDef<MaterialAvailability>("Weight", "Weight", p => "30"),
                    new ColumnDef<MaterialAvailability>("Days Since Received", "DaysSinceReceived", p => "3"),
                    new ColumnDef<MaterialAvailability>("0-30 Days", "TimeRange1", p => ""),
                    new ColumnDef<MaterialAvailability>("31-60 Days", "TimeRange2", p => ""),
                    new ColumnDef<MaterialAvailability>("61-90 Days", "TimeRange3", p => ""),
                    new ColumnDef<MaterialAvailability>("91-180 Days", "TimeRange4", p => ""),
                    new ColumnDef<MaterialAvailability>("> 180 Days", "TimeRange5", p => "")
                };

        public override IList<ColumnDef<MaterialAvailability>> Columns { get { return ColumnDefs; } }
  
    }
     
}