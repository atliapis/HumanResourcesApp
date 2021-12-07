using HumanResourcesApp.DataAccess.Services;
using HumanResourcesApp.Services;
using HumanResourcesApp.Views.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace HumanResourcesApp.Controllers
{
    public class IndexModel
    {
        public PaginatedList<Employee> Employees { get; set; }

        public int? DepartmentIdFilter { get; set; }

        public int? StatusIdFilter { get; set; }

        public int? PageIndex { get; set; }

        public List<SelectListItem> DepartmentsList { get; set; }

        public List<SelectListItem> StatusesList { get; set; }

        public string LastNameSort { get; set; }
        public string DepartmentSort { get; set; }
        public string StatusSort { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel()
        {
        }
    }

    public class EmployeesController : Controller
    {
        private readonly HumanResourcesContext db;
        private readonly EmployeesService _employeesService;
        private readonly DepartmentsService _departmentsService;
        private readonly StatusesService _statusesService;
        private readonly SettingsService _settingsService;

        public EmployeesController()
        {
            db = new HumanResourcesContext();
            _employeesService = new EmployeesService(db);
            _departmentsService = new DepartmentsService(db);
            _statusesService = new StatusesService(db);
            _settingsService = new SettingsService(db);
        }

        // GET: Employees
        public async Task<ActionResult> Index(string sortOrder, string departmentIdFilter, string statusIdFilter, int? pageIndex)
        {
            

            if(pageIndex == null)
            {
                pageIndex = 1;
            }

            int departmentIdInt = 0;
            if (departmentIdFilter != null) departmentIdInt = Int32.Parse(departmentIdFilter);

            int statusIdInt = 0;
            if (statusIdFilter != null) statusIdInt = Int32.Parse(statusIdFilter);

            var employees = await _employeesService.GetEmployeesAsync(departmentIdInt, statusIdInt);

            //sorting
            var results = new IndexModel()
            {
                CurrentSort = sortOrder,
                LastNameSort = sortOrder == "last_name_asc" ? "last_name_desc" : "last_name_asc",
                DepartmentSort = sortOrder == "department_asc" ? "department_desc" : "department_asc",
                StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc"
            };

            // Sorting
            switch (sortOrder)
            {
                case "last_name_desc":
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
                case "last_name_asc":
                    employees = employees.OrderBy(e => e.LastName);
                    break;
                case "department_desc":
                    employees = employees.OrderByDescending(e => e.Department1.Name);
                    break;
                case "department_asc":
                    employees = employees.OrderBy(e => e.Department1.Name);
                    break;
                case "status_desc":
                    employees = employees.OrderByDescending(e => e.Status1.Name);
                    break;
                case "status_asc":
                    employees = employees.OrderBy(e => e.Status1.Name);
                    break;
                default:
                    employees = employees.OrderBy(e => e.LastName);
                    break;
            }

            results.Employees = await PaginatedList<Employee>.CreateAsync(employees, pageIndex ?? 1, await _settingsService.GetResultsPerPageSetting());
            results.DepartmentIdFilter = departmentIdInt;
            results.StatusIdFilter = statusIdInt;
            results.PageIndex = pageIndex;
            results.DepartmentsList = await PopulateDepartmentsList();
            results.StatusesList = await PopulateStatusesList();

            ViewBag.departmentIdFilter = results.DepartmentsList;
            ViewBag.statusIdFilter = results.StatusesList;

            return View(results);
        }

        private async Task<List<SelectListItem>> PopulateDepartmentsList()
        {
            var departmentsListItems = new List<SelectListItem>();

            var deps = await _departmentsService.GetAllDepartmentsAsync();

            departmentsListItems.Add(new SelectListItem()
            {
                Text = "All Departments",
                Value = "0",
                Selected = true
            });

            foreach (var dep in deps)
            {
                departmentsListItems.Add(new SelectListItem()
                {
                    Text = dep.Name,
                    Value = dep.Id.ToString()
                });
            }

            return departmentsListItems;
        }

        private async Task<List<SelectListItem>> PopulateStatusesList()
        {
            var statusesListItems = new List<SelectListItem>();

            var statuses = await _statusesService.GetAllStatusesAsync();

            statusesListItems.Add(new SelectListItem()
            {
                Text = "All Statuses",
                Value = "0",
                Selected = true
            });

            foreach (var status in statuses)
            {
                statusesListItems.Add(new SelectListItem()
                {
                    Text = status.Name,
                    Value= status.Id.ToString()
                });
            }

            return statusesListItems;
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Status = new SelectList(db.Statuses, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeNumber,FirstName,LastName,DateOfBirth,Email,Department,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Department = new SelectList(db.Departments, "Id", "Name", employee.Department);
            ViewBag.Status = new SelectList(db.Statuses, "Id", "Name", employee.Status);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name", employee.Department);
            ViewBag.Status = new SelectList(db.Statuses, "Id", "Name", employee.Status);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeNumber,FirstName,LastName,DateOfBirth,Email,Department,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name", employee.Department);
            ViewBag.Status = new SelectList(db.Statuses, "Id", "Name", employee.Status);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
