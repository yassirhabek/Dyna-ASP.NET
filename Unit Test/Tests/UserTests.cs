using Interfaces.DTO;
using Logic.Containers;
using Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Stubs;

namespace Unit_Test.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUserSuccesful()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "Test1234";
            string verpass = "Test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(1, state);
            Assert.AreEqual(username, stub.UserDTOs[stub.UserDTOs.Count - 1].UserName);
            Assert.AreEqual(email, stub.UserDTOs[stub.UserDTOs.Count - 1].Email);
            Assert.AreEqual(hashedPass, stub.UserDTOs[stub.UserDTOs.Count - 1].HashedPassword);
        }

        [TestMethod]
        public void CreateUserNullEmail()
        {
            //Arrange
            string username = "test123";
            string email = "";
            string password = "Test1234";
            string verpass = "Test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("email is required", errors[0]);
        }

        [TestMethod]
        public void CreateUserNullUsername()
        {
            //Arrange
            string username = "";
            string email = "test@hotmail.com";
            string password = "Test1234";
            string verpass = "Test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("username is required", errors[0]);
        }

        [TestMethod]
        public void CreateUserNullPassword()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "";
            string verpass = "Test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("password is required", errors[0]);
        }

        [TestMethod]
        public void CreateUserNullVerifiedPassword()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "Test1234";
            string verpass = "";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("password confirmation is required", errors[0]);
        }

        [TestMethod]
        public void CreateUserWrongEmailFormat()
        {
            //Arrange
            string username = "test123";
            string email = "test";
            string password = "Test1234";
            string verpass = "Test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("email must be valid", errors[0]);
        }

        [TestMethod]
        public void CreateUserExistingEmail()
        {
            //Arrange
            string username = "test123";
            string email = "yassirhabek@hotmail.com";
            string password = "Test1234";
            string verpass = "Test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("account already linked to this email", errors[0]);
        }

        [TestMethod]
        public void CreateUserShortPasswordFormat()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "Test1";
            string verpass = "Test1";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("password must be at least 8 characters", errors[0]);
        }

        [TestMethod]
        public void CreateUserNoCapPasswordFormat()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "test1234";
            string verpass = "test1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("password must contain at least 1 uppercase letter", errors[0]);
        }

        [TestMethod]
        public void CreateUserNoLowerPasswordFormat()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "TEST1234";
            string verpass = "TEST1234";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("password must contain at least 1 lowercase letter", errors[0]);
        }

        [TestMethod]
        public void CreateUserNoDigitPasswordFormat()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "Testtest";
            string verpass = "Testtest";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("password must contain at least 1 symbol/digit", errors[0]);
        }

        [TestMethod]
        public void CreateUserPasswordAndVerPasswordDontMatch()
        {
            //Arrange
            string username = "test123";
            string email = "test@hotmail.com";
            string password = "Test1234";
            string verpass = "Test5678";
            string hashedPass = "767b4972babfe7918b1441ccd807b77d81c1429462c12e8baccc63f0e344801e";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, username, password, verpass);
            List<string> errors = new List<string>();

            //Act
            int state = userContainer.Register(user, out errors);

            //Assert
            Assert.AreEqual(0, state);
            Assert.AreEqual("two passwords must match", errors[0]);
        }

        [TestMethod]
        public void SuccesfulLogin()
        {
            //Arrange
            string username = "xyz456";
            string email = "Test321@hotmail.com";
            string password = "Tester1421";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, password);
            List<string> errors = new List<string>();

            //Act
            bool state = userContainer.Login(user, out errors);

            //Assert
            Assert.AreEqual(true, state);
        }

        [TestMethod]
        public void LoginNullEmail()
        {
            //Arrange
            string username = "xyz456";
            string email = "";
            string password = "Tester1421";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, password);
            List<string> errors = new List<string>();

            //Act
            bool state = userContainer.Login(user, out errors);

            //Assert
            Assert.AreEqual(false, state);
            Assert.AreEqual("email is required", errors[0]);
        }

        [TestMethod]
        public void LoginNullPassword()
        {
            //Arrange
            string username = "xyz456";
            string email = "Test321@hotmail.com";
            string password = "";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, password);
            List<string> errors = new List<string>();

            //Act
            bool state = userContainer.Login(user, out errors);

            //Assert
            Assert.AreEqual(false, state);
            Assert.AreEqual("password is required", errors[0]);
        }

        [TestMethod]
        public void UnsuccesfulLogin()
        {
            //Arrange
            string username = "xyz456";
            string email = "Test321@hotmail.com";
            string password = "Tester133";
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User(email, password);
            List<string> errors = new List<string>();

            //Act
            bool state = userContainer.Login(user, out errors);

            //Assert
            Assert.AreEqual(false, state);
            Assert.AreEqual("wrong email/password", errors[0]);
        }

        [TestMethod]
        public void GetSingleUser()
        {
            //Arrange
            string username = "xyz456";
            string email = "Test321@hotmail.com";
            int userId = 2;
            UserDalStub stub = new UserDalStub();
            UserContainer userContainer = new UserContainer(stub);
            User user = new User() { Email = email };

            //Act
            User fullUser = userContainer.GetSingleUser(user);

            //Assert
            Assert.AreEqual(email, fullUser.Email);
            Assert.AreEqual(username, fullUser.UserName);
            Assert.AreEqual(userId, fullUser.UserID);
        }

        [TestMethod]
        public void ConstructorUserLogin()
        {
            //Arrange
            string email = "Test123@hotmail.com";
            string password = "Test1234";
            //Act
            User user = new User(email, password);

            //Assert
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(password, user.Password);
        }

        [TestMethod]
        public void ConstructorUserRegister()
        {
            //Arrange
            string username = "test123";
            string email = "Test123@hotmail.com";
            string password = "Test1234";
            string verPass = "Test1234";
            //Act
            User user = new User(email, username, password, verPass);

            //Assert
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(verPass, user.VerifiedPassword);
        }

        [TestMethod]
        public void ConstructorUserFull()
        {
            //Arrange
            string username = "test123";
            string email = "Test123@hotmail.com";
            string password = "Test1234";
            int uid = 34;
            //Act
            User user = new User(uid, email, username, password);

            //Assert
            Assert.AreEqual(uid, user.UserID);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(password, user.Password);
        }

        [TestMethod]
        public void ConstructorUserDTO()
        {
            //Arrange
            string username = "test123";
            string email = "Test123@hotmail.com";
            string password = "Test1234";
            string verPass = "Test1234";
            int uid = 34;
            UserDTO userDTO = new UserDTO()
            {
                UserID = uid,
                Email = email,
                UserName = username,
                HashedPassword = password,
                VerifiedPassword = verPass,
            };

            //Act
            User user = new User(userDTO);

            //Assert
            Assert.AreEqual(uid, user.UserID);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(verPass, user.VerifiedPassword);
        }
    }
}
