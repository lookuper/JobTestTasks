using CommonTypes.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class DaxxSqlDbModelTests
    {
        [TestMethod()]
        public void DaxxSqlDbModelTest()
        {
            var model = new DaxxSqlDbModel();
            var users = model.Users;
            var countries = model.Countries;

            Assert.IsNotNull(users);
            Assert.IsNotNull(countries);
        }

        [TestMethod]
        public void AddUserTest()
        {
            //var newUser = new UserDto()
            //{
            //    Login = "Maksym",
            //    Password = "pass",
            //    AgreeToWorkForFood = false,
            //    Location = countries.First()
            //};

            //model.AddUser(newUser);
        }

        [TestMethod()]
        public void UserExistsTest()
        {
            var model = new DaxxSqlDbModel();
            var user = model.Users.First();
            Assert.IsTrue(model.UserExists(user.Login));
        }
    }
}