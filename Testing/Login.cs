using BackGaming.Controllers;
using BackGaming.Data;
using BackGaming.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class Login
    {


        [TestMethod]
        public void TestLoginCoach_Success()
        {
           
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                Email = "ahmed@gmail.com",
                Password = "root"
            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var anonymousResult = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType(anonymousResult, typeof(object));
            var token = anonymousResult.GetType().GetProperty("token").GetValue(anonymousResult);
            Assert.IsInstanceOfType(token, typeof(string));
        }

        [TestMethod]
        public void TestLoginCoach_WrongPassword()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                Email = "ahmed@gmail.com",
                Password = "11561812564" //Wrong Password
            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            Assert.AreEqual("Wrong password Coach", ((UnauthorizedObjectResult)result).Value);
        }

        [TestMethod]
        public void TestLoginClient_Success()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                Email = "karim@gmail.com",
                Password = "root"
            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var anonymousResult = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType(anonymousResult, typeof(object));
            var token = anonymousResult.GetType().GetProperty("token").GetValue(anonymousResult);
            Assert.IsInstanceOfType(token, typeof(string));
        }

        [TestMethod]
        public void TestLoginClient_WrongPassword()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                Email = "karim@gmail.com",
                Password = "5948198189484" //Wrong Password
            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            Assert.AreEqual("Wrong password Client", ((UnauthorizedObjectResult)result).Value);
        }




        [TestMethod]
        public void TestLogin_UserNotFound()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                //Wrong data
                Email = "nonexistent@example.com",
                Password = "password"
            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual("user not found", ((NotFoundObjectResult)result).Value);

        }

        [TestMethod]
        public void TestLogin_EmptyEmail()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                //empty email
                Email = "",
                Password = "sddddsf"


            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));


        }
        [TestMethod]
        public void TestLogin_EmptyPassword()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                //empty password
                Email = "karim@gmail.com",
                Password = ""

            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));


        }
        [TestMethod]
        public void TestLogin_EmptyPasswordAndEmail()
        {
            // Arrange
            var dbContext = new GamingApiDbContext();
            var controller = new ClientController(dbContext);
            var client = new Client
            {
                //empty password and email
                Email = "",
                Password = ""
            };

            // Act
            var result = controller.Login(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual("user not found", ((NotFoundObjectResult)result).Value);

        }
    }
}
