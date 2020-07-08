using RESTFul.Services.Interface;
using RESTFul.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RESTFul.Services.Implement
{
    /// <summary>
    /// 属性映射关系实体
    /// </summary>
    /// <typeparam name="TSource">Dto类型</typeparam>
    /// <typeparam name="TDestination">Entity类型</typeparam>
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            this.MappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
