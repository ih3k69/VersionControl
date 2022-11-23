using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        // [Test]

        [Test]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var ténylegeseredmény = accountController.ValidateEmail(email);
            //Assert
            Assert.AreEqual(expectedResult, ténylegeseredmény);

        }
        
        
    }
}
