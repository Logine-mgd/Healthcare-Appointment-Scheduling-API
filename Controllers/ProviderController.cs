using AutoMapper;
using MedConnect_API.DTOs;
using MedConnect_API.Models;
using MedConnect_API.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        
        unitofwork uow;
        IMapper mapper;
        RoleManager<IdentityRole> roleManager;
        UserManager<IdentityUser> userManager { get; set; }
        SignInManager<IdentityUser> sign;

        public ProviderController(unitofwork uow,IMapper _mapper, SignInManager<IdentityUser> sign,RoleManager<IdentityRole> rm, UserManager<IdentityUser> um)
        {
            this.mapper = _mapper;
            this.uow = uow;
            this.sign = sign;
            roleManager = rm;
            userManager = um;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register new provider")]
        [SwaggerResponse(201,"Provider is registered")]
        [SwaggerResponse(400,"Invalid Data")]
        public IActionResult Register(ProviderDTO pdto) 
        {
            if (ModelState.IsValid) 
            {
                Provider p = mapper.Map<Provider>(pdto); 
                IdentityResult ir = userManager.CreateAsync(p, pdto.Password).Result;
                if (ir.Succeeded)
                {
                    IdentityResult r = userManager.AddToRoleAsync(p, "provider").Result;
                    if (r.Succeeded) return Created();
                    else return BadRequest(r.Errors);
                }
                else return BadRequest(ir.Errors);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Provider log in")]
        [SwaggerResponse(201, "Provider is logged in")]
        [SwaggerResponse(400, "Invalid Data")]
        [SwaggerResponse(401,"Unauthorized Provider")]
        public IActionResult Login(Patient_LoginDTO pdto)
        {
            if (ModelState.IsValid)
            {
                var res = sign.PasswordSignInAsync(pdto.Username,pdto.Password, false, false).Result;
                if (res.Succeeded)
                {
                    var user = userManager.FindByNameAsync(pdto.Username).Result;
                    List<Claim> userdata = new List<Claim>();
                    userdata.Add(new Claim(ClaimTypes.Name, user.UserName));
                    userdata.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    var roles = userManager.GetRolesAsync(user).Result;
                    foreach (var itemRole in roles)
                    {
                        userdata.Add(new Claim(ClaimTypes.Role, itemRole));
                    }

                    string key = "Secret key Logine Magdy Secret Key";
                    var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    var signcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                         claims: userdata,
                         expires: DateTime.Now.AddDays(2),
                         signingCredentials: signcer
                         );
                    var tokenst = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenst);

                }
                else return Unauthorized("invalid username or password");
            }
            else 
                return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get provider by Id")]
        [SwaggerResponse(200, "Provider is found")]
        [SwaggerResponse(404, "There is no provider with this Id")]
        public IActionResult GetById(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            if(user != null) 
            return Ok(new { user.Id, user.UserName});
            return NotFound();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Edit provider information")]
        [SwaggerResponse(200, "Provider information is updated")]
        [SwaggerResponse(400, "Invalid Data")]
        public IActionResult Edit(EditProviderDTO pdto)
        {
            if (ModelState.IsValid)
            {
                Provider p = uow.Providers.SelectById<string>(pdto.Id);
                p.UserName = pdto.Username;
                uow.Providers.Update(p);
                uow.Providers.Save();
                return Ok();
            }else return BadRequest(ModelState);
        }

    }
}
