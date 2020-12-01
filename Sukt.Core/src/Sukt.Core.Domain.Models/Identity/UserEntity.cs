﻿using Sukt.Core.Identity;
using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DisplayName("用户信息")]
    public class UserEntity : UserBase<Guid>, IFullAuditedEntity<Guid>, ITenantEntity<Guid>
    {
        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [DisplayName("学历")]
        public string Education { get; set; }

        /// <summary>
        /// 专业技术等级
        /// </summary>
        [DisplayName("专业技术等级")]
        public string TechnicalLevel { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        public string IdCard { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public string Duties { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        public string Department { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [DisplayName("租户")]
        public Guid TenantId { get; set; }

        #region 公共字段

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid? CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public virtual DateTime LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        #endregion 公共字段
    }
}