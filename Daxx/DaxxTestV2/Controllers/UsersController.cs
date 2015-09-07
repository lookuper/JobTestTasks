using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Model;
using CommonTypes;
using CommonTypes.DTO;

namespace DaxxTestV2.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IDaxxModel model = new DaxxSqlDbModel();
        
        public IHttpActionResult Get()
        {
            return this.Ok(model.Users);
        }

        public IHttpActionResult Post(UserDto newUser)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            model.AddUser(newUser);

            return this.Ok();
        }

        public IHttpActionResult Countries()
        {
            return this.Ok(model.Countries);
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
