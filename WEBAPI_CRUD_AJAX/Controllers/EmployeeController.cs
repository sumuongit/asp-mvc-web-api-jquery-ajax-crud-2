using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WEBAPI_CRUD_AJAX.Controllers
{
    public class EmployeeController : ApiController
    {
        private TestDBEntities db = new TestDBEntities();

        // GET api/EmployeesAPI
        [Route("api/EmployeesAPI")]
        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.AsEnumerable();
        }

        // GET api/EmployeesAPI/5
        [Route("api/EmployeesAPI/{id}")]
        public Employee GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return employee;    
        }

        // PUT api/EmployeesAPI/5
        [Route("api/EmployeesAPI/{id}")]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            if (id != employee.EmployeeID)
            {
                return NotFound();
            }           

            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/EmployeesAPI
        [Route("api/EmployeesAPI")]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return Ok();
            }
            else
            {               
                return NotFound();
            }
        }

        // DELETE api/EmployeesAPI/5
        [Route("api/EmployeesAPI/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
