using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Service.Utilities
{
    public static class GenericMapper
    {
        /// <summary>
        /// Maps properties from source to a new destination object of type TDestination.
        /// Properties with matching names (case-insensitive) will be copied.
        /// </summary>
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null)
                return default;

            TDestination destination = new TDestination();

            var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var destProp = destProps.FirstOrDefault(dp =>
                    dp.Name.Equals(sourceProp.Name, StringComparison.OrdinalIgnoreCase) &&
                    dp.CanWrite &&
                    dp.PropertyType.IsAssignableFrom(sourceProp.PropertyType)
                );

                if (destProp != null)
                {
                    var value = sourceProp.GetValue(source, null);
                    destProp.SetValue(destination, value, null);
                }
            }

            return destination;
        }
    }
}
