using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            this._propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(this._employeePropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            var propertyMappings = matchingMapping.ToList();
            if (propertyMappings.Count == 1)
            {
                return propertyMappings.First().MappingDictionary;
            }

            throw new Exception($"无法找到唯一映射关系:{typeof(TSource)},{typeof(TDestination)}");
        }


        private Dictionary<string, PropertyMappingValue> _employeePropertyMapping
            = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                        {"Id", new PropertyMappingValue(new List<string> {"Id"}) },
                        {"CompanyId", new PropertyMappingValue(new List<string>{ "CompanyId" })},
                        {"EmployeeNo", new PropertyMappingValue(new List<string>{ "EmployeeNo" })},
                        {"Name", new PropertyMappingValue(new List<string>{ "FirstName", "LastName"})},
                        {"GenderDisplay",new PropertyMappingValue(new List<string>{ "Gender"})},
                        {"Age",new PropertyMappingValue(new List<string>{ "DateOfBirth"}, true)}
            };
    }
}
