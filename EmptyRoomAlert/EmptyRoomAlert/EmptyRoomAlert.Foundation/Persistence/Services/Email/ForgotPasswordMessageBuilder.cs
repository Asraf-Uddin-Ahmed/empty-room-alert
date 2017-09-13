using Ninject;
using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Core.Services.Email;
using EmptyRoomAlert.Foundation.Persistence.Template.Email;

namespace EmptyRoomAlert.Foundation.Persistence.Services.Email
{
    public class ForgotPasswordMessageBuilder : MessageBuilder, IForgotPasswordMessageBuilder
    {
        private User _registeredUser;
        private string _url;
        private User RegisteredUser
        {
            get
            {
                if (_registeredUser == null)
                {
                    throw new NullReferenceException("Url value is not provided");
                }
                return _registeredUser;
            }
            set
            {
                _registeredUser = value;
            }
        }
        private string Url
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                {
                    throw new NullReferenceException("Url value is not provided");
                }
                return _url;
            }
            set
            {
                _url = value;
            }
        }



        [Inject]
        public ForgotPasswordMessageBuilder(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public void Build(User registeredUser, string url)
        {
            this.RegisteredUser = registeredUser;
            this.Url = url;
        }



        protected override string GetSubject()
        {
            return "Change your Website password";
        }

        protected override string GetBody()
        {
            ForgotPassword template = new ForgotPassword(this.RegisteredUser, this.Url);
            string body = template.TransformText();
            return body;
        }

        protected override NameWithEmail GetFrom()
        {
            return base.GetSystemNameWithEmail();
        }

        protected override List<NameWithEmail> GetToList()
        {
            return new List<NameWithEmail>() 
            { 
                new NameWithEmail(this.RegisteredUser.Name, this.RegisteredUser.EmailAddress) 
            };
        }

        protected override List<NameWithEmail> GetReplyToList()
        {
            return new List<NameWithEmail>() { base.GetSystemNameWithEmail() };
        }

    }
}
