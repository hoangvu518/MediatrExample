using System.Globalization;

namespace MediatrExample.Core.Exceptions
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(String.Format(CultureInfo.CurrentCulture, message), innerException)
        {
        }

        public UnauthorizedException(string message, Exception innerException, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args), innerException)
        {
        }
    }
}
