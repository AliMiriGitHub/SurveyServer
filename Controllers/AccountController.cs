using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SurveySystem.Models;
using SurveySystem.Models.Auth;
using SurveySystem.Models.Error;

namespace SurveySystem.Controllers
{
    [Route("/api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            var user = new ApplicationUser()
            {
                UserName = credentials.Fullname,
                Email = credentials.Email
            };
            if (credentials.Fullname.Contains("@"))
            {
                var spiltedFullname = credentials.Fullname.Split("@");
                if (spiltedFullname.Length == 2)
                {
                    var clientName = spiltedFullname[0];
                    var domainName = spiltedFullname[1];
                    var domainUser = await userManger.FindByNameAsync(domainName);
                    if (domainUser != null)
                        user.Parent = domainUser;
                }
            }
            var result = await userManger.CreateAsync(user, credentials.Password);
            if (!result.Succeeded)
                return BadRequest(new ErrorsResponse(result.Errors.Select(e => e.Description)));

            await signInManager.SignInAsync(user, isPersistent: false);
            return Ok(new AuthenticationResponse(createToken(user)));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.Fullname, credentials.Password, false, false);
            if (!result.Succeeded)
                return BadRequest(new ErrorsResponse(new List<string> { "نام کاربری یا رمز عبور اشتباه است." }));
            var user = await userManger.FindByNameAsync(credentials.Fullname);
            return Ok(new AuthenticationResponse(createToken(user)));
        }

        string createToken(ApplicationUser user)
        {
            var claims = new Claim[]{
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id)
                };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SurveySecretKey_SurveySecretKey"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}