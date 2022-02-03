namespace MediatrExample.Core.Domain
{
    public class StudentCourse: IEntity<long>
    {
        public long Id { get; private set; }
        public int StudentId { get; private set; }
        public int CourseId { get; private set; }

        public Course Course { get; private set; }
        public Student Student { get; private set; }


        //for EF
        private StudentCourse() { }
        public StudentCourse(int studentId, int courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
