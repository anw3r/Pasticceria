using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pasticceria;
using System.IO;
using System.Timers;

namespace Pasticceria.Controllers
{
    public class ProductsController : Controller
    {
        private Model2 db = new Model2();

        [Authorize]
        // GET: Products
        public async Task<ActionResult> List()
        {
            return View(await db.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        [Authorize]
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "idProduct,ProdName,Ingredients,ProdDate,Details,Photo,Price")]
        public async Task<ActionResult> Create( Product p,HttpPostedFileBase ProdImg)
        {
            try
            {
                if (ProdImg != null)
                {
                    string pathDisk = Server.MapPath("~/GraphiCSS");
                    ProdImg.SaveAs(Path.Combine(pathDisk, ProdImg.FileName));
                    p.Photo = ProdImg.FileName;
                }
                

                db.Product.Add(p);
                await db.SaveChangesAsync();
                return RedirectToAction("List");
                //return View(p);
            }
            catch (Exception ex)
            {
                ViewBag.Error=ex.Message;
                return View();
            }

        }


        [Authorize]
        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "idProduct,ProdName,Ingredients,Details,Price,ProdImg")]
        public async Task<ActionResult> Edit(Product p, HttpPostedFileBase ProdImg)
        {
            Product ModProduct = new Product();
            //if (ModelState.IsValid)
            //{
            try
            {
                if (ProdImg != null)
                {
                    string pathDisk = Server.MapPath("~/GraphiCSS");
                    ProdImg.SaveAs(Path.Combine(pathDisk, ProdImg.FileName));
                    p.Photo = ProdImg.FileName;
                }
                if (p.ProdDate==null)
                {
                    p.ProdDate = DateTime.Now;
                }
                
                db.Entry(p).State = EntityState.Modified;
               
                await db.SaveChangesAsync();
                
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            //}
            //return RedirectToAction("Index");
        }
        [Authorize]
        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Product.FindAsync(id);
            db.Product.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    
        public async Task<ActionResult> Cart(int id,int qta)
        {
            Product p=db.Product.Find(id);
            if (qta <= p.NPieces)
            {
                p.NPieces = p.NPieces - qta;
                db.Entry(p).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                ViewBag.Error = "Quantità Non Disponibile";
            }

            return RedirectToAction("Index","Home");
        }


        public async Task<ActionResult> Refresh()
        {
            foreach (var i in db.Product.ToList())
            {
                if (DateTime.Today.Day == i.ProdDate.Value.Day + 1)
                {
                    i.Price = (i.Price * 80)/100 ;
                }
                else if (DateTime.Today.Day == i.ProdDate.Value.Day + 2)
                {
                    i.Price = (i.Price *20)/100;
                }
                else if (DateTime.Today.Day == i.ProdDate.Value.Day + 3)
                {
                    db.Product.Remove(i);
                }
            }
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

    }
}
