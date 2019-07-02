using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Helpers;

namespace WebApi.Versioning.Configuration
{
    public class ApiVersionConfiguration
    {
        public class ApiVersion
        {
            public int Version { get; }
            public string Description { get; }

            public ApiVersion(int version, string description)
            {
                Ensure.IsPositiveInteger(version, nameof(version));
                Ensure.IsNotNullOrEmpty(description, nameof(description));

                Version = version;
                Description = description;
            }
        }

        public List<ApiVersion> Versions { get; } = new List<ApiVersion>();

        public void AdvertiseApiVersion(int version, string description)
        {
            Ensure.IsPositiveInteger(version, nameof(version));
            Ensure.IsNotNullOrEmpty(description, nameof(description));

            if (Versions.Any(x => x.Version == version))
            {
                throw new InvalidOperationException(
                    $"Api version {version} is already defined.");
            }

            Versions.Add(new ApiVersion(version, description));
        }
    }
}