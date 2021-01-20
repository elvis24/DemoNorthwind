using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CategorieController : ApiController
    {
        // GET: api/Categorie
        public IHttpActionResult Get()
        {
            List<CategoriesViewModel> lst = new List<CategoriesViewModel>();
            using (var ctx = new NorthwindEntities())
            {
                lst = (from d in ctx.Categories
                       select new CategoriesViewModel
                       {
                           CategoryID = d.CategoryID,
                           CategoryName = d.CategoryName,
                           Description = d.Description,
                           picture = d.Picture
                       }).ToList();
            }
            return Ok(lst);
        }

        // GET: api/Categorie/5
        public IHttpActionResult Get(int id)
        {
            CategoriesViewModel categories = null;
            using (var db = new NorthwindEntities())
            {
                categories = db.Categories.Where(x => x.CategoryID == id).Select(x => new CategoriesViewModel()
                {
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    picture = x.Picture
                }).FirstOrDefault();
            }
            if (categories ==null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        // POST: api/Categorie
        public IHttpActionResult Post(CategoriesViewModel categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No es un modelo valido");
            }
            using (var db = new NorthwindEntities())
            {
                db.Categories.Add(new Categories()
                {
                    CategoryID = categories.CategoryID,
                    CategoryName = categories.CategoryName,
                    Description = categories.Description,
                    Picture = categories.picture
                });
                db.SaveChanges();
            }
            return Ok();
        }

        // PUT: api/Categorie/5
        public IHttpActionResult Put(CategoriesViewModel categories)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest("No es un modelo valido");
            }
            using (var db = new NorthwindEntities())
            {
                var data = db.Categories.Where(x => x.CategoryID == categories.CategoryID).FirstOrDefault();
                if (data !=null)
                {
                    data.CategoryName = categories.CategoryName;
                    data.Description = categories.Description;
                    data.Picture = categories.picture;
                }
                db.SaveChanges();
            }
            return Ok();
        }

        // DELETE: api/Categorie/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("No es una categoria validad");
            }
            using (var db = new NorthwindEntities())
            {
                var categoria = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
                db.Entry(categoria).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
