using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VNext.Entity
{
    public class ColumnMapper
    {
        public static void SetMapper<T>()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(T), new ColumnAttributeTypeMapper(typeof(T)));
        }

        public static void SetMapper(Type type)
        {
            Dapper.SqlMapper.SetTypeMap(type, new ColumnAttributeTypeMapper(type));
        }
    }

    public abstract class CustomTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public CustomTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            _mappers = mappers;
        }


        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    ConstructorInfo result = mapper.FindConstructor(names, types);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }
            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            return _mappers
                .Select(mapper => mapper.FindExplicitConstructor())
                .FirstOrDefault(result => result != null);
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var result = mapper.GetConstructorParameter(constructor, columnName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }
            return null;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var result = mapper.GetMember(columnName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }
            return null;
        }
    }

    public class ColumnAttributeTypeMapper : CustomTypeMapper
    {
        public ColumnAttributeTypeMapper(Type type)
            : base(new SqlMapper.ITypeMap[]
                {
                    new CustomPropertyTypeMap(
                       type,
                       (type, columnName) =>
                           type.GetProperties().FirstOrDefault(prop =>
                                prop.GetCustomAttributes(false)
                                   .OfType<ColumnAttribute>()
                           .Any(attr => string.Equals(attr.Name,columnName,StringComparison.OrdinalIgnoreCase)))
                       ),
                    new DefaultTypeMap(type)
                })
        { }
    }
}