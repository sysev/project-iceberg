using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GF.Data;
using GF.Data.Helpers;
using GF.Data.Models;
using GF.Web.Models;
using GF.Web.Security;

namespace GF.Web.Controllers
{
    /// <summary>
    ///   Home Controller Class for proto
    ///      
    /// </summary> 
    public class HomeController : Controller
    {
        private IGFRepository _gfRepository;
        private GFPrincipal _gfPrincipal;
       
        public HomeController(IGFRepository GfRepository, GFPrincipal GfPrincipal)
        {
            _gfRepository = GfRepository;
            _gfPrincipal = GfPrincipal;
        }

        private int CustomerID
        {
            get
            { 
                return (_gfPrincipal.Identity as GFIdentity).CustomerID; 
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction("HistoryDetailServerSide", new { CustomerID = CustomerID }); 
        }

        public ActionResult HistoryDetail(int? CustomerID)
        {
            var HistoryDetails = _gfRepository.GetOrderRolls(CustomerID.Value); 
            return View(HistoryDetails);
        }

 
        public ActionResult HistoryDetailServerSide(int? CustomerID)
        {

            var model = new HistoryDetailViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new HistoryDetailViewModel(this.GetGuid(), colList));
        }
         
        [HttpPost]
        public ActionResult GetOrderRollHistory(DataTable dataTable)
        { 
            var model = new HistoryDetailViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<OrderRoll> HistoryDetails = _gfRepository.GetOrderRolls(CustomerID).OrderBy(orderByClause).ToList<OrderRoll>(); 
            return GetTableRows<OrderRoll>(dataTable, HistoryDetails, model.Columns); 
        }

        public ActionResult MaterialAvailabilityServerSide(int? CustomerID)
        {
            var model = new MaterialAvailabilityViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden(); 
            return View(new MaterialAvailabilityViewModel(this.GetGuid(), colList));
        }

        [HttpPost]
        public ActionResult GetMaterialAvailability(DataTable dataTable)
        {
           
            var model = new MaterialAvailabilityViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> items = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();
            return GetTableRows<MaterialAvailability>(dataTable, items, model.Columns);
        }


        private DataTableResult GetTableRows<T>(DataTable dataTable, IList<T> Objects, IList<ColumnDef<T>> Columns)
        {
            var end = dataTable.iDisplayStart + dataTable.iDisplayLength;
            if (end > Objects.Count - 1)
                end = Objects.Count;

            List<List<string>> table = new List<List<string>>();
            for (int i = dataTable.iDisplayStart; i < end; i++)
            {
                var row = new List<string>();
                foreach (var c in Columns)
                {
                    var obj = Objects[i];
                    var value = c.ValueFunction(obj); 
                    row.Add(value);
                }
                table.Add(row);
            }

           var result = new DataTableResult(dataTable, Objects.Count, Objects.Count, table);
           result.ContentEncoding = Encoding.UTF8;
           return result;

        }

         
        public ActionResult About()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        
    }
}
