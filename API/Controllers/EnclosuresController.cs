using AutoMapper;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using API.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureService _EnclosureService;
        private readonly IMapper _mapper;
        public EnclosuresController(IEnclosureService EnclosureService, IMapper mapper)
        {
            _EnclosureService = EnclosureService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EnclosureSendDTO>>> Get()
        {
            try
            {
                var res = await _EnclosureService.GetAllWithRelatedData();
                if(res.Any())
                {
                    var resDTO = _mapper.Map<IList<EnclosureSendDTO>>(res);
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
        public async Task<ActionResult<EnclosureSendDTO>> Get(int id)
        {
            try
            {
                var res = await _EnclosureService.GetByIdWithRelatedData(id);
                if(res != null)
                {
                    var resDTO = _mapper.Map<EnclosureSendDTO>(res);
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
        public ActionResult Post([FromBody] EnclosureReceiveDTO enclosures)
        {
            try
            {
                var mappedEnclosures = enclosures.Enclosures.Select(_mapper.Map<Enclosure>);
                _EnclosureService.AddMany(mappedEnclosures.ToList());
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] EnclosureSendDTO enclosure)
        {
            try
            {
                _EnclosureService.Add(_mapper.Map<Enclosure>(enclosure));
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Enclosure enclosure)
        {
            try
            {
                _EnclosureService.Update(_mapper.Map<Enclosure>(enclosure));
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
                _EnclosureService.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}