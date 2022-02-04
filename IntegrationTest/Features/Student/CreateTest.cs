using IntegrationTest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatrExample.Features.Student;


namespace IntegrationTest.Features.Student
{
    public class CreateTest: IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServer;
        private const string PATH = "api/Student";
        public CreateTest(TestServerFixture testServer)
        {
            _testServer = testServer;
        }

        [Fact]
        public async Task ShouldCreateNewStudent_With201Code()
        {
            //given
            var command = new Create.Command("Hoang", "Pham");

            //when
            var response = await _testServer.HttpClient.PostAsJsonAsync(PATH, command);
            var data = await response.Content.ReadFromJsonAsync<Create.Result>();

            //then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            data.FirstName.Should().Be("Hoang");
            data.LastName.Should().Be("Pham");
        }

        [Theory]
        [MemberData(nameof(TestData.BadCreateNewStudentData), MemberType = typeof(TestData))]
        public async Task ShouldNotCreateNewStudent_With400Code(Create.Command command)
        {
            //given

            //when
            var response = await _testServer.HttpClient.PostAsJsonAsync(PATH, command);

            //then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }



}
