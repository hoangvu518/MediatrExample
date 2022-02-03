namespace MediatrExample.Features.Student
{
    public static class Update
    {
        public record Command(int Id, string FirstName, string LastName) : IRequest<Unit>
        {
        }
        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly AppDb _db;

            public Handler(AppDb db)
            {
                _db = db;
            }

            public async Task<Unit> Handle(Command command, CancellationToken token)
            {
                var editStudent = await _db.Student.FindAsync(command.Id);
                if (editStudent == null)
                {
                    throw new ResourceNotFoundException(ErrorMessage.NotFoundId("Student", command.Id));
                }

                editStudent.Update(command.FirstName, command.LastName);
                await _db.SaveChangesAsync(); 
                return Unit.Value;
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
