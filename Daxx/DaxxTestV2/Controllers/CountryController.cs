using CommonTypes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaxxTestV2.Controllers
{
    public class CountryController : ApiController
    {
        private readonly IDaxxModel model = new DaxxSqlDbModel();

        public IHttpActionResult Get()
        {
            return this.Ok(model.Countries);
        }
    }
}
