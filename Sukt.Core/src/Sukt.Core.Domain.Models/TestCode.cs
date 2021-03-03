

//------------------------------------------------------------------------------
//<auto-generated>
//此代码由代码生成器生成。
//手动更改此文件可能导致应用程序出现意外的行为。
//如果重新生成代码，将覆盖对此文件的手动更改。
//</auto-generated>
//<copyright file="TestCode.cs" company="XXXX开车公司">
//王莫某
//</copyright>
//<site>http://admin.destinycore.club</site>
//<last-editor>王莫某</last-editor>
//<last-date>2020/09/21 14:23</last-date>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Sukt.Core.Shared.Entity;

namespace Sukt.Core.Domain.Models.Entities
{

    /// <summary>
    ///代码生成
    /// </summary>
    [DisplayName("代码生成")]
    public partial class TestCode : EntityBase<Guid>
, ICreatedAudited<Guid>, IModifyAudited<Guid>
    {

        [DisplayName("名字")]
        /// <summary>
        /// 获取或设置 名字
        /// </summary>
        public string Name { get; set; }

        [DisplayName("名字1")]
        /// <summary>
        /// 获取或设置 名字1
        /// </summary>
        public string Name1 { get; set; }

        [DisplayName("价格")]
        /// <summary>
        /// 获取或设置 价格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 获取或设置创建用户ID
        /// </summary>
        [DisplayName("创建用户ID")]
        public Guid CreatedId { get; set; }
        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 获取或设置最后修改用户
        /// </summary>
        [DisplayName("最后修改用户")]
        public Guid? LastModifyId { get; set; }
        /// <summary>
        /// 获取或设置最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTime LastModifedAt { get; set; }

    }
}