using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_MVC_EF.Data;
using Student_CRUD_MVC_EF.Models;
using Student_CRUD_MVC_EF.Models.Entities;



namespace Student_CRUD_MVC_EF.Controllers
{
    public class StudentController : Controller
    {
      

        public StudentController(ApplicationDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            


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

            //after student is added, redirect to the read action
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
