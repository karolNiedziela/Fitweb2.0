using AutoMapper;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping.Converters
{
    public class InformationConverter<T> : ITypeConverter<T, Information> where T : class
    {
        public Information Convert(T source, Information destination, ResolutionContext context)
        {
            var properties = new List<PropertyInfo>();

            foreach (var property in typeof(T).GetProperties())
            {
                properties.Add(property);
            }

            return Information.Create("test", "test");
        }
    }
}
