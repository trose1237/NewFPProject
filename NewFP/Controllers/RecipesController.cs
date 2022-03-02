using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewFP.Models;

namespace NewFP.Controllers
{
    public class RecipesController : Controller
    {
        private familypiDBEntities db = new familypiDBEntities();

        // GET: Recipes
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            var recipes = from r in db.Recipes 
                          select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    recipes = recipes.OrderByDescending(r => r.Name);
                    break;
                default:
                    recipes = recipes.OrderBy(r => r.Name);
                    break;
            }

            return View(recipes.ToList());
        }

        //[HttpGet]
        //public async Task<ActionResult> IndexAsync(string searchString)
        //{
        //    ViewData["Getrecipes"] = searchString;

        //    var recipes = from R in db.Recipes
        //                  select R;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        recipes = recipes.Where(s => s.Name.Contains(searchString));
        //    }

        //    return View(await recipes.ToListAsync());
        //}

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,Name,Makes,Ingredients,Directions,Storing")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,Name,Makes,Ingredients,Directions,Storing")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
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
