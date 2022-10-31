using System.ComponentModel.DataAnnotations;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.SqlSugarHelper;

public class Entity<T>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, IsIdentity = true)]
    public virtual T Id { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者Id", IsNullable = true)]
    public virtual long? CreatedUserId { get; set; }

    /// <summary>
    /// 创建者名称
    /// </summary>
    [MaxLength(50)]
    [SugarColumn(ColumnDescription = "创建者名称", IsNullable = true)]
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间")]
    public virtual DateTime CreatedTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 修改者Id
    /// </summary>
    [SugarColumn(ColumnDescription = "修改者Id", IsNullable = true)]
    public virtual long? UpdatedUserId { get; set; }

    /// <summary>
    /// 修改者名称
    /// </summary>
    [MaxLength(50)]
    [SugarColumn(ColumnDescription = "修改者名称", IsNullable = true)]
    public virtual string UpdatedUserName { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnDescription = "更新时间", IsNullable = true)]
    public virtual DateTime? UpdatedTime { get; set; }

    /// <summary>
    /// 软删除
    /// </summary>
    [SugarColumn(ColumnDescription = "软删除")]
    public virtual bool IsDeleted { get; set; } = false;


    /// <summary>
    /// 更新信息列
    /// </summary>
    /// <returns></returns>
    public virtual string[] UpdateColumn()
    {
        var result = new[] { nameof(UpdatedUserId), nameof(UpdatedUserName), nameof(UpdatedTime) };
        return result;
    }

    /// <summary>
    /// 假删除的列，包含更新信息
    /// </summary>
    /// <returns></returns>
    public virtual string[] FalseDeleteColumn()
    {
        var updateColumn = UpdateColumn();
        var deleteColumn = new[] { nameof(IsDeleted) };
        var result = new string[updateColumn.Length + deleteColumn.Length];
        deleteColumn.CopyTo(result, 0);
        updateColumn.CopyTo(result, deleteColumn.Length);
        return result;
    }
}