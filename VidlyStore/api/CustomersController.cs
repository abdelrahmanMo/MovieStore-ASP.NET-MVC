using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.api
{
    [Authorize(Roles = RoleName.Admin + "," + RoleName.Managers)]
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
      
        public CustomersController()
        {
            _context = new ApplicationDbContext();
            
        }
        //GET /api/customer/1
        [AllowAnonymous]
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.customers.Include(c => c.MemberShipType);
            if (!string.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where((c => c.Name.Contains(query)));

            var customerDTO = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDTO);
        }
        //GET /api/customer/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.customers.SingleOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //POST /api/customer
        [Authorize(Roles = RoleName.Publishers + "," + RoleName.Admin + "," + RoleName.Managers)]
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var customer = _context.customers.Add(Mapper.Map<CustomerDto, Customer>(customerDto));
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto)  ;
        }
        //PUT /api/customer/1
        [Authorize(Roles = RoleName.Publishers + "," + RoleName.Admin + "," + RoleName.Managers)]
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();
            Mapper.Map(customerDTo, customerInDb);
            _context.SaveChanges();
            return Ok();
        }
        [Authorize(Roles = RoleName.Publishers + "," + RoleName.Admin + "," + RoleName.Managers)]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return  NotFound();
            _context.customers.Remove(customerInDb);
            _context.SaveChanges();
            return Ok();
        }

    }
}
