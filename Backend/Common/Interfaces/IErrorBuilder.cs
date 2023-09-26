using Common.Utilities;

namespace Common.Interfaces
{
    public interface IErrorBuilder
    {
        public Error BuildError(Exception exception, List<string>? errors = null);
        public Error BuildError(Exception exception, string details, List<string>? errors = null);
        public Error BuildError(Errors error, List<string>? errors = null);
        public Error BuildError(Errors error, string details, List<string>? errors = null);
    }
}
