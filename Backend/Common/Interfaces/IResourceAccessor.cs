using Common.Utilities;

namespace Common.Interfaces
{
    public interface IResourceAccessor
    {
        public ResourceInfo GetResourceInfo(Errors key);
    }
}
