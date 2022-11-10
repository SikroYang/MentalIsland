using System.ComponentModel;
using MentalIsland.Core.CodeFirst.SqlSugarBase;
using SqlSugar;

namespace MentalIsland.Core.CodeFirst.Models;

/// <summary>
/// ������������
/// </summary>
[SugarTable("ArticleTypes")]
[Description("�������ͱ�")]
public class ArticleType
{
    /// <summary>
    /// ����
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// ������
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// �����������
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(Article.ArticleTypeId))]
    public virtual List<Article> Articles { get; set; }

    /// <summary>
    /// �߼�ɾ��
    /// </summary>
    public virtual bool IsDeleted { get; set; } = false;
}
