using Furion;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Core.CodeFirst.SqlSugarBase;

namespace MentalIsland.Web.Models.Extensions;

public static class Utils
{
    private static readonly SqlSugarRepository<BlackKey> blackkeyRepository = App.GetService<SqlSugarRepository<BlackKey>>();

    private static string[] blackWrods;

    private static string[] GetBlackWords()
    {
        if (blackWrods == null)
            blackWrods = blackkeyRepository.AsQueryable().Select(wa => wa.Keyword).ToArray();

        return blackWrods;
    }

    public static bool ContainsKeyWords(this string Content)
    {
        var blackList = GetBlackWords();
        if (blackList.Length > 0)
        {
            return blackList.Any(wa => Content.Contains(wa));
        }
        return false;
    }
}