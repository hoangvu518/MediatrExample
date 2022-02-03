namespace MediatrExample.Core.Domain
{
    public class Student : IEntity<int>
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        //for EF
        private Student() { }

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentCourse = new HashSet<StudentCourse>();
        }

        public void Update(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public HashSet<StudentCourse> StudentCourse { get; private set;}

    }
}
