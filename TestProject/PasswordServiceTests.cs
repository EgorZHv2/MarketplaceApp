using WebAPi.Services;

namespace TestProject
{
    public class PasswordServiceTests
    {
        private RandomStringGeneratorService passwordservice = new RandomStringGeneratorService();

        [Fact]
        public void PasswordServiceTest()
        {
            var result = passwordservice.Generate(10);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}