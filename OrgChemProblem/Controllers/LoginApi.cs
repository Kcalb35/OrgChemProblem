using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrgChemProblem.Models;

namespace OrgChemProblem.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginApi:ControllerBase
    {
        private ILogger<LoginApi> _Logger;
        private IOptions<TokenManagement> _Token;
        private IAuthenticate _Auth;

        public LoginApi(IAuthenticate auth,ILogger<LoginApi> logger,IOptions<TokenManagement> token)
        {
            _Logger = logger;
            _Token = token;
            _Auth = auth;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginReq req)
        {
            var (b, token) = await _Auth.IsAuthenticated(req);
            if (!b)
            {
                return NotFound("User not found or password incorrect");
            }
            else
            {
                return Ok(token);
            }
        }
        
    }
}