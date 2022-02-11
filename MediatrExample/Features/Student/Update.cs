namespace MediatrExample.Features.Student
{
    public static class Update
    {
        public record Command(int Id, string FirstName, string LastName) : IRequest
        {
        }
        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly AppDb _db;

            public Handler(AppDb db)
            {
                _db = db;
            }

            protected override async Task Handle(Command command, CancellationToken cancellationToken)
            {
                var editStudent = await _db.Student.FindAsync(command.Id);
                if (editStudent == null)
                {
                    throw new ResourceNotFoundException(ErrorMessage.NotFoundId("Student", command.Id));
                }

                editStudent.Update(command.FirstName, command.LastName);
                await _db.SaveChangesAsync();
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
