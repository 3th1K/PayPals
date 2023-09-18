using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class ResourceInfo
    {
        public string Name { get; set; } = string.Empty;
        public int ValueCode { get; set; }
        public string ValueMessage { get; set; } = string.Empty;
        public string ValueDescription { get; set; } = string.Empty;
        public string ValueSolution { get; set; } = string.Empty;
    }

    public class ResourceAccessor : IResourceAccessor
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("Common.Resource", Assembly.GetExecutingAssembly());

        public ResourceInfo GetResourceInfo(Errors key)
        {
            try
            {
                var value = _resourceManager.GetString(key.ToString()).Split(":");
                var resourceInfo = new ResourceInfo
                {
                    Name = key.ToString(),
                    ValueCode = Convert.ToInt32(value[0]),
                    ValueMessage = value[1],
                    ValueDescription = _resourceManager.GetString($"{value[0]}_DESCRIPTION") ?? string.Empty,
                    ValueSolution = _resourceManager.GetString($"{value[0]}_SOLUTION") ?? string.Empty,
                };
                return resourceInfo;
            }
            catch (MissingManifestResourceException)
            {
                return new ResourceInfo
                {
                    Name = key.ToString(),
                    ValueMessage = $"[Resource Key Missing: {key}]"
                };
            }
        }


    }

}
