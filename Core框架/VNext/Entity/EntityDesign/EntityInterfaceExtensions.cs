using System;
using System.Linq;
using System.Security.Principal;
using VNext.Data;
using VNext.Extensions;
using VNext.Identity;

namespace VNext.Entity
{
    /// <summary>
    /// 实体接口扩展方法
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 检测指定类型是否为<see cref="IEntity{TKey}"/>实体类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsEntityType(this Type type)
        {
            Check.NotNull(type, nameof(type));
            return typeof(IEntity<>).IsGenericAssignableFrom(type) && !type.IsAbstract && !type.IsInterface;
        }

        /// <summary>
        /// 判断指定实体是否已过期
        /// </summary>
        /// <param name="entity">要检测的实体</param>
        /// <returns></returns>
        public static bool IsExpired(this IExpirable entity)
        {
            Check.NotNull(entity, nameof(entity));
            DateTime now = DateTime.Now;
            return entity.BeginTime != null && entity.BeginTime.Value > now || entity.EndTime != null && entity.EndTime.Value < now;
        }

        /// <summary>
        /// 检测并执行<see cref="ICreatedTime"/>接口的逻辑
        /// </summary>
        public static TEntity CheckICreatedTime<TEntity, TKey>(this TEntity entity)
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!(entity is ICreatedTime))
            {
                return entity;
            }
            ICreatedTime entity1 = (ICreatedTime)entity;
            if (entity1.CreatedTime == default(DateTime))
            {
                entity1.CreatedTime = DateTime.Now;
            }
            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="ICreationAudited{TUserKey}"/>接口的处理
        /// </summary>
        public static TEntity CheckICreationAudited<TEntity, TKey, TUserKey>(this TEntity entity, IPrincipal user)
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TUserKey : struct, IEquatable<TUserKey>
        {
            if (!(entity is ICreationAudited<TUserKey>))
            {
                return entity;
            }

            ICreationAudited<TUserKey> entity1 = (ICreationAudited<TUserKey>)entity;
            entity1.CreatorId = user.Identity.IsAuthenticated ? (TUserKey?)user.Identity.GetUserId<TUserKey>() : null;
            entity1.CreatedTime = DateTime.Now;
            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="IUpdateAudited{TUserKey}"/>接口的处理
        /// </summary>
        public static TEntity CheckIUpdateAudited<TEntity, TKey, TUserKey>(this TEntity entity, IPrincipal user)
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TUserKey : struct, IEquatable<TUserKey>
        {
            if (!(entity is IUpdateAudited<TUserKey>))
            {
                return entity;
            }

            IUpdateAudited<TUserKey> entity1 = (IUpdateAudited<TUserKey>)entity;
            entity1.LastUpdaterId = user.Identity.IsAuthenticated ? (TUserKey?)user.Identity.GetUserId<TUserKey>() : null;
            entity1.LastUpdatedTime = DateTime.Now;
            return (TEntity)entity1;
        }

        /// <summary>
        /// 生成当前实体测试对象
        /// </summary>
        /// <param name="index">生成当前实体测试对象</param>
        /// <returns></returns>
        public static TEntity GenerateEntity<TEntity>(int index)
            where TEntity : new()
        {
            var entity = new TEntity();
            var properties = entity.GetType().GetProperties();
            properties.ToList().ForEach(a =>
            {
                if (a.Name == "Id") return;
                var dataType = a.PropertyType.GetNonNullableType();
                if (dataType == typeof(DateTime))
                {
                    a.SetValue(entity, DateTime.Now);
                }
                else if (dataType == typeof(int))
                {
                    a.SetValue(entity, new Random().Next(0, 1000));
                }
                else if (dataType == typeof(decimal) || dataType == typeof(double))
                {
                    a.SetValue(entity, new Random().NextDouble());
                }
                else if (a.Name.Contains("Time") || a.Name.Contains("time"))
                {
                    a.SetValue(entity, $"{DateTime.Now.AddDays(-index).ToStringYMDHMS()}");
                }
                else
                {

                    a.SetValue(entity, $"{a.Name}_{index}");
                }
            });
            return entity;
        }
    }
}