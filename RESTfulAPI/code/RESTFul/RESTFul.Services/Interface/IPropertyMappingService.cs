using RESTFul.Services.Models;
using System.Collections.Generic;

namespace RESTFul.Services.Interface
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSourct, TDestination>(string fields);
    }
}
