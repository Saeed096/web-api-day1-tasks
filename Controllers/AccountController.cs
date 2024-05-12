using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task1.DTO;
using task1.Model;

namespace task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> _usermanager ,
            IConfiguration _config)
        {
            usermanager = _usermanager;
            config = _config;
        }

        [HttpPost]
        public async Task <IActionResult> register(RegisterDTO model)
        {
            if(ModelState.IsValid) 
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.name,
                    PasswordHash = model.password,
                    PhoneNumber = model.phoneNumber,
                    address = model.address,
                };

                IdentityResult result = await usermanager.CreateAsync(user , user.PasswordHash);
                if(result.Succeeded)
                    return Ok("Account created");

                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);   // implicitly can display err inside model state >> in mvc >> using tag helpers make that >> read from model satate err in span foreach input and div for the whole form
        }


        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDTO model)
        {
            if (ModelState.IsValid) 
            {
             ApplicationUser? user = await usermanager.FindByNameAsync(model.name);
                if(user != null)
                {
                 bool matched = await usermanager.CheckPasswordAsync(user , model.password);
                 if(matched)
                    {
                        // payload
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()));  // jti represent token id >> using this line >> dif tokens foreach login even the same user >> for more jwtRegisteredClaimNames >> visit IANA site

                        IList<string> roles = await usermanager.GetRolesAsync(user);
                        foreach(string role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role , role));
                        }

                        // signature "key , algorithm"
                        SymmetricSecurityKey key = new SymmetricSecurityKey(  // symetric sec key is a sec key
                            Encoding.UTF8.GetBytes("sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss"));

                        SigningCredentials signingCredentials =
                            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        // create token
                        JwtSecurityToken token = new JwtSecurityToken(    // design
                            issuer: config["JWT:validiss"],
                            audience: config["JWT:validaud"],
                            expires: DateTime.Now.AddDays(1),     // if this line and lineof new guid above are commented >> token will be the same foreach login for same user
                            claims: claims,
                            signingCredentials: signingCredentials
                            );

                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),  // create
                            valid = token.ValidTo
                        }); 
                    }
                    return BadRequest("wrong password");
                }
                return BadRequest("wrong user");
            }
            return BadRequest(ModelState);
        }
    }
}
