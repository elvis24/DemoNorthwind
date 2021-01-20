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
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        public IHttpActionResult Get()
        {
            List<ProductsViewModel> lst = new List<ProductsViewModel>();
            using (var db = new NorthwindEntities())
            {
                lst = (from d in db.Products
                       select new ProductsViewModel
                       {
                           ProductID = d.ProductID,
                           ProductName = d.ProductName,
                           QuantityPerUnit = d.QuantityPerUnit,
                           UnitPrice = d.UnitPrice,
                           UnitsInStock = d.UnitsInStock,
                           UnitsOnOrder = d.UnitsOnOrder,
                           ReorderLevel = d.ReorderLevel,
                           Discontinued = d.Discontinued
                       }).ToList();
            }
            return Ok(lst);
        }

        // GET: api/Productos/5
        public IHttpActionResult Get(int id)
        {
            ProductsViewModel products = null;
            using (var db = new NorthwindEntities())
            {
                products = db.Products.Where(x => x.ProductID == id).Select(x => new ProductsViewModel()
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    QuantityPerUnit = x.QuantityPerUnit,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    UnitsOnOrder = x.UnitsOnOrder,
                    ReorderLevel = x.ReorderLevel,
                    Discontinued = x.Discontinued
                }).FirstOrDefault();
            }
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // POST: api/Productos
        public IHttpActionResult Post(ProductsViewModel products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No es un modelo valido");
            }
            using (var db = new NorthwindEntities())
            {
                db.Products.Add(new Products()
                {
                    ProductID = products.ProductID,
                    ProductName = products.ProductName,
                    QuantityPerUnit = products.QuantityPerUnit,
                    UnitPrice = products.UnitPrice,
                    UnitsInStock = products.UnitsInStock,
                    UnitsOnOrder = products.UnitsOnOrder,
                    ReorderLevel = products.ReorderLevel,
                    Discontinued = products.Discontinued
                });
                db.SaveChanges();
            }
            return Ok();
        }

        // PUT: api/Productos/5
        public IHttpActionResult Put(ProductsViewModel products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No es un modelo valido");
            }
            using (var db = new NorthwindEntities())
            {
                var existeProducto = db.Products.Where(x => x.ProductID == products.ProductID).FirstOrDefault<Products>();
                if (existeProducto !=null)
                {
                    existeProducto.ProductName = products.ProductName;
                    existeProducto.QuantityPerUnit = products.QuantityPerUnit;
                    existeProducto.UnitPrice = products.UnitPrice;
                    existeProducto.UnitsInStock = products.UnitsInStock;
                    existeProducto.UnitsOnOrder = products.UnitsOnOrder;
                    existeProducto.ReorderLevel = products.ReorderLevel;
                    existeProducto.Discontinued = products.Discontinued;
                }
                db.SaveChanges();
            }
            return Ok();
        }

        // DELETE: api/Productos/5
        public IHttpActionResult Delete(int id)
        {
            if (id <=0)
            {
                return BadRequest("No es una identificacion de Producto valido");
            }
            using (var db = new NorthwindEntities())
            {
                var productos = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                db.Entry(productos).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
