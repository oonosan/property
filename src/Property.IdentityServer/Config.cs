using IdentityServer4.Models;
using System.Collections.Generic;

namespace Property.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("APIProperty", "Property API")
            };
        }
    }
}
