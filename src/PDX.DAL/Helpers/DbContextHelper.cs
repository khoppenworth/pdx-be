using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PDX.DAL.Query;
using PDX.Domain;
using PDX.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using Newtonsoft.Json;

namespace PDX.DAL.Helpers
{
    public static class DbContextHelper
    {
        public static IList<Type> GetAllDbSetTypes(this DbContext context)
        {
            var dbSetTypes = new List<Type>();
            var properties = context.GetType().GetProperties();

            foreach (var property in properties)
            {
                var dbSetType = property.PropertyType;

                var isDbSet = dbSetType.IsConstructedGenericType && typeof(DbSet<>).IsAssignableFrom(dbSetType.GetGenericTypeDefinition());

                if (isDbSet)
                {
                    //extract entity type here
                    dbSetTypes.Add(dbSetType.GetGenericArguments().First());
                }
            }

            return dbSetTypes;

        }

        public static Includes<T> GetNavigations<T>() where T : BaseEntity
        {
            var type = typeof(T);
            var navigationProperties = new List<string>();

            //get navigation properties
            GetNavigationProperties(type, type, string.Empty, navigationProperties);

            Includes<T> includes = new Includes<T>(query =>
            {
                return navigationProperties.Aggregate(query, (current, inc) => current.Include(inc));
            });

            return includes;
        }

        public static T NullifyForeignKeys<T>(this T t) where T : class
        {
            return t.NullifyProperty(typeof(ForeignKeyAttribute));
        }

        public static void NullifyForeignKeys<T>(this IEnumerable<T> ts) where T : class
        {
            foreach (var t in ts)
            {
                t.NullifyForeignKeys();
            }
        }
        public static T NullifyPrimaryKeys<T>(this T t) where T : class
        {
            return t.NullifyProperty(typeof(KeyAttribute));
        }

        public static T NullifyProperty<T>(this T t, System.Type attributeType) where T : class
        {
            var tCopy = t;

            //get foreign key properties
            var properties = tCopy.GetType().GetProperties();
            var keyPropertyInfoList = properties.Where(prop => prop.IsDefined(attributeType));

            foreach (var prop in keyPropertyInfoList)
            {
                prop.SetValue(tCopy, null, null);
            }

            var collectionPropertyInfoList = properties.Where(prop => prop.PropertyType.IsConstructedGenericType
                                                                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));

            foreach (var prop in collectionPropertyInfoList)
            {
                ICollection collection = prop.GetValue(tCopy, null) as ICollection;
                if (collection != null)
                {
                    foreach (var col in collection)
                    {
                        col.NullifyProperty(attributeType);
                    }
                    prop.SetValue(tCopy, collection, null);
                }
            }

            return tCopy;
        }

        public static T NullifyProperty<T>(this T t, string propertyName) where T : class
        {
            var tCopy = t;

            //get foreign key properties
            var properties = tCopy.GetType().GetProperties();
            var keyPropertyInfoList = properties.Where(prop => prop.Name == propertyName);

            foreach (var prop in keyPropertyInfoList)
            {
                prop.SetValue(tCopy, null, null);
            }

            var collectionPropertyInfoList = properties.Where(prop => prop.PropertyType.IsConstructedGenericType
                                                                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));

            foreach (var prop in collectionPropertyInfoList)
            {
                ICollection collection = prop.GetValue(tCopy, null) as ICollection;
                if (collection != null)
                {
                    foreach (var col in collection)
                    {
                        col.NullifyProperty(propertyName);
                    }
                    prop.SetValue(tCopy, collection, null);
                }
            }

            return tCopy;
        }

        private static void GetNavigationProperties(Type baseType, Type type, string parentPropertyName, IList<string> accumulator)
        {
            //get navigation properties
            var properties = type.GetProperties();
            var navigationPropertyInfoList = properties.Where(prop => prop.IsDefined(typeof(NavigationPropertyAttribute)));

            foreach (PropertyInfo prop in navigationPropertyInfoList)
            {
                var propertyType = prop.PropertyType;
                var elementType = propertyType.GetTypeInfo().IsGenericType ? propertyType.GetGenericArguments()[0] : propertyType;

                //Prepare navigation property in {parentPropertyName}.{propertyName} format and push into accumulator
                var properyName = string.Format("{0}{1}{2}", parentPropertyName, string.IsNullOrEmpty(parentPropertyName) ? string.Empty : ".", prop.Name);
                accumulator.Add(properyName);

                //Skip recursion of propert has JsonIgnore attribute or current property type is the same as baseType
                var isJsonIgnored = prop.IsDefined(typeof(JsonIgnoreAttribute));
                if (!isJsonIgnored && elementType != baseType)
                {
                    GetNavigationProperties(baseType, elementType, properyName, accumulator);
                }
            }
        }

    }
}