using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using VNext.Extensions;

namespace VNext.Data
{
    /// <summary>
    /// <see cref="IDto"/>验证扩展 
    /// </summary>
    public static class IDtoValidateExtensions
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>> _dict
            = new ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>>();

        /// <summary>
        /// InputDto属性验证
        /// </summary>
        public static void Validate<TKey>(this IEnumerable<IDto> dtos)
        {
            IDto[] unNullDtos = dtos as IDto[] ?? dtos.ToArray();
            Check.NotNull(dtos, nameof(dtos));
            foreach (IDto dto in unNullDtos)
            {
                Validate(dto);
            }
        }

        /// <summary>
        /// IDto属性验证
        /// </summary>
        public static void Validate(this IDto dto)
        {
            Check.NotNull(dto, nameof(dto));
            Type type = dto.GetType();
            if (!_dict.TryGetValue(type, out ConcurrentDictionary<PropertyInfo, ValidationAttribute[]> dict))
            {
                PropertyInfo[] properties = type.GetProperties();
                dict = new ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>();
                if (properties.Length == 0)
                {
                    _dict[type] = dict;
                    return;
                }
                foreach (var property in properties)
                {
                    dict[property] = null;
                }
                _dict[type] = dict;
            }

            foreach (PropertyInfo property in dict.Keys)
            {
                if (!dict.TryGetValue(property, out ValidationAttribute[] attributes) || attributes == null)
                {
                    attributes = property.GetAttributes<ValidationAttribute>();
                    dict[property] = attributes;
                }
                if (attributes.Length == 0)
                {
                    continue;
                }
                object value = property.GetValue(dto);
                foreach (ValidationAttribute attribute in attributes)
                {
                    attribute.Validate(value, property.Name);
                }
            }
        }
    }
}