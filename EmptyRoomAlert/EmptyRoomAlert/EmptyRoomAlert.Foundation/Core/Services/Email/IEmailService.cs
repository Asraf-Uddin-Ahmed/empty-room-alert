using Ratul.Utility.Email;
using System;
using EmptyRoomAlert.Foundation.Core.Aggregates;
namespace EmptyRoomAlert.Foundation.Core.Services.Email
{
    // Director for MessageBuilder
    public interface IEmailService
    {
        void SendText(IMessageBuilder messageFactory);
        void SendHtml(IMessageBuilder messageFactory);
        void SendTextAsync(IMessageBuilder messageBuilder);
        void SendHtmlAsync(IMessageBuilder messageBuilder);
        void SendTextAsync(IMessageBuilder messageBuilder, EmailSender.SendCompletedCallback sendCompletedCallback);
        void SendHtmlAsync(IMessageBuilder messageBuilder, EmailSender.SendCompletedCallback sendCompletedCallback);
        void SendAsyncCancel();
    }
}
