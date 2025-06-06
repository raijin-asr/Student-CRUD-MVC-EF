using Microsoft.AspNetCore.Http;

namespace Student_CRUD_MVC_EF.Models
{
    public class AddStudentViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Subscribed { get; set; }
        public IFormFile? Photo { get; set; } 

    }
}
