using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private IPlatformRepo _platformRepo;
        private IMapper _mapper;
        private ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformController(
            IPlatformRepo platformRepo, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
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
        public async Task<IActionResult> Create(PlatformCreateDto platform)
        {
            var model = _mapper.Map<Platform>(platform);
            _platformRepo.Create(model);
            _platformRepo.SaveChanges();

            var dto = _mapper.Map<PlatformReadDto>(model);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Cound not send synchronously: {ex.Message}");
            }

            // Send Async Message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(dto);
                platformPublishedDto.Event = "Platform_Published";

                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Cound not send asynchronously: {ex.Message}");
            }


            return CreatedAtAction(nameof(GetById), new {id = model.Id}, dto);
        }
    }
}