using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {

                List<Employee> emplist = db.Employees.ToList<Employee>();
                return Json(new { data = emplist }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                using (DBModel db = new DBModel())
                {

                    return View(db.Employees.Where(x => x.EmployeeiD == id).FirstOrDefault<Employee>());
                }


            }
        }

        [HttpPost]
        public ActionResult AddorEdit(Employee emp)//we have to pass instance of object in defined stages of enity that's we have created instance 
        {
            using (DBModel db = new DBModel())
            {
                if (emp.EmployeeiD == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "saved Successfully" }, JsonRequestBehavior.AllowGet);


                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            using (DBModel db = new DBModel()) {
                Employee emp = db.Employees.Where(x => x.EmployeeiD == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}