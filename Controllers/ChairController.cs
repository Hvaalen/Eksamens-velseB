using Microsoft.AspNetCore.Mvc;
using ChairsLib;

namespace ChairController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairsController : ControllerBase
    {
        // Vi holder en reference til repositoriet
        private readonly ChairRepository _repository;

        // Constructor Injection:
        // Systemet giver os automatisk den instans, vi oprettede i Program.cs
        public ChairsController(ChairRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Chairs
        [HttpGet]
        public ActionResult<IEnumerable<Chair>> Get()
        {
            // Returnerer status 200 OK med listen af stole
            return Ok(_repository.GetAll());
        }

        // GET: api/Chairs/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> Get(int id)
        {
            Chair? chair = _repository.GetById(id);

            if (chair == null)
            {
                // Returnerer 404 hvis den ikke findes
                return NotFound($"Der findes ingen stol med id {id}");
            }

            return Ok(chair);
        }

        // POST: api/Chairs
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Chair> Post([FromBody] Chair chair)
        {
            try
            {
                // Her fanger vi exceptions fra valideringsmetoderne
                // Repository tildeler ID
                Chair createdChair = _repository.Add(chair);

                // Returnerer 201 Created og en URL til den nye stol
                return CreatedAtAction(nameof(Get), new { id = createdChair.Id }, createdChair);
            }
            catch (ArgumentException ex) // Fanger fejl fra ValidateModel/ValidateMaxWeight
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Chairs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> Delete(int id)
        {
            Chair? deletedChair = _repository.Remove(id);

            if (deletedChair == null)
            {
                return NotFound($"Kunne ikke slette id {id}, da den ikke findes.");
            }

            return Ok(deletedChair);
        }
    }
}