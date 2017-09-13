using EmptyRoomAlert.Foundation.Core.Services.Email;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;

namespace EmptyRoomAlert.Identity.Message
{
    public interface IIdentityMessageBuilder : IMessageBuilder
    {
        void Build(ApplicationUser user, string subject, string body);
    }
}
