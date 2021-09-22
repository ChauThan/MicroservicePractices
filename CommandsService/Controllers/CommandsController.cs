using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCommands(int platformId)
        {
            Console.WriteLine($"--> Hit {nameof(GetCommands)}: {platformId}");

            if (!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _commandRepo.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = nameof(GetCommand))]
        public IActionResult GetCommand(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit {nameof(GetCommand)}: {platformId} / {commandId}");

            if (!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _commandRepo.GetCommand(platformId, commandId);
            if (command is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public IActionResult CreateCommand(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> Hit {nameof(CreateCommand)}: {platformId}");

            if (!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _commandRepo.CreateCommand(platformId, command);
            _commandRepo.SaveChanges();

            var dto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtAction(nameof(GetCommand),
                new 
                {
                    platformId = dto.PlatformId,
                    commandId = dto.Id
                },
                dto);
        }
    }
}