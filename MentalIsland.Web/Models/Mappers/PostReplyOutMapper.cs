using Mapster;
using MentalIsland.Core.CodeFirst.Models;
using MentalIsland.Web.Models.OutPubModels;

namespace MentalIsland.Web.Models.Mappers;

public class PostReplyOutMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Island_Post, PostReplyOutput>()
            .Map(repOut => repOut.Replies, post => new PagedList<ReplyOutput> { List = post.Replies.Adapt<List<ReplyOutput>>() });
    }
}