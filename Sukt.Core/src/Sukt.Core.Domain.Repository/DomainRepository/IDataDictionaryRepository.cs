﻿using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models.SystemFoundation.DataDictionary;
using Sukt.Core.Shared;
using Sukt.Core.Shared.Attributes.Dependency;
using Sukt.Core.Shared.Entity;
using System;

namespace Sukt.Core.Domain.Repository.DomainRepository
{
    public interface IDataDictionaryRepository : IEFCoreRepository<DataDictionaryEntity, Guid>
    {
    }

    [Dependency(ServiceLifetime.Scoped)]
    public class DataDictionaryRepository : BaseRepository<DataDictionaryEntity, Guid>, IDataDictionaryRepository
    {
        public DataDictionaryRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}