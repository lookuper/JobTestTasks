using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonTypes;
using Model;
using CommonTypes.DTO;

namespace AngularStart.Controllers
{
    /// <summary>
    /// API controller to manage users
    /// </summary>
    public class UsersController : ApiController
    {            
        private readonly IDaxxModel model = new DaxxSqlDbModel();

        public IEnumerable<UserDto> Get()
        {
            return model.Users;
        }

        [HttpPost]
        public UserDto Post(UserDto newUser)
        {
            model.AddUser(newUser);
            return newUser;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                model.Dispose();
            }

            base.Dispose(disposing);
        }    
    }
}
