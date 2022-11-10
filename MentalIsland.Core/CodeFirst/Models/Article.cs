using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ��������
/// </summary>
[SugarTable("Articles")]
[Description("�������±�")]
public class Article : Entity
{
    /// <summary>
    /// ����
    /// </summary>
    public string Title { get; set; } = default!;
    /// <summary>
    /// ����
    /// </summary>
    public string Content { get; set; } = default!;
    /// <summary>
    /// ͼƬ��ַ
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? ImageUrl { get; set; } = default!;

    /// <summary>
    /// �����������
    /// </summary>
    public int ArticleTypeId { get; set; }
}
