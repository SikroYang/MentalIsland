using System.Linq;
using Mapster;
using MentalIsland.Core.CodeFirst.Enums;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Web.Models.InputModels;
using MentalIsland.Web.Models.OutPubModels;

namespace MentalIsland.Web.Models.Mappers;

public class UserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<User, UserOutput>()
            .Map(repOut => repOut.UserComment, u => string.IsNullOrWhiteSpace(u.UserComment) ? new List<UserComment>() : u.UserComment.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => (UserComment)Convert.ToInt32(s)));

        config.ForType<UserInput, User>()
            .Map(u => u.UserComment, input => input.UserComment == null || input.UserComment.Count == 0 ? null : string.Join(',', input.UserComment.Select(c => (int)c)));
        config.ForType<UserRegistryInput, User>()
            .Map(u => u.UserComment, input => input.UserComment == null || input.UserComment.Count == 0 ? null : string.Join(',', input.UserComment.Select(c => (int)c)));
    }
}