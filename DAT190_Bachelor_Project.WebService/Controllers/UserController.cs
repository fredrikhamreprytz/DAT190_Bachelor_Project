using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAT190_Bachelor_Project.Model;
using Microsoft.EntityFrameworkCore;
using DAT190_Bachelor_Project.WebService.Model;

namespace DAT190_Bachelor_Project.WebService.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext context;

        public UserController(UserContext context)
        {
            
            this.context = context;

            if (this.context.Users.Count() == 0)
            {
                this.context.Users.Add(new User());
                this.context.SaveChanges();
            }

        }
        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.context.Users.ToList();
        }

        // GET api/values/5
        [HttpGet("{email}", Name = "GetUser")]
        public IActionResult Get(string email)
        {
            
            var user = this.context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);

        }

        // POST api/values
        [HttpPost]
        public IAsyncResult Create([FromBody]User user)
        {
            
            if (user == null)
            {
                return (System.IAsyncResult)BadRequest();
            }

            this.context.Users.Add(user);
            this.context.SaveChanges();
            return (System.IAsyncResult)CreatedAtRoute("GetUser", new { email = user.Email }, user);

        }

        // PUT api/values/5
        [HttpPut("{email}")]
        public IActionResult Update(string email, [FromBody]User user)
        {
            
            if (user == null || user.Email != null)
            {
                return BadRequest();
            }

            var updateUser = this.context.Users.FirstOrDefault(u => u.Email == email);
            if (updateUser == null)
            {
                return NotFound();
            }

            updateUser.CarbonFootprint = user.CarbonFootprint;
            updateUser.ClientId = user.ClientId;
            updateUser.ClientSecret = user.ClientSecret;
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.Password = user.Password;
            updateUser.SocialSecurityNr = user.SocialSecurityNr;
            updateUser.Vehicle = user.Vehicle;

            this.context.Update(updateUser);
            this.context.SaveChanges();
            return new NoContentResult();

        }

        // DELETE api/values/5
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            
            var user = this.context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            this.context.Users.Remove(user);
            this.context.SaveChanges();
            return new NoContentResult();

        }
    }
}
