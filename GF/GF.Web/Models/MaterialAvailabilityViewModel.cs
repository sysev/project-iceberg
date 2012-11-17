using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GF.Data;

namespace GF.Web.Models
{
    public class MaterialAvailabilityViewModel : ObjectItemViewModel<MaterialAvailability>
    {
        private static IList<ColumnDef<MaterialAvailability>> ColumnDefs =
             new List<ColumnDef<MaterialAvailability>>(){ 
                    new ColumnDef<MaterialAvailability>("Material Number", "MaterialNumber", p => p.MaterialNumber),
                    new ColumnDef<MaterialAvailability>("Quanity In Stock", "QuanityInStock", p => p.QuanityInStock.ToString()),
                    new ColumnDef<MaterialAvailability>("Roll Weight", "RollWeight", p => p.RollWeight.ToString()), 
                };

        public override IList<ColumnDef<MaterialAvailability>> Columns { get { return ColumnDefs; } }
  
    }
     
}