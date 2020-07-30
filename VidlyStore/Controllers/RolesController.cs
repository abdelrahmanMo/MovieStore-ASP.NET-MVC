using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using VidlyStore.Models;

namespace VidlyStore.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class RolesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(_context.Roles.ToList());
        }

      
        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _context.Entry(role).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            }
            catch
            {
                return View(role);
            }
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole myRole)
        {
        
                // TODO: Add delete logic here
                IdentityRole role = _context.Roles.Find(myRole.Id);
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
            
        }
    }
}
