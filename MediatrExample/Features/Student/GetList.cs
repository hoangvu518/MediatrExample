


namespace MediatrExample.Features.Student
{
    public static class GetList
    {
        public record Query : IRequest<Result>
        {
        }
        public record Result(List<StudentDto>? Students)
        {
        }
        public record StudentDto(int Id, string FirstName, string LastName);
        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly AppDb _db;

            public Handler(AppDb db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Query query, CancellationToken token)
            {
                var students = await _db.Student.Select(x => new StudentDto(x.Id, x.FirstName, x.LastName))
                                                .ToListAsync();
                var result = new Result(students);
                return result;
            }
        }
    }

}
