using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

namespace GF.Web.Models
{
    public class AutoReplenishmentViewModel : ObjectItemViewModel<EmptyCriteria, OrderRoll>
    {
        public AutoReplenishmentViewModel() { }
        public AutoReplenishmentViewModel(string key, string Cols)
        {
            this.ViewKey = key;
            this.Cols = Cols;
        }

        public EmptyCriteria criteria { get; set; }
         
        private static IList<ColumnDef<OrderRoll>> ColumnDefs =
             new List<ColumnDef<OrderRoll>>(){
                    new ColumnDef<OrderRoll>("Material Num", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<OrderRoll>("Customer Part Num", "CustomerPartNumber", p => p.CustomerPartNumber),
                    new ColumnDef<OrderRoll>("Material Desc", "MaterialDescription", p => p.MaterialNumber),
                    new ColumnDef<OrderRoll>("Weight", "Weight", p => p.RollWeight.ToString()),
                    new ColumnDef<OrderRoll>("Reorder Point", "ReorderPoint", p => "<input class=\"in-table\" type=\"text\"/>"),
                    new ColumnDef<OrderRoll>("Reorder Amount", "ReorderAmount", p => "<input class=\"in-table\" type=\"text\"/>"),
                    new ColumnDef<OrderRoll>("Allow Auto Replenishment", "AllowAutoReplenishment", p => "<input type=\"checkbox\"/>"),
                };

        public override IList<ColumnDef<OrderRoll>> Columns { get { return ColumnDefs; } }
  
    }
     
}