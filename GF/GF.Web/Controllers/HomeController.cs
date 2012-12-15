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
            return RedirectToAction("HistoryDetailReport", new { CustomerID = CustomerID }); 
        }

        public ActionResult HistoryDetail(int? CustomerID)
        {
            var HistoryDetails = _gfRepository.GetOrderRolls(CustomerID.Value); 
            return View(HistoryDetails);
        }

 
        public ActionResult HistoryDetailReport(int? CustomerID)
        {

            var model = new HistoryDetailViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new HistoryDetailViewModel(this.GetGuid(), colList));
        }

        public ActionResult InventoryStatusReport(int? CustomerID)
        {
            var model = new InventoryStatusViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new InventoryStatusViewModel(this.GetGuid(), colList));
        }

        public ActionResult InventoryAgingReport(int? CustomerID)
        {
            var model = new InventoryAgingViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden();

            return View(new InventoryAgingViewModel(this.GetGuid(), colList));
        }

        [HttpPost]
        public ActionResult GetOrderRollHistory(OrderHistoryCriteria criteria, DataTable dataTable)
        { 
            var model = new HistoryDetailViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<OrderRoll> HistoryDetails = _gfRepository.GetOrderRolls(CustomerID).OrderBy(orderByClause).ToList<OrderRoll>();

            //var criteria = new OrderHistoryCriteria();
            return GetResults<OrderRoll>(criteria, dataTable, HistoryDetails, model.Columns); 
        }

        [HttpPost]
        public ActionResult GetOrderRollHistorySummary(OrderHistoryCriteria criteria, DataTable dataTable)
        {
            var model = new HistorySummaryViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<OrderRoll> HistoryDetails = _gfRepository.GetOrderRolls(CustomerID).OrderBy(orderByClause).ToList<OrderRoll>();
            return GetResults<OrderRoll>(criteria, dataTable, HistoryDetails, model.Columns);
        }

        [HttpPost]
        public ActionResult GetInventory(EmptyCriteria criteria, DataTable dataTable)
        {
            var model = new InventoryStatusViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> MaterialAvailability = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();

            return GetResults<MaterialAvailability>(criteria, dataTable, MaterialAvailability, model.Columns);
        }

        [HttpPost]
        public ActionResult GetInventoryAging(EmptyCriteria criteria, DataTable dataTable)
        {
            var model = new InventoryAgingViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> MaterialAvailability = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();

            return GetResults<MaterialAvailability>(criteria, dataTable, MaterialAvailability, model.Columns);
        }

        public ActionResult HistorySummaryReport(int? CustomerID)
        {
            var model = new HistorySummaryViewModel();
            string colList = this.GetGuidSession(model.ColumnKey) as string;
            if (colList == null)
                colList = model.GetColumnIndexesToBeHidden(); 
            return View(new HistorySummaryViewModel(this.GetGuid(), colList));
        }

        [HttpPost]
        public ActionResult GetMaterialAvailability(DataTable dataTable)
        {

            var model = new MaterialAvailabilityViewModel();
            this.SetGuidSession(dataTable.sKey, model.ColumnKey, dataTable.sColVis);
            var orderByClause = model.GetOrderByClause(dataTable);
            IList<MaterialAvailability> items = _gfRepository.GetMaterialAvailability(CustomerID).OrderBy(orderByClause).ToList<MaterialAvailability>();
            var criteria = new EmptyCriteria();
            return GetResults<MaterialAvailability>(criteria, dataTable, items, model.Columns);
        }

        private ReportResult GetResults<T>(Criteria criteria, DataTable dataTable, IList<T> Objects, IList<ColumnDef<T>> Columns)
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
            var dataTableDataMember = new DataTableDataMember(dataTable, Objects.Count, Objects.Count, table);

            var result = new ReportResult(new ReportModel(criteria, dataTableDataMember));
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
