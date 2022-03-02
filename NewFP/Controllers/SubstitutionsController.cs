using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewFP.Models;

namespace NewFP.Controllers
{
    public class SubstitutionsController : Controller
    {
        private familypiDBEntities db = new familypiDBEntities();

        // GET: Substitutions
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "Ingredient_desc" : "";
            var substitutions = from s in db.Substitutions
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                substitutions = substitutions.Where(s => s.Ingredient.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Ingredient_desc":
                    substitutions = substitutions.OrderByDescending(s => s.Ingredient);
                    break;
                default:
                    substitutions = substitutions.OrderBy(s => s.Ingredient);
                    break;
            }

            return View(substitutions.ToList());
        }

        // GET: Substitutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Substitution substitution = db.Substitutions.Find(id);
            if (substitution == null)
            {
                return HttpNotFound();
            }
            return View(substitution);
        }

        // GET: Substitutions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Substitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientID,Ingredient,Substitution1,Substitution2,Substitution3")] Substitution substitution)
        {
            if (ModelState.IsValid)
            {
                db.Substitutions.Add(substitution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(substitution);
        }

        // GET: Substitutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Substitution substitution = db.Substitutions.Find(id);
            if (substitution == null)
            {
                return HttpNotFound();
            }
            return View(substitution);
        }

        // POST: Substitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredientID,Ingredient,Substitution1,Substitution2,Substitution3")] Substitution substitution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(substitution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(substitution);
        }

        // GET: Substitutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Substitution substitution = db.Substitutions.Find(id);
            if (substitution == null)
            {
                return HttpNotFound();
            }
            return View(substitution);
        }

        // POST: Substitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Substitution substitution = db.Substitutions.Find(id);
            db.Substitutions.Remove(substitution);
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
