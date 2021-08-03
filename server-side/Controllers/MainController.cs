using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace server_side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        public IMainRepository _mainRepository;

        public MainController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(new { queue = _mainRepository.GetCurrentQueue(), current = await _mainRepository.GetCurrentUser() });
        }

        [HttpPost("enqueue")]
        public async Task<IActionResult> Enquque([FromBody]EnqueueRequest request)
        {
            QueuePlace newQueuePlace = new QueuePlace() { Name = request.Name, EnqueuedAt = DateTime.Now };
            await _mainRepository.Enqueue(newQueuePlace);
            return Ok(new { queue = _mainRepository.GetCurrentQueue(), current = await _mainRepository.GetCurrentUser() });
        }

        [HttpPost("dequeue")]
        public async Task<IActionResult> Dequeue()
        {
            await _mainRepository.Dequeue();
            return Ok(new { queue = _mainRepository.GetCurrentQueue(), current = await _mainRepository.GetCurrentUser() });
        }
    }
}
