using System.ComponentModel;
using MentalIsland.Core.CodeFirst.Enums;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// �û���ע��Ⱥ
/// </summary>
[SugarTable("User_Islands")]
[Description("�û���ע��Ⱥ��")]
public class User_Island
{
    /// <summary>
    /// �û����
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int UserId { get; set; }

    /// <summary>
    /// ��Ⱥ���
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int IslandId { get; set; }

}
