using MediatrExample.Features.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Features.Student
{
    public class GetAllTest:IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServer;
        private const string PATH = "api/Student";
        public GetAllTest(TestServerFixture testServer)
        {
            _testServer = testServer;
        }

        [Fact]
        public async Task ShouldGetAllStudents_With200Code()
        {
            //given
            var command1 = new Create.Command("Tam", "La");
            var createdResponse1 = await _testServer.HttpClient.PostAsJsonAsync(PATH, command1);
            var command2 = new Create.Command("Tam", "Khoi");
            var createdResponse2 = await _testServer.HttpClient.PostAsJsonAsync(PATH, command2);

            //when
            var getAllResponse = await _testServer.HttpClient.GetAsync($"{PATH}/all");
            var getData = await getAllResponse.Content.ReadFromJsonAsync<GetList.Result>();

            //then
            getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getData.Students.Count.Should().Be(2);
        }
    }
}
