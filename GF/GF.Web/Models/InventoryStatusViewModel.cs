using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

namespace GF.Web.Models
{
    public class InventoryStatusViewModel : ObjectItemViewModel<EmptyCriteria, MaterialAvailability>
    {
        public InventoryStatusViewModel() { }
        public InventoryStatusViewModel(string key, string Cols)
        {
            this.ViewKey = key;
            this.Cols = Cols;
        }

        public EmptyCriteria criteria { get; set; }
         
        private static IList<ColumnDef<MaterialAvailability>> ColumnDefs =
             new List<ColumnDef<MaterialAvailability>>(){
                    new ColumnDef<MaterialAvailability>("Material Num", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<MaterialAvailability>("Customer Part Num", "CustomerPartNumber", p => "5"),
                    new ColumnDef<MaterialAvailability>("Material Desc", "MaterialDescription", p => "???"),
                    new ColumnDef<MaterialAvailability>("Qty in Stock", "QuantityInStock", p => p.QuantityInStock.ToString()),
                    new ColumnDef<MaterialAvailability>("Lbs in Stock", "PoundsInStock", p => "3"),
                    new ColumnDef<MaterialAvailability>("Reorder Point", "ReorderPoint", p => "2"),
                    new ColumnDef<MaterialAvailability>("Reorder Amount", "ReorderAmount", p => "2"),
                    new ColumnDef<MaterialAvailability>("Qty Ordered", "QuantityOrdered", p => "2"),
                    new ColumnDef<MaterialAvailability>("Lbs Ordered", "PoundsOrdered", p => "3"),
                    new ColumnDef<MaterialAvailability>("Qty in Transit", "QuantityInTransit", p => "3"),
                    new ColumnDef<MaterialAvailability>("Lbs in Transit", "PoundsInTransit", p => "2"),
                };

        public override IList<ColumnDef<MaterialAvailability>> Columns { get { return ColumnDefs; } }
  
    }
     
}