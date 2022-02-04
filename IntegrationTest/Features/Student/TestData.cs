using MediatrExample.Features.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Features.Student
{
    public class TestData
    {
        public static TheoryData<Create.Command> BadCreateNewStudentData =>
            new TheoryData<Create.Command>
            {
                    new Create.Command("", "Pham"),
                    new Create.Command("Hoang", "")
            };
    }
}
