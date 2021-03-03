﻿using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// api授权范围属性
    /// </summary>
    [DisplayName("api授权范围属性")]
    public class ApiScopeProperty : /*ApiScopePropertyBase ,*/EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public ApiScopeProperty(string key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 键
        /// </summary>
        [DisplayName("键")]
        public string Key { get; private set; }
        /// <summary>
        /// 值
        /// </summary>
        [DisplayName("值")]
        public string Value { get; private set; }
        /// <summary>
        /// 范围
        /// </summary>
        [DisplayName("范围")]
        public ApiScope Scope { get; private set; }
        #region 公共字段

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }

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
