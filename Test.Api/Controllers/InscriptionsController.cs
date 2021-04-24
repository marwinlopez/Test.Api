using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test.Api.Common;
using Test.Application.Students;
using Test.Core.Students;
using Test.EntityFramework;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscriptionsController : ControllerBase
    {

        private readonly TestDbContext _context;

        public InscriptionsController(TestDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Consultar todas las solicitudes enviadas.
        /// </summary>
        // GET: api/<InscriptionsController>
        [HttpGet]
        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        /// <summary>
        /// Verificar Solicitud por Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>student</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetTodoItem(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        /// <summary>
        /// Enviar Solicitud de Ingreso.
        /// </summary>
        /// <param name="studentDTO">studentDTO</param>
        /// <returns>A newly created CreateStudent</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        // POST api/<InscriptionsController>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentDto studentDTO)
        {
            int studyHouseId = await IsValideteHouseAsync(studentDTO.StudyHouseId);
            if (studyHouseId == 0)
            {
                ModelState.AddModelError("StudyHouseId", "the house that aspires to belong does not exist");
                return BadRequest(ModelState);
            }

            if (studentDTO.Age.ToString().Length > 2)
            {
                ModelState.AddModelError("Age", "Max 2 digits");
                return BadRequest(ModelState);
            }
            var student = new Student
            {
                Name = studentDTO.Name,
                LastName = studentDTO.LastName,
                Identification = studentDTO.Identification,
                Age = studentDTO.Age,
                StudyHouseId= studyHouseId
            };


            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok("Student created");
        }

        /// <summary>
        /// Consultar todas las solicitudes enviadas.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student">student</param>
        /// <returns></returns>
        // PUT api/<InscriptionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, Student student)
        {
            if (student == null)
            {
                return NotFound();
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            int studyHouseId = await IsValideteHouseAsync(student.StudyHouseId);
            if (studyHouseId == 0)
            {
                ModelState.AddModelError("StudyHouseId", "the house that aspires to belong does not exist");
                return BadRequest(ModelState);
            }

            if (student.Age.ToString().Length > 2)
            {
                ModelState.AddModelError("Age", "Max 2 digits");
                return BadRequest(ModelState);
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();
            }

            return Ok(new ResponseApi<Student>(HttpStatusCode.OK, student));
        }

        /// <summary>
        /// Eliminar la solicitud de ingreso.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<InscriptionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new ResponseApi<Student>(HttpStatusCode.OK, student));
        }

        #region Validate StudyHouse

        private async Task<int> IsValideteHouseAsync(int house)
        {
            var studyHouse = await _context.StudyHouses.FindAsync(house);
            if (studyHouse != null)
            {
                return studyHouse.Id;
            }
            return 0;
        }

        #endregion
    }
}
