using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using V2_test.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Net;

namespace V2_test.Controllers
{
    public class HomeController : Controller
    {
        private MongoDbContext _context { get; set; }

        public HomeController()
        {
            _context = new MongoDbContext();
        }
        // GET: Home
        public  ActionResult Index()
        {
            //Get Data from Db
            ViewBag.Data = _context.Students.AsQueryable().Select(a=>new Student { Name=a.Name,Address=a.Address ,StudentId=a.StudentId}).ToList();
            return View();
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentId,Name,Address")] Student student)
        {
            try
            {
                //Insert one item 
                await _context.Students.InsertOneAsync(student);
                return View();

            }
            catch (Exception)
            {
                return View(student);
            }

        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            
            Student student = GetStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,StudentId,Name,Address")] Student student)
        {
            //Full update /replace
            // await _context.Students.ReplaceOneAsync(r => r.StudentId == student.StudentId, student);
           
            //Partial Update
            var update = Builders<Student>.Update.Set(s => s.Name, student.Name);
            await _context.Students.UpdateOneAsync(r => r.StudentId == student.StudentId, update);
            return RedirectToAction("index");
        }

        private Student GetStudent(int id)
        {
            //Filter element
            var std = _context.Students
                .Find(r => r.StudentId == id)
                .FirstOrDefault();
            return std;
        }

        public async Task<ActionResult> Delete(int id)
        { 
            //Delete element           
            await _context.Students.DeleteOneAsync(s=> s.StudentId==id);
            return RedirectToAction("index");
        }
    }
}