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
    /// <summary>
    /// 属性映射服务，主要用于数据排序时DTO与Entity实体的属性对应关系。
    /// </summary>
    public class PropertyMappingService : IPropertyMappingService
    {
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            this._propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(this._employeePropertyMapping));
            this._propertyMappings.Add(new PropertyMapping<CompanyDto, Company>(this._companyPropertyMapping));
        }
        /// <summary>
        /// 获取DTO与Entity的字段对应关系的Dictionary
        /// </summary>
        /// <typeparam name="TSource">Dto类型</typeparam>
        /// <typeparam name="TDestination">Entity类型</typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// 验证DTO与Entity对应关系中是否包含传入多字段字符串
        /// </summary>
        /// <typeparam name="TSourct">DTO类型</typeparam>
        /// <typeparam name="TDestination">Entity类型</typeparam>
        /// <param name="fields">以逗号分割的字段名字符串</param>
        /// <returns></returns>
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

        #region Dto与Entity属性对应关系
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
        #endregion
    }
}
