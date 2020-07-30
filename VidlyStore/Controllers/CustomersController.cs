using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyStore.Models;

namespace VidlyStore.Controllers
{
    [Authorize(Roles = RoleName.Admin + "," + RoleName.Managers)]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
            public CustomersController()
            {
                _context = new ApplicationDbContext();
            }

            protected override void Dispose(bool disposing)
            {
                 _context.Dispose();
            }
            [AllowAnonymous]
            // GET: Customers
            public ActionResult Index()
            {

                return View();
            }

            public ActionResult Detail(int id)
            {
                var customers = _context.customers.Include(c=>c.MemberShipType).SingleOrDefault(c => c.Id == id);
                if (customers == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(customers);
                }
                
            }

            public ActionResult New()
            {
                ViewBag.MembershipType = new SelectList(_context.MembershipType.ToList(), "Id", "Name"); 
                return View("CustomerForm",new Customer());
            }
            
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Save(Customer customer)
            {
                if (!ModelState.IsValid)
                {
                ViewBag.MembershipType = new SelectList(_context.MembershipType.ToList(), "Id", "Name");
                return View("CustomerForm", customer);
                }
                if (customer.Id == 0)
                {
                    _context.customers.Add(customer);
                }
                else
                {
                    var customerInDb = _context.customers.Single(m => m.Id == customer.Id);
                    customerInDb.Name = customer.Name;
                    customerInDb.BirthDate = customer.BirthDate;
                    customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
                    customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                }

                _context.SaveChanges();
                return RedirectToAction("Index","Customers");
            }

            public ActionResult Edit(int id)
            {
                var customer = _context.customers.SingleOrDefault(m => m.Id == id);
                ViewBag.MembershipType = new SelectList(_context.MembershipType.ToList(), "Id", "Name");
                if (customer == null)
                {
                    return HttpNotFound();
                }

                return View("CustomerForm",customer);

            }
        }
    }
