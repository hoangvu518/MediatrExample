

namespace MediatrExample.Features.Student
{
    public static class GetById
    {
        public record Query(int Id) : IRequest<Result>
        {
        }
        public record Result(int Id, string FirstName, string LastName)
        {
        }
        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly AppDb _db;

            public Handler(AppDb db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Query query, CancellationToken token)
            {
                var student = await _db.Student.FindAsync(query.Id);
                if (student == null)
                {
                    throw new ResourceNotFoundException(ErrorMessage.NotFound("Student", query.Id));
                }
                var result = new Result(student.Id,student.FirstName, student.LastName);
                return result;
            }
        }

    }
}
