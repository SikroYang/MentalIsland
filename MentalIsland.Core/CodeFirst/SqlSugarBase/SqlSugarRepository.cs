using Furion;
using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using Snowflake.Core;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.SqlSugarBase;
/// <summary>
/// SqlSugar仓储类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SqlSugarRepository<T> : SimpleClient<T> where T : class, new()
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SqlSugarRepository(ISqlSugarClient? context = null) : base(context)
    {
        base.Context = App.GetService<ISqlSugarClient>();
    }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public async Task<bool> RecycleByIdAsync<TEntity>(int Id) where TEntity : Entity, new()
    {
        var entity = new { Id = Id, IsDeleted = true };
        var isSuccess = await Context.Updateable<TEntity>(entity).IgnoreColumns(true)
            .UpdateColumns(x => new { x.IsDeleted, x.UpdatedTime, x.UpdatedUserId, x.UpdatedUserName })  // 允许更新的字段-AOP拦截自动设置UpdateTime、UpdateUserId
            .ExecuteCommandHasChangeAsync();
        return isSuccess;
    }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    public async Task<bool> RecycleIslandAsync(int Id)
    {
        var entity = new { Id = Id, IsDeleted = true };
        var lastName = new IdWorker(1, 1).NextId();
        var isSuccess = await Context.Updateable<Island>(entity).IgnoreColumns(true)  // 允许更新的字段-AOP拦截自动设置UpdateTime、UpdateUserId
            .SetColumns(x => x.Name == x.Name + lastName)
            .Where(x => x.Id == Id)
            .ExecuteCommandHasChangeAsync();
        return isSuccess;
    }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public async Task<bool> RecycleEntityAsync<TEntity>(TEntity entity) where TEntity : Entity, new()
    {
        entity.IsDeleted = true;
        var isSuccess = await Context.Updateable(entity).IgnoreColumns(true)
            .UpdateColumns(x => new { x.IsDeleted, x.UpdatedTime, x.UpdatedUserId, x.UpdatedUserName })  // 允许更新的字段-AOP拦截自动设置UpdateTime、UpdateUserId
            .ExecuteCommandHasChangeAsync();
        return isSuccess;
    }
}