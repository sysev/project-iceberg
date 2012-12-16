using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace GF.Web.Models
{
    /// <summary>
    ///   Generic view model base class to help organize and manage
    ///   aspects of each object view model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectItemViewModel<C, T>
    {
         
        private static string COMMA = ", ";
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
                    OrderByClause.Append(COMMA);
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
                        sb.Append(COMMA);
                    sb.Append(ndx);
                }
                ndx++; 
            }
            return sb.ToString();
        }
         
        public IList<IList<string>> AsTable(bool includeHeader)
        {
            var table =  new List<IList<string>>();

            if (includeHeader)
            {
                var row = new List<string>();
                foreach (var c in Columns)
                {
                    row.Add(c.DisplayName);
                }
                table.Add(row);
            }

            foreach (var item in Results)
            {
                var row = new List<string>();
                foreach (var c in Columns)
                {
                    row.Add(c.ValueFunction(item));
                }
                table.Add(row);
            }
            return table;
        }

        public IList<IList<KeyValuePair<string, string>>> AsKeyValuePairList()
        {
           var result =  new List<IList<KeyValuePair<string, string>>>();
           foreach (var item in Results)
           {
               var row = new List<KeyValuePair<string, string>>();
               foreach (var c in Columns)
               {
                   row.Add(new KeyValuePair<string,string>(c.Name, c.ValueFunction(item)));
               }
               result.Add(row);
           }
           return result;
        }

        public MemoryStream AsStream()
        { 
            var stream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(List<T>)); 
            //We turn it into an XML and save it in the memory
            serializer.Serialize(stream, Results);
            stream.Position = 0;
            return stream;

        }

        [XmlElement("Items")]
        public IList<T> Results { get; set; }

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