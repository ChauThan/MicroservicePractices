using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private IPlatformRepo _platformRepo;
        private IMapper _mapper;

        public PlatformController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_platformRepo.GetAll()));
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public IActionResult GetById(int id)
        {
            var platform = _platformRepo.GetById(id);
            if(platform is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformReadDto>(_platformRepo.GetById(id)));
        }

        [HttpPost]
        public IActionResult Create(PlatformCreateDto platform)
        {
            var model = _mapper.Map<Platform>(platform);
            _platformRepo.Create(model);
            _platformRepo.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = model.Id}, _mapper.Map<PlatformReadDto>(model));
        }
    }
}