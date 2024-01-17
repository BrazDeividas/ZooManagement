using AutoMapper;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using API.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _AnimalService;
        private readonly IMapper _mapper;
        public AnimalsController(IAnimalService AnimalService, IMapper mapper)
        {
            _AnimalService = AnimalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimalSendDTO>>> Get()
        {
            try
            {
                var res = await _AnimalService.GetAllWithRelatedData();
                if(res.Any())
                {
                    var resDTO = _mapper.Map<IList<AnimalSendDTO>>(res);
                    return Ok(resDTO);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalSendDTO>> Get(int id)
        {
            try
            {
                var res = await _AnimalService.GetByIdWithRelatedData(id);
                if(res != null)
                {
                    var resDTO = _mapper.Map<AnimalSendDTO>(res);
                    return Ok(resDTO);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("json")]
        public ActionResult Post([FromBody] AnimalsReceiveDTO animals)
        {
            try
            {
                _AnimalService.AddMany(animals.Animals);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] AnimalSendDTO animal)
        {
            try
            {
                _AnimalService.Add(_mapper.Map<Animal>(animal));
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] AnimalSendDTO animal)
        {
            try
            {
                _AnimalService.Update(_mapper.Map<Animal>(animal));
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _AnimalService.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}