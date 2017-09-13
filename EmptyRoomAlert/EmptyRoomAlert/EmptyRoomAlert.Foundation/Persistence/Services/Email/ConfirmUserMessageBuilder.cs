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
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;

namespace EmptyRoomAlert.Foundation.Persistence.Services.Email
{
    public class ConfirmUserMessageBuilder : MessageBuilder, IConfirmUserMessageBuilder
    {
        private ApplicationUser _newUser;
        private string _url;
        private ApplicationUser NewUser
        {
            get
            {
                if (_newUser == null)
                {
                    throw new NullReferenceException("Url value is not provided");
                }
                return _newUser;
            }
            set
            {
                _newUser = value;
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
        public ConfirmUserMessageBuilder(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public void Build(ApplicationUser newUser, string url)
        {
            this.NewUser = newUser;
            this.Url = url;
        }



        protected override string GetSubject()
        {
            return "Confirm your email address";
        }

        protected override string GetBody()
        {
            ConfirmUser template = new ConfirmUser(this.NewUser, this.Url);
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
                new NameWithEmail(this.NewUser.UserName, this.NewUser.Email) 
            };
        }

        protected override List<NameWithEmail> GetReplyToList()
        {
            return new List<NameWithEmail>() { base.GetSystemNameWithEmail() };
        }

    }
}
