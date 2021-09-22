using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPlatforms(string name)
        {
            Console.WriteLine($"--> Getting Platforms from CommandsService.");
            
            var platforms = _commandRepo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpPost()]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine($"--> Inbound POST # Command Service");
            
            return Ok("Inbound POST # Command Service");
        }
    }
}