using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace NZ_Walks.API.Controllers
{
    //https://localhost:portnumber/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult getAllStudents() {
            string[] studentNames = new string[] { "Hello", "ahmed" , "ahsan"};
             return Ok(studentNames);
         }

     






    }
}
