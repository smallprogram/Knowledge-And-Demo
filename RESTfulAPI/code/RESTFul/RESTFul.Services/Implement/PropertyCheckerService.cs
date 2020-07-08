using RESTFul.Services.Interface;
using System.Reflection;

namespace RESTFul.Services.Implement
{
    /// <summary>
    /// 检测属性
    /// </summary>
    public class PropertyCheckerService : IPropertyCheckerService
    {
        /// <summary>
        /// 检测多属性以逗号分割的字符串，是否输入类型T中的属性
        /// </summary>
        /// <typeparam name="T">需要检测的类型</typeparam>
        /// <param name="fields">以逗号分割的属性名字符串</param>
        /// <returns></returns>
        public bool TypeHasProperites<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(",");
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
