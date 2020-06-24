using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Helpers
{
    public static class ObjectExtensions
    {
        public static ExpandoObject shapeData<TSource>(this TSource source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var expandoObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)expandoObject).Add(propertyInfo.Name, propertyValue);
                }
            }
            else
            {
                var fieldsAfterSplit = fields.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo == null)
                    {
                        throw new Exception($" 没有找到：{typeof(TSource)} 上的PropertyName: {propertyName}");
                    }

                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)expandoObject).Add(propertyInfo.Name, propertyValue);
                }
            }

            return expandoObject;
        }
    }
}
