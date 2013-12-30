using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManager.Models;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        // GET: /Employee/
        public ActionResult Index()
        {
            ViewBag.IsReadOnly = GlobalData.CurrentUser.ReadOnly;
            return View(db.Employees.ToList());
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.IsReadOnly = GlobalData.CurrentUser.ReadOnly;
            ViewBag.StartDateString = string.Empty;
            if (id == null || id == -1) //-1 is the value specified in the "create" actionlink in the cshtml
            {
                return View(); //this is our case for creating a new record
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            //DisplayFormat of StartDate is yyyy-mm-DD, but that is only in order to be compatible with browsers that use the HTML5 date picker control.  We're calling ToShortDateString so that we can use the standard US date format for other browsers.
            ViewBag.StartDateString = employee.StartDate.ToShortDateString();
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EmployeeId,FirstName,MiddleName,LastName,StartDate")] Employee employee)
        {
            ViewBag.IsReadOnly = GlobalData.CurrentUser.ReadOnly;
            if (ModelState.IsValid)
            {
                if (employee.ID == -1) //-1 is the value specified in the "Create" actionlink in the cshtml
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //param selEmployeePk is provided by a hidden input with the same ID.  ("pk" naming convention is used b/c there is also a property called Employee.EmployeeId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int selEmployeePk)
        {
            Employee employee = db.Employees.Find(selEmployeePk);
            db.Employees.Remove(employee);
            db.SaveChanges();
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
