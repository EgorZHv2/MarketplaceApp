using WebAPi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class PasswordServiceTests
    {
        PasswordGeneratorService passwordservice = new PasswordGeneratorService();

        [Fact]
        public void PasswordServiceTest()
        {
            var result = passwordservice.GeneratePassword();

            Assert.NotNull(result);
            Assert.NotEmpty(result);

        }
    }
}
