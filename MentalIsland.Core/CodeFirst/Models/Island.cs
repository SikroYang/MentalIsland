using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ��Ⱥ
/// </summary>
[SugarTable("Islands")]
[Description("��Ⱥ��")]
public class Island : Entity
{
    /// <summary>
    /// ��Ⱥ����
    /// </summary>
    public string Name { get; set; } = default!;

}
