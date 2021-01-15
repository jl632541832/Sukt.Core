﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.Organization;
using Sukt.Core.Shared;
using System;

namespace Sukt.Core.Domain.Models.EntityConfigurations.Organization
{
    public class OrganizationUserConfiguration : EntityMappingConfiguration<OrganizationUserEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<OrganizationUserEntity> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("OrganizationUser");
        }
    }
}
