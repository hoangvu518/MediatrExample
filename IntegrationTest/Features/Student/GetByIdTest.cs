
using MediatrExample.Features.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Features.Student
{
    public class GetByIdTest:IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServer;
        private const string PATH = "api/Student";
        public GetByIdTest(TestServerFixture testServer)
        {
            _testServer = testServer;
        }

        [Fact]
        public async Task ShouldGetStudent_With200Code()
        {
            //given
            var command = new Create.Command("Tam", "La");
            var createdResponse = await _testServer.HttpClient.PostAsJsonAsync(PATH, command);
            var createdData = await createdResponse.Content.ReadFromJsonAsync<Create.Result>();
            var createdAtPath = createdResponse.Headers.Location.AbsolutePath;

            //when
            var getResponse = await _testServer.HttpClient.GetAsync(createdAtPath);
            var getData = await getResponse.Content.ReadFromJsonAsync<GetById.Result>();

            //then
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getData.Should().BeEquivalentTo(createdData);
        }


        [Fact]
        public async Task ShouldNotGetStudent_With404Code()
        {
            //given
            var unExistStudentId = 100000;

            //when
            var response = await _testServer.HttpClient.GetAsync($"{PATH}/{unExistStudentId}");
            var data = await response.Content.ReadFromJsonAsync<GetById.Result>();

            //then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
