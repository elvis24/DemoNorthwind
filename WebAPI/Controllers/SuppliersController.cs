using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using Datos;

namespace WebAPI.Controllers
{
    public class SuppliersController : ApiController
    {
        // GET: api/Suppliers
        public IHttpActionResult Get()
        {
            List<SuppliersViewModel> lst = new List<SuppliersViewModel>();
            using (var db = new NorthwindEntities())
            {
                lst = (from d in db.Suppliers
                       select new SuppliersViewModel
                       {
                           SupplierID = d.SupplierID,
                           CompanyName = d.CompanyName,
                           ContactName = d.ContactName,
                           ContactTitle = d.ContactTitle,
                           Address = d.Address,
                           City = d.City,
                           PostalCode = d.PostalCode,
                           Country = d.Country,
                           Phone = d.Phone,
                           Fax = d.Fax,
                           HomePage = d.HomePage
                       }).ToList();
            }
            return Ok(lst);
        }

        // GET: api/Suppliers/5
        public IHttpActionResult Get(int id)
        {
            SuppliersViewModel suppliers = null;
            using (NorthwindEntities db = new NorthwindEntities())
            {
                suppliers = db.Suppliers.Where(x => x.SupplierID == id).Select(x => new SuppliersViewModel()
                {
                    SupplierID = x.SupplierID,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address,
                    City = x.City,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    Phone = x.Phone,
                    HomePage = x.HomePage

                }).FirstOrDefault();
            }
            if (suppliers==null)
            {
                return NotFound();
            }
            return Ok(suppliers);
        }

        // POST: api/Suppliers
        public IHttpActionResult Post(Suppliers suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No es un modelo Valido");
            }
            using (var db= new NorthwindEntities())
            {
                db.Suppliers.Add(new Suppliers()
                {
                    SupplierID = suppliers.SupplierID,
                    CompanyName = suppliers.CompanyName,
                    ContactName = suppliers.ContactName,
                    ContactTitle = suppliers.ContactTitle,
                    Address = suppliers.Address,
                    City = suppliers.City,
                    PostalCode = suppliers.PostalCode,
                    Country = suppliers.Country,
                    Phone = suppliers.Phone,
                    HomePage = suppliers.HomePage
                });
                db.SaveChanges();
            }
            return Ok();
        }

        // PUT: api/Suppliers/5
        public IHttpActionResult Put(SuppliersViewModel suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No es un modelo ");
            }
            using (var db = new NorthwindEntities())
            {
                var data = db.Suppliers.Where(x => x.SupplierID == suppliers.SupplierID).FirstOrDefault();
                if (data !=null)
                {
                    data.SupplierID = suppliers.SupplierID;
                    data.CompanyName = suppliers.CompanyName;
                    data.ContactName = suppliers.ContactName;
                    data.ContactTitle = suppliers.ContactTitle;
                    data.Address = suppliers.Address;
                    data.City = suppliers.City;
                    data.PostalCode = suppliers.PostalCode;
                    data.Country = suppliers.Country;
                    data.Phone = suppliers.Phone;
                    data.HomePage = suppliers.HomePage;
                }
                db.SaveChanges();
            }
            return Ok();
        }

        // DELETE: api/Suppliers/5
        public IHttpActionResult Delete(int id)
        {
            if (id <=0)
            {
                return BadRequest("No es un Supplier valido");
            }
            using (var db = new NorthwindEntities())
            {
                var suppliers = db.Suppliers.Where(x => x.SupplierID == id).FirstOrDefault();
                db.Entry(suppliers).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
