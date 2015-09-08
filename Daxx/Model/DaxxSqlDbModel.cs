using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes.DTO;
using System.Security;
using DataAccess;
using DataAccess.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Model
{
    public class DaxxSqlDbModel : IDaxxModel
    {
        private readonly UnitOfWork unit = new UnitOfWork();

        public DaxxSqlDbModel()
        {
            Mapper.CreateMap<Province, ProvinceDto>();
            Mapper.CreateMap<ProvinceDto, Province>();
            Mapper.CreateMap<Country, CountryDto>();
            Mapper.CreateMap<CountryDto, Country>();
            Mapper.CreateMap<User, UserDto>()
                .ForMember(x => x.Location, opt => opt.Ignore())
                .ForMember(x => x.CountryId, opt => opt.Ignore())
                .ForMember(x => x.ProvinceId, opt => opt.Ignore());
            Mapper.CreateMap<UserDto, User>()
                .ForMember(x => x.Country, opt => opt.Ignore())
                .ForMember(x => x.Province, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        public ICollection<CountryDto> Countries
        {
            get
            {
                return unit.CountryRepository
                    .GetQueryable()
                    .Project()
                    .To<CountryDto>()
                    .ToList();
            }
        }

        public ICollection<UserDto> Users
        {
            get
            {
                return unit.UserRepository
                    .GetQueryable()
                    .Project()
                    .To<UserDto>()
                    .ToList();
            }
        }

        public void AddUser(UserDto newUser)
        {
            var user = Mapper.Map<User>(newUser);
            user.Country = unit.CountryRepository.GetQueryable().First(c => c.Id == newUser.CountryId);
            user.Province = user.Country.Provinces.First(p => p.Id == newUser.ProvinceId);

            unit.UserRepository.Insert(user);
            unit.Save();
        }

        public bool UserExists(string login)
        {
            return unit.UserRepository
                .GetQueryable()
                .Any(x => login.Equals(x.Login, StringComparison.OrdinalIgnoreCase));
        }

        public void Dispose()
        {
            unit.Dispose();
            Mapper.Reset();            
        }
    }
}
