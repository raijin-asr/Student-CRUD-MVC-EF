using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_MVC_EF.Data;
using Student_CRUD_MVC_EF.Models;
using Student_CRUD_MVC_EF.Models.Entities;

namespace Student_CRUD_MVC_EF.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public StudentController(ApplicationDBContext dbContext)
        

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {

            try
            {
         

                var student = new Student
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Subscribed = viewModel.Subscribed,

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

        
    }
}
