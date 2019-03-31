using System;
using System.ComponentModel.DataAnnotations;
using NET_CMONO.Framework;

namespace NET_CMONO.Model
{
    public abstract class StaffKeyEntity<TKey> : NCGenericEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key]
        public TKey StaffID { get; set; }

        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="obj">要比较的实体信息</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            StaffKeyEntity<TKey> entity = obj as StaffKeyEntity<TKey>;
            if (entity == null)
            {
                return false;
            }
            return StaffID.Equals(entity.StaffID) && CreatedTime.Equals(entity.CreatedTime);
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return StaffID.GetHashCode() ^ CreatedTime.GetHashCode();
        }
    }

    /// <summary>
    /// 主键自增实体模型类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class IdentityKeyEntity<TKey> : NCGenericEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }

        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="obj">要比较的实体信息</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            IdentityKeyEntity<TKey> entity = obj as IdentityKeyEntity<TKey>;
            if (entity == null)
            {
                return false;
            }
            return Id.Equals(entity.Id) && CreatedTime.Equals(entity.CreatedTime);
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ CreatedTime.GetHashCode();
        }
    }

    /// <summary>
    /// 可持久化到数据库的数据模型基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class NCGenericEntity<TKey> : GenericEntity<TKey>
    {
        protected NCGenericEntity()
        {
            IsDeleted = false;
            CreatedTime = DateTime.Now;
        }

        #region 属性

        /// <summary>
        /// 获取或设置 是否删除，逻辑上的删除，非物理删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        public int CreatedBy { get; set; }

        /// <summary>
        /// 获取或设置 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        public int? UpdatedBy { get; set; }

        /// <summary>
        /// 获取或设置 版本控制标识，用于处理并发
        /// </summary>
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        #endregion

        #region 方法

        #endregion
    }
}