using DotNetCoreCRUD.Data;
using DotNetCoreCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
       public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var res = _context.Employees.Include(d=>d.Department).OrderBy(e=>e.EmployeeName).ToList();
            return View(res);
        }


        public IActionResult Creat()
        {
            ViewBag.dept = _context.Departments.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creat(Employee newEmp)
        {
            UploadImage(newEmp);
            if (ModelState.IsValid)
            {
                _context.Employees.Add(newEmp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.dept = _context.Departments.ToList();
            return View();
        }

    

        public IActionResult Edit(int? id)
        {
            ViewBag.dept = _context.Departments.ToList();
            var result = _context.Employees.Where(n=>n.EmployeeID==id).SingleOrDefault();
            return View("Creat", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp)
        {
            UploadImage(emp);
            if(ModelState.IsValid)
            {
                _context.Employees.Update(emp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dept = _context.Departments.ToList();
            return View(emp);
        }


        public IActionResult Delete(int? id)
        {
            var res = _context.Employees.Where(n=>n.EmployeeID==id).SingleOrDefault();
            if(res!=null)
            {
                _context.Employees.Remove(res);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        private void UploadImage(Employee newEmp)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                // to upload image
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                newEmp.ImageUser = ImageName;

            }
            else if (newEmp.ImageUser == null && newEmp.EmployeeID == null)
            {
                // not upload image and new employee
                newEmp.ImageUser = "Default.jpg";
            }
            else
            {
                newEmp.ImageUser = newEmp.ImageUser;
            }
        }
    }
}
