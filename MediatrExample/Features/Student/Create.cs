

namespace MediatrExample.Features.Student
{
    public static class Create
    {
        public record Command(string FirstName, string LastName) : IRequest<Result>
        {
        }
        public record Result(int Id, string FirstName, string LastName)
        {
        }
        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly AppDb _db;

            public Handler(AppDb db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Command command, CancellationToken token)
            {
                var newStudent = new Core.Domain.Student(command.FirstName, command.LastName);
                await _db.Student.AddAsync(newStudent);
                await _db.SaveChangesAsync();

                var result = new Result(newStudent.Id, newStudent.FirstName, newStudent.LastName);
                return result;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

                RuleFor(x => x.FirstName).NotEmpty()
                                         .MaximumLength(255);

                RuleFor(x => x.LastName).NotEmpty()
                                        .MaximumLength(255);
            }
        }
    }
}
