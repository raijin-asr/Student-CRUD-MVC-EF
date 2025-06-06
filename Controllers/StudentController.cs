using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_MVC_EF.Data;
using Student_CRUD_MVC_EF.Models;
using Student_CRUD_MVC_EF.Models.Entities;

using Microsoft.AspNetCore.Hosting; // for IWebHostEnvironment
using System.IO; // for Path

namespace Student_CRUD_MVC_EF.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment; // Inject IWebHostEnvironment

        public StudentController(ApplicationDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment; // Initialize it

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            string? photoPath = null;

            try
            {
                if (viewModel.Photo != null && viewModel.Photo.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(viewModel.Photo.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.Photo.CopyToAsync(stream);
                    }

                    photoPath = $"/images/{fileName}";
                }


                var student = new Student
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Subscribed = viewModel.Subscribed,
                    PhotoPath = photoPath

                };
                await dbContext.Students.AddAsync(student);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //after student is add, redirect to the read action
            return RedirectToAction("Read", "student");
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var students= await dbContext.Students.ToListAsync();
            return View(students);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var studentToUpdate = await dbContext.Students.FindAsync(viewModel.Id);

            if(studentToUpdate is not null)
            {
                studentToUpdate.Name = viewModel.Name;
                studentToUpdate.Email = viewModel.Email;
                studentToUpdate.Phone = viewModel.Phone;
                studentToUpdate.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Read", "student");
        }

      
    }
}
