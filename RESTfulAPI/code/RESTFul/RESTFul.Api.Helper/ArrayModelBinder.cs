using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTFul.Api.Helper
{
    /// <summary>
    /// 自定义的绑定器
    /// </summary>
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // 如果需要绑定的数据类型不是IEnumerable，则绑定失败，并返回
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            // 获取需要绑定的值
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            // 如果绑定的值为null或者空格，则绑定成功返回
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // 获取第一个绑定参数类型
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            // 生成一个该类型的转换器
            var converter = TypeDescriptor.GetConverter(elementType);

            // 将需要绑定的值通过转换器转换为参数类型的值
            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray();

            // 生成一个空的绑定值类型的数组
            var typeValues = Array.CreateInstance(elementType, values.Length);

            // 将转换后的绑定类型的值复制进数组
            values.CopyTo(typeValues, 0);

            // 将该数组绑定
            bindingContext.Model = typeValues;

            // 设置绑定成功，并返回
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
