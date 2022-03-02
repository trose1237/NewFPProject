using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewFP.Models;

namespace NewFP.Controllers
{
    public class SearchRecipeController : Controller
    {
        private readonly familypiDBEntities db = new familypiDBEntities();
        // GET: SearchRecipe

        public ActionResult Index()
        {
            return View(db.Recipes.ToList());
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync(string searchString)
        {
            ViewData["Getrecipes"] = searchString;

            var recipes = from R in db.Recipes
                          select R;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Name.Contains(searchString));
            }

            return View(await recipes.ToListAsync());
        }
        //public ActionResult Index(string option, string search)
        //{
        //    if (option == "Makes")
        //    {
        //        return View(db.Recipes.Where(x => x.Makes == search || search == null).ToList());
        //    }
        //    else
        //    {
        //        return View(db.Recipes.Where(x => x.Name == search || search == null).ToList());
        //    }
        //}

        // GET: SearchRecipe/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchRecipe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchRecipe/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchRecipe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchRecipe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchRecipe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchRecipe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
