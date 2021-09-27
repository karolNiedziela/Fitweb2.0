using AutoMapper;
using Fitweb.Application.Commands.Trainings.Add;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping.Resolvers
{
    public class InformationResolver<TCommand, TEntity> : IValueResolver<TCommand, TEntity, Information>
        where TCommand : class
        where TEntity : class
    {
        public Information Resolve(TCommand source, TEntity destination, Information destMember, ResolutionContext context)
        {
            var name = source.GetType().GetProperty("Name").GetValue(source, null).ToString();
            var description = source.GetType().GetProperty("Description").GetValue(source, null) == null ? null : ToString();

            return Information.Create(name, description);
        }
    }
}
