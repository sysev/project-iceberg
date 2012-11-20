using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

namespace GF.Web.Models
{
    public class HistoryDetailViewModel : ObjectItemViewModel<OrderRoll>
    {
        public HistoryDetailViewModel() { }
        public HistoryDetailViewModel(string key, string Cols)
        {
            this.ViewKey = key;
            this.Cols = Cols;
        }
         
        private static IList<ColumnDef<OrderRoll>> ColumnDefs = 
             new List<ColumnDef<OrderRoll>>(){
        
                    new ColumnDef<OrderRoll>("Order Date", "OrderDate", p => p.OrderDate.ToString("d")),
                    new ColumnDef<OrderRoll>("Part #", "CustomerPartNumber", p => p.CustomerPartNumber),
                    new ColumnDef<OrderRoll>("Roll #", "RollNumber", p => p.RollNumber),
                    new ColumnDef<OrderRoll>("Roll Wt", "RollWeight", p => p.RollWeight.ToString()),
                };

        public override IList<ColumnDef<OrderRoll>> Columns { get { return ColumnDefs; } }
  
    }
     
}