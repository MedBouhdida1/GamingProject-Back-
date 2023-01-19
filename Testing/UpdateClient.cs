using BackGaming.Controllers;
using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;


namespace Testing
{
    [TestClass]
    public class UpdateClient
    {

        
        [TestMethod]
        public async Task TestUpdateClient_Success()
        {
            // Arrange
            var dbContext = new GamingApiDbContext(); // create a new instance of the DbContext
            var controller = new ClientController(dbContext); // create a new instance of the controller
            var client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            var id = 4; // assume that this is the ID of the client we want to update

            // Act
            var result = await controller.UpdateClient(client, id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(client, ((OkObjectResult)result).Value);
        }
        [TestMethod]
        public async Task TestUpdateClient_NotFound()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            var id = 999; // assume that this is an invalid ID

            // Act
            var result = await controller.UpdateClient(client, id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual("User not found", ((NotFoundObjectResult)result).Value);
        }

        [TestMethod]
        public async Task TestUpdateClient_EmailExists()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "bouhdidamohamed22@gmail.com"
            };
            var id = 3; // assume that this is the ID of a client with a different email

            // Act
            var result = await controller.UpdateClient(client, id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            Assert.AreEqual("Email exist", ((UnauthorizedObjectResult)result).Value);
        }


    }
}