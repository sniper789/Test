using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer list
        public ActionResult List()
        {
            CustomerListViewModel model = new CustomerListViewModel
            {
                //CustomerList = _context.Customers.Include("MembershipType").ToList() 
                CustomerList = _context.Customers.Include(c => c.MembershipType).ToList()
            };
                
            return View(model);
        }

        public ActionResult Details(int id)
        {
            Customer model = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            return View(model);
        }
    }
}