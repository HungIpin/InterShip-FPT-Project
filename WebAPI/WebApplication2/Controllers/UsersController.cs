using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.ViewModels;
using System.IO;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Counts")]
        public async Task<ActionResult> GetNumOfUsers()
        {
            var list = await _context.Users.ToListAsync();
            return Content(list.Count.ToString());
        }
        // GET: api/Users
        [HttpGet]
        [ActionName("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(a => a.Account).ToListAsync();
        }

        [HttpGet]
        [ActionName("GetUsersAsync")]
        [Route("Admin")]
        public async Task<ActionResult<UserViewModel>> GetUsersAsync()
        {
            UserViewModel userViewModels = new UserViewModel();
            userViewModels.Users = _context.Users.Include(a => a.Account).ToList();
            userViewModels.Accounts = _context.Accounts.Select(c => new Account { Id = c.Id, Username = c.Username , Password = c.Password}).ToList();
            return userViewModels;
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Where(u => u.Account.Id == id).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Exams/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, [FromForm] User user)
        {
            
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                user.Image = fileData;
            }
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("GetUsers", "Users");
                }
            }

            return await _context.Users.Where(u => u.Id == id).Include(a => a.Account).FirstAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromForm] User user)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];

                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                user.Image = fileData;
            }
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

           
            return RedirectToAction("GetUsers", "Users");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)   //Quỳnh: id của user là int chứ k phải string
        {
            var user = await _context.Users.FindAsync(id);
            var accountUser = await _context.Accounts.FindAsync(user.AccountId);
            accountUser.IsActive = false;

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}