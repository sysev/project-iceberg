using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

namespace GF.Web.Models
{
    public class HistoryDetailViewModel : ObjectItemViewModel<OrderHistoryCriteria, OrderRoll>
    {
        public HistoryDetailViewModel() { }
        public HistoryDetailViewModel(string key, string Cols)
        {
            this.ViewKey = key;
            this.Cols = Cols;
        }

        public OrderHistoryCriteria criteria { get; set; }
         
        private static IList<ColumnDef<OrderRoll>> ColumnDefs = 
             new List<ColumnDef<OrderRoll>>(){
                    new ColumnDef<OrderRoll>("Roll #", "RollNumber", p => p.RollNumber),
                    new ColumnDef<OrderRoll>("Material #", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<OrderRoll>("Part #", "CustomerPartNumber", p => p.CustomerPartNumber),
                    new ColumnDef<OrderRoll>("Material Desc.", "MaterialDescription", p => "???"),
                    new ColumnDef<OrderRoll>("Weight", "RollWeight", p => p.RollWeight.ToString()),
                    new ColumnDef<OrderRoll>("Status", "OrderStatus", p => 
                        (p.UsedDate != null) ? "USED" : (p.ReceivedDate != null) ? "RECEIVED" : (p.ShippedDate != null) ? "SHIPPED" : (p.OrderDate != null) ? "ORDERED" : ""
                    ),
                    new ColumnDef<OrderRoll>("Date Ordered", "OrderDate", p => p.OrderDate.ToString("d")),
                    new ColumnDef<OrderRoll>("Date Shipped", "ShippedDate", p => p.ShippedDate.ToString("d")),
                    new ColumnDef<OrderRoll>("Date Received", "ReceivedDate", p => p.ReceivedDate.ToString("d")),
                    new ColumnDef<OrderRoll>("Last Physical Inventory", "LastPhysicalDate", p => p.LastPhysicalDate.ToString("d")),
                    new ColumnDef<OrderRoll>("Date Used", "UsedDate", p => p.UsedDate.ToString("d")),
                    new ColumnDef<OrderRoll>("PO #", "CustomerPONumber", p => p.CustomerPONumber)
                };

        public override IList<ColumnDef<OrderRoll>> Columns { get { return ColumnDefs; } }
  
    }
     
}