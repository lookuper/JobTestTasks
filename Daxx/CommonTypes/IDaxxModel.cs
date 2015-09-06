using CommonTypes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public interface IDaxxModel : IDisposable
    {
        ICollection<UserDto> Users { get; }
        ICollection<CountryDto> Countries { get; }

        bool UserExists(string login);
        void AddUser(UserDto newUser);
    }
}
