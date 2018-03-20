using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BDSA2017.Assignment07.Models;

namespace BDSA2017.Assignment07.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TracksController : Controller
    {
        private readonly ITrackRepository _repository;

        public TracksController(ITrackRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Tracks
        [HttpGet]
        public IEnumerable<TrackDTO> Get()
        {
            return _repository.Read().ToList();
        }

        // GET: api/Tracks/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var track = await _repository.Find(id);

            if (track == null) return NotFound();

            return Ok(track);
        }

        // POST: api/Tracks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TrackCreateDTO trackCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _repository.Create(trackCreateDTO);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }
        
        // PUT: api/Tracks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TrackUpdateDTO trackUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            trackUpdateDTO.Id = id;

            var update = await _repository.Update(trackUpdateDTO);
            if (!update) { return NotFound(); }
            return Ok(update);

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var track = await _repository.Find(id);

            if (track == null) return NotFound();

            var update = await _repository.Delete(track.Id);

            if (!update) { return NotFound(); }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
