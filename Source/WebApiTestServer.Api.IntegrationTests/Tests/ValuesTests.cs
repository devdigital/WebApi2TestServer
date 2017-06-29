namespace WebApiTestServer.Api.IntegrationTests.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using WebApiTestServer.Api.IntegrationTests.Models;
    using WebApiTestServer.Api.Repositories;

    using Xunit;
    using Xunit.Abstractions;

    public class ValuesTests
    {
        private readonly ITestOutputHelper output;

        public ValuesTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [AutoData]
        public async Task ValuesReturnsExpectedValues(
            MyTestServerFactory testServerFactory,
            Mock<IValuesRepository> valuesRepository,
            List<int> expectedValues)
        {
            valuesRepository.Setup(r => r.GetValues()).Returns(expectedValues);

            using (var serverFactory = testServerFactory
                .With<IValuesRepository>(valuesRepository.Object)
                .WithLogging(this.output)
                .Create())
            {
                var response = await serverFactory.HttpClient.GetAsync("/api/values");
                var values = response.AsCollection<int>();
                Assert.Equal(expectedValues, values);
            }
        }
    }    
}
