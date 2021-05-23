using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using WebApplicationbandaMusicales;
using System.Runtime.Remoting.Messaging;

namespace WebApplicationbandaMusicales.Controllers
{
    public class BandasMusicalesController : Controller
    {
        private ProyectosWebEntities1 db = new ProyectosWebEntities1();

        // GET: BandasMusicales
        public ActionResult Index()
        {
            return View(db.BandasMusicales.ToList());
        }

        // GET: BandasMusicales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandasMusicales bandasMusicales = db.BandasMusicales.Find(id);
            if (bandasMusicales == null)
            {
                return HttpNotFound();
            }
            return View(bandasMusicales);
        }

        // GET: BandasMusicales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BandasMusicales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,banda,albums,descripcion,notas,ruta_foto")] BandasMusicales bandasMusicales)
        {
            HttpPostedFileBase http = Request.Files[0];
            WebImage webimage = new WebImage(http.InputStream);
            bandasMusicales.ruta_foto = webimage.GetBytes();
           
            if (ModelState.IsValid)
            {
                db.BandasMusicales.Add(bandasMusicales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bandasMusicales);
        }

        // GET: BandasMusicales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandasMusicales bandasMusicales = db.BandasMusicales.Find(id);
            if (bandasMusicales == null)
            {
                return HttpNotFound();
            }
            return View(bandasMusicales);
        }

        // POST: BandasMusicales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,banda,albums,descripcion,notas,ruta_foto")] BandasMusicales bandasMusicales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandasMusicales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bandasMusicales);
        }

        // GET: BandasMusicales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandasMusicales bandasMusicales = db.BandasMusicales.Find(id);
            if (bandasMusicales == null)
            {
                return HttpNotFound();
            }
            return View(bandasMusicales);
        }

        // POST: BandasMusicales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BandasMusicales bandasMusicales = db.BandasMusicales.Find(id);
            db.BandasMusicales.Remove(bandasMusicales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
