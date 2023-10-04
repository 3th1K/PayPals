using Common.Utilities;

namespace Common.Interfaces
{
    public interface IErrorBuilder
    {
        public Error BuildError(Exception exception, List<string>? errors = null);
        public Error BuildError(Exception exception, string details, List<string>? errors = null);
        public Error BuildError(ErrorType error, List<string>? errors = null);
        public Error BuildError(ErrorType error, string details, List<string>? errors = null);
    }
}
