using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GF.Web.Models
{
    /// <summary>
    ///   Generic view model base class to help organize and manage
    ///   aspects of each object view model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectItemViewModel<C, T>
    {
         
        public string ViewKey { get; set; }
        public string Cols { get; set; }
        public C Criteria { get; set; }
        public abstract IList<ColumnDef<T>> Columns { get; }
        public IList<string> GetColumNames()
        {
            var result = new List<string>();
            foreach (var item in Columns)
                result.Add(item.Name);
            return result;
        }

        public string GetOrderByClause(DataTable dataTable)
        {
            var OrderByClause = new StringBuilder();
            var ColumnList = GetColumNames();
            foreach (var sortItem in dataTable.SortColumns)
            {
                if (OrderByClause.Length > 0)
                    OrderByClause.Append(", ");
                OrderByClause.Append(ColumnList[sortItem.Item1] + " " + sortItem.Item2);
            }
            return OrderByClause.ToString();
        }

        public string GetColumnIndexesToBeHidden()
        {
            var sb = new StringBuilder("");
            int ndx=0;
            foreach (var column in Columns)
            {
                if (!column.ShowByDefault)
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(ndx);
                }
                ndx++;
                
            }
            return sb.ToString();
        }

        public string ColumnKey
        {
            get
            {
                return "columns_" + this.GetType().ToString();
            }
        }

        public ObjectItemViewModel()
        {
            // TODO: Complete member initialization
        }
    }
}