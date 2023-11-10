using GetConnectedWithSantos.Dtos;
using GetConnectedWithSantos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetConnectedWithSantos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        public ChatController(ChatService chatService)
        {
          _chatService = chatService;
        }

        [HttpPost("register-user")]
        public IActionResult RegisterUser(UserDto userDto)
        {
            if(_chatService.AddUserTolist(userDto.name))
            {
                return NoContent();
            }

            return BadRequest("This name is already taken please choose another name");

        }

    }
}
