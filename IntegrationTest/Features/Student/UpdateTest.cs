using MediatrExample.Features.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Features.Student
{
    public class UpdateTest: IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServer;
        private const string PATH = "api/Student";
        public UpdateTest(TestServerFixture testServer)
        {
            _testServer = testServer;
        }

        [Fact]
        public async Task ShouldUpdateStudent_With204Code()
        {
            //given
            var command = new Create.Command("Tam", "La");
            var createdResponse = await _testServer.HttpClient.PostAsJsonAsync(PATH, command);
            var createdData = await createdResponse.Content.ReadFromJsonAsync<Create.Result>();

            //when
            var modifiedData = new Update.Command(createdData.Id, "Hoang", "Pham");
            var updateResponse = await _testServer.HttpClient.PutAsJsonAsync(PATH, modifiedData);

            //then
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
