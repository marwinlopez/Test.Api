using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test.Api.Common;
using Test.Application.StudyHouses;
using Test.Core.StudyHouses;
using Test.EntityFramework;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyHouseController : ControllerBase
    {
        private readonly TestDbContext _context;

        public StudyHouseController(TestDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Consultar todas las Casas disponibles.
        /// </summary>
        // GET: api/<InscriptionsController>
        [HttpGet]
        public IEnumerable<StudyHouse> GetAll()
        {
            return _context.StudyHouses.ToList();
        }

        /// <summary>
        /// Verificar casa por Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>student</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudyHouse>> GetTodoItem(int id)
        {
            var studyHouse = await _context.StudyHouses.FindAsync(id);

            if (studyHouse == null)
            {
                return NotFound();
            }

            return studyHouse;
        }
        /// <summary>
        /// Enviar Solicitud de Ingreso de nueva casas.
        /// </summary>
        /// <param name="studyHouseDTO"></param>
        /// <returns>A newly created CreateStudent</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        // POST api/<InscriptionsController>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudyHousesDto>> CreateStudent(StudyHousesDto studyHouseDTO)
        {

            var studyHouse = new StudyHouse
            {
                Name = studyHouseDTO.Name,
                CreateDate = DateTime.UtcNow
            };


            _context.StudyHouses.Add(studyHouse);
            await _context.SaveChangesAsync();

            return Ok("Student created");
        }

        /// <summary>
        /// Actualizar.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studyHouse">studyHouse</param>
        /// <returns></returns>
        // PUT api/<InscriptionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, StudyHouse studyHouse)
        {
            if (studyHouse == null)
            {
                return NotFound();
            }

            if (id != studyHouse.Id)
            {
                return BadRequest();
            }


            _context.Entry(studyHouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();
            }

            return Ok(new ResponseApi<StudyHouse>(HttpStatusCode.OK, studyHouse));
        }

        /// <summary>
        /// Eliminar la una casa de estudio.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<InscriptionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var studyHouse = await _context.StudyHouses.FindAsync(id);

            if (studyHouse == null)
            {
                return NotFound();
            }

            _context.StudyHouses.Remove(studyHouse);
            await _context.SaveChangesAsync();

            return Ok(new ResponseApi<StudyHouse>(HttpStatusCode.OK, studyHouse));
        }

    }
}

