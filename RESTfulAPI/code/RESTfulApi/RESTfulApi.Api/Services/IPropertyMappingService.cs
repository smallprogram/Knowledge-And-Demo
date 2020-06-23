using System.Collections.Generic;

namespace RESTfulApi.Api.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSourct, TDestination>(string fields);
    }
}