using Aws_Login.Application.InputModels;
using Aws_Login.Application.Ports;
using Aws_Login.Core.ServicesCore;
using Aws_Login.Infrastruture.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Aws_Login.Controllers
{
    [Route("")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService) => _userService = userService;
      
        [HttpPost("v1/accounts")]
        public async Task<IActionResult> CreateUser(CreateUserDto model, EmailService emailService)
        {
            await _userService.Create(model);

            emailService.Send(toName: model.Name,
                              toEmail: model.Email,
                              subject: "Welcome Test prologic.cloud",
                              body: $"Your password is {model.Password}");

            return Ok(new Notification<dynamic>(model.Password));

        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(LoginDto dto,
                                               TokenService tokenService,
                                               ApplicationDbContext context)
        {

            var user = await context.Users
                .Include(usr => usr.Roles)
                .FirstOrDefaultAsync(usr => usr.Email.Equals(dto.Email));

            if (user is null)
            {
                return StatusCode(400, new Notification<string>("Invalid username/password"));
            }
            else if(!PasswordHasher.Verify(user.PasswordHash, dto.Password))
            {
                return StatusCode(401, new Notification<string>("Invalid username/password"));
            }
                        
            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new Notification<string>(token, null));
            }
            catch
            {

                return StatusCode(500, new Notification<string>("Internal Server error!"));
            }

        }
    }
}
