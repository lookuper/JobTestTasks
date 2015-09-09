using CommonTypes;
using CommonTypes.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularStart.Controllers
{
    public class CountryController : ApiController
    {
        private readonly IDaxxModel model = new DaxxSqlDbModel();

        public IEnumerable<CountryDto> Get()
        {
            return model.Countries;
        }
    }
}
