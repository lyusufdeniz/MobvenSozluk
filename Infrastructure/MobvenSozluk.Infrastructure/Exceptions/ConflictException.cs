using System.Net;

namespace MobvenSozluk.Infrastructure.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message) { }
    }
}
