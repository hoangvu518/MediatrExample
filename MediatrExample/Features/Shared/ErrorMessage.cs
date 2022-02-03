namespace MediatrExample.Features.Shared
{
    public static class ErrorMessage
    {
        public static string NotFoundId(string entityType, int id) => $"{entityType} with id {id} not found.";
        public static string NotFound(string entityType) => $"{entityType} not found.";
        public static string Unauthorized() => $"User is not authorized to perform this action.";
    }
}
