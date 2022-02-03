namespace MediatrExample.Core.Domain
{
    public class Course: IEntity<int>
    {
        //for EF
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty ;
        private Course() { }

        public Course(string name, string description)
        {
            Name = name;
            Description = description;
            StudentCourse = new HashSet<StudentCourse>();
        }
        public HashSet<StudentCourse> StudentCourse { get; private set; }

    }
}
