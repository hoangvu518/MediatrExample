using System.Globalization;

namespace MediatrExample.Core.Exceptions
{
    public class ResourceNotFoundException: Exception
    {
        public ResourceNotFoundException() : base() { }

        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
      
        public ResourceNotFoundException(string message, Exception innerException)
            : base(String.Format(CultureInfo.CurrentCulture, message), innerException) 
        {
        }

        public ResourceNotFoundException(string message, Exception innerException, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args), innerException)
        {
        }
    }
}
