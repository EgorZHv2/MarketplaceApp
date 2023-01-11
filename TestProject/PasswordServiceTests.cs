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
        RandomStringGeneratorService passwordservice = new RandomStringGeneratorService();

        [Fact]
        public void PasswordServiceTest()
        {
            var result = passwordservice.Generate(10);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

        }
    }
}
