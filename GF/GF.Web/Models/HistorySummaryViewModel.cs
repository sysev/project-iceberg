using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

namespace GF.Web.Models
{
    public class HistorySummaryViewModel : ObjectItemViewModel<OrderHistoryCriteria, OrderRoll>
    {
        public HistorySummaryViewModel() { }
        public HistorySummaryViewModel(string key, string Cols)
        {
            this.ViewKey = key;
            this.Cols = Cols;
        }

        public OrderHistoryCriteria criteria { get; set; }
         
        private static IList<ColumnDef<OrderRoll>> ColumnDefs = 
             new List<ColumnDef<OrderRoll>>(){
                    new ColumnDef<OrderRoll>("Material Num", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<OrderRoll>("Customer Part Num", "CustomerPartNumber", p => p.CustomerPartNumber),
                    new ColumnDef<OrderRoll>("Material Desc", "MaterialDescription", p => "???"),
                    new ColumnDef<OrderRoll>("Wt", "RollWeight", p => p.RollWeight.ToString()),
                    new ColumnDef<OrderRoll>("Number of Rolls", "RollQuantity", p => "10"),
                };

        public override IList<ColumnDef<OrderRoll>> Columns { get { return ColumnDefs; } }
  
    }
     
}