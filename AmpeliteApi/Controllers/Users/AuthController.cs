using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using AmpeliteApi.Data;
using AmpeliteApi.Models;

using AmpeliteApi.Controllers.Users;

namespace AmpeliteApi.Controllers.Users
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly db_AmpeliteContext _context;


        public AuthController(db_AmpeliteContext context)
        {
            _context = context;
        }
        
        [HttpPost("SignIn")]
        public async Task<IActionResult> Authtication([FromBody] SignIn signin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<CustomerInfo> customer;
            customer = await (from p in _context.HrEmployee
                              where p.SEmpUserName == signin.UserName
                              select new CustomerInfo
                              {
                                  UserId = p.SEmpId,
                                  UserName = p.SEmpUserName,
                                  Email = p.SEmpEmail,
                                  Password = p.SEmpPassword
                              }).ToListAsync();

            if (customer.Count == 0 || customer == null)
            {
                return StatusCode(401);
            }
            else
            {
                string hash = customer[0].Password;

                using (MD5 md5Hash = MD5.Create())
                {
                    if (Auth.VerifyMd5Hash(md5Hash, signin.Password, hash))
                    {
                        var payload = new Dictionary<string, object>
                            {
                                { "userId", customer[0].UserId},
                                { "userName", customer[0].UserName },
                            };
                        var token = Auth.JwtEncoder(payload);
                        var obj = new Dictionary<string, object>
                            {
                                {"access_token", token}
                            };
                        Response.Headers.Add("Authorization", token);
                        return Ok(obj);
                    }
                    else
                    {
                        return StatusCode(401);
                    }
                }
            }
        }
    }

    public class SignIn
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public partial class CustomerInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string MacAddress { get; set; }
    }


}