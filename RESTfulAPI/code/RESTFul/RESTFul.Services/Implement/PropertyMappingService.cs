using RESTFul.Data.Entities.AppDbEntities;
using RESTFul.Models.Dto;
using RESTFul.Services.Interface;
using RESTFul.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTFul.Services.Implement
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            this._propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(this._employeePropertyMapping));
            this._propertyMappings.Add(new PropertyMapping<CompanyDto, Company>(this._companyPropertyMapping));
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

        public bool ValidMappingExistsFor<TSourct, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSourct, TDestination>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldAfterSplit = fields.Split(",");
            foreach (var field in fieldAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
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
        private Dictionary<string, PropertyMappingValue> _companyPropertyMapping
            = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                        {"Id", new PropertyMappingValue(new List<string> {"Id"}) },
                        {"CompanyName", new PropertyMappingValue(new List<string>{ "Name" })},
                        {"Country", new PropertyMappingValue(new List<string>{ "Country" })},
                        {"Industry", new PropertyMappingValue(new List<string>{ "Industry"})},
                        {"Product",new PropertyMappingValue(new List<string>{ "Product"})},
                        {"Introduction",new PropertyMappingValue(new List<string>{ "Introduction"})}
            };
    }
}
