using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GF.Web.Models
{
    public class DataTable
    {

        public DataTable()
        {
            bSortables = new List<bool>();
            bSearchables = new List<bool>();
            sSearchs = new List<string>(); 
            bEscapeRegexs = new List<bool>();
            iSortCols = new List<int>();
            sSortDirs = new List<DataTableSortDirection>();
        }

        /// <summary>
        /// Information for DataTables to use for rendering
        /// </summary>
        public string sEcho { get; set; }
        /// <summary>
        /// Display start point
        /// </summary>
        public int iDisplayStart { get; set; }
        /// <summary>
        /// Number of records to display
        /// </summary>
        public int iDisplayLength { get; set; }
        /// <summary>
        /// Number of columns being displayed (useful for getting individual column search info)
        /// </summary>
        public int iColumns { get; set; }
        /// <summary>
        /// Global search field
        /// </summary>
        public string sSearch { get; set; }
        /// <summary>
        /// Global search is regex or not
        /// </summary>
        public bool? bEscapeRegex { get; set; }
        /// <summary>
        /// Indicator for if a column is flagged as sortable or not on the client-side
        /// </summary>
        public IList<bool> bSortables { get; set; }
        /// <summary>
        /// Indicator for if a column is flagged as searchable or not on the client-side
        /// </summary>
        public IList<bool> bSearchables { get; set; }
        /// <summary>
        /// Individual column filter
        /// </summary>
        public IList<string> sSearchs { get; set; }
        /// <summary>
        /// Individual column filter is regex or not
        /// </summary>
        public IList<bool> bEscapeRegexs { get; set; }
        /// <summary>
        /// Number of columns to sort on
        /// </summary>
        public int iSortingCols { get; set; }
        /// <summary>
        /// Column being sorted on (you will need to decode this number for your database)
        /// </summary>
        public IList<int> iSortCols { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }
        public int iSortCol_1 { get; set; }
        public string sSortDir_1 { get; set; }
        public int iSortCol_2 { get; set; }
        public string sSortDir_2 { get; set; }
        public int iSortCol_3 { get; set; }
        public string sSortDir_3 { get; set; }
        public int iSortCol_4 { get; set; }
        public string sSortDir_4 { get; set; }
        public int iSortCol_5 { get; set; }
        public string sSortDir_5 { get; set; }
        /// <summary>
        /// Direction to be sorted - "desc" or "asc". Note that the prefix for this variable is wrong in 1.5.x where iSortDir_(int) was used)
        /// </summary>
        public IList<DataTableSortDirection> sSortDirs { get; set; }

        public string sColVis { get; set; }
        public string sKey { get; set; }

        private IList<Tuple<int, string>> PossibleSortColumns
        {
            get
            {
                return new List<Tuple<int, string>>(){
                    new Tuple<int, string>(iSortCol_0, sSortDir_0),
                    new Tuple<int, string>(iSortCol_1, sSortDir_1),
                    new Tuple<int, string>(iSortCol_2, sSortDir_2),
                    new Tuple<int, string>(iSortCol_3, sSortDir_3),
                    new Tuple<int, string>(iSortCol_4, sSortDir_4),
                    new Tuple<int, string>(iSortCol_5, sSortDir_5)
                };
            }
        }

        public IList<Tuple<int, string>> SortColumns
        {
            get
            {
                var result = new List<Tuple<int, string>>();
                foreach (var item in PossibleSortColumns){
                    if (!string.IsNullOrEmpty(item.Item2)){
                        result.Add(item);
                    }
                }
                return result;
            }
        }  
    }

    /// <summary>
    /// The sort order of a column.
    /// </summary>
    public enum DataTableSortDirection
    {
        /// <summary>
        /// Sort the column ascending
        /// </summary>
        Ascending,

        /// <summary>
        /// Sort the column descending
        /// </summary>
        Descending
    }
}