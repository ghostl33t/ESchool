using Microsoft.AspNetCore.Mvc;
using server.Models.DTOs.UsersDTO;
using server.Repositories.Interfaces;
using System.Security.Cryptography;

namespace UnitTestProj
{

    public class UsersUnitTest
    {
        private  server.Repositories.Interfaces.IUser fakeuserrepository { get; set; }
        private  server.Validations.IUserValidations fakeuservalidations { get; set; }
        private  server.Controllers.UserController userscontroller { get; set; }
        public UsersUnitTest()
        {
            this.fakeuserrepository = new FakeRepo.UserRepository();
            this.fakeuservalidations = new FakeValid.UserValidations();
            this.userscontroller = new server.Controllers.UserController(this.fakeuserrepository, this.fakeuservalidations);
        }
        [Fact]
        public async Task<bool> GetAllAsync_WhenCalled_ReturnsCorrectType()
        {
            //Act
            var data = await this.userscontroller.GetAllAsync() as OkObjectResult;
            Assert.IsType<List<server.Models.DTOs.UsersDTO.UsersDTO>>(data.Value);
            return true;
        }
        [Fact]
        public async Task<bool> GetAllAsync_WhenCalled_ReturnsOkResult()
        {
            var data = await this.userscontroller.GetAllAsync();
            Assert.IsType<OkObjectResult>(data as OkObjectResult);
            return true;
            
        }
        //[Fact]
        //public async Task<bool> Get_WhenCalled_ReturnsAllItems()
        //{
        //    //Act
        //    var OkResult = await GetAllAsync() as UsersDTO;
        //    var items = Assert.IsType<List<UsersDTO>>(OkResult);
        //    Assert.Equal(3, items.Count);
        //    return true;
        //}
    }
}