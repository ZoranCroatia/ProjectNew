using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProjectAutos.DAL;
using ProjectAutos.Models;
using PagedList;

namespace ProjectAutos.Controllers
{
    public class VehicleMakeController : Controller
    {
        private AutosContext db = new AutosContext();

        // GET: VehicleMake
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "Abrv_desc" : "Abrv";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var vehicleMakes = from s in db.VehicleMakes
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Abrv.Contains(searchString)
                                       || s.Name.Contains(searchString));
            }

            // This is for sorting objects (sorting rows)
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Name);
                    break;
                case "Abrv":
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Abrv);
                    break;
                case "Abrv_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Abrv);
                    break;
                default:  // Name ascending 
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(vehicleMakes.ToPagedList(pageNumber, pageSize));
        }

        // GET: VehicleMake/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = db.VehicleMakes.Find(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Abrv")]VehicleMake vehicleMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.VehicleMakes.Add(vehicleMake);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(vehicleMake);
        }


        // GET: VehicleMake/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = db.VehicleMakes.Find(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMakeToUpdate = db.VehicleMakes.Find(id);
            if (TryUpdateModel(vehicleMakeToUpdate, "",
               new string[] { "Name", "Abrv" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(vehicleMakeToUpdate);
        }

        // GET: VehicleMake/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            VehicleMake vehicleMake = db.VehicleMakes.Find(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                VehicleMake vehicleMake = db.VehicleMakes.Find(id);
                db.VehicleMakes.Remove(vehicleMake);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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