using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using DaxxView.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DaxxView.API
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly DaxxContext context;

        public UsersController(DaxxContext dbContext)
        {
            context = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
            //var model = new DaxxSqlDbModel();
            //return model.Users.FirstOrDefault(u => u.Id == id);
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
