﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;

namespace EmptyRoomAlert.Foundation.Core.Services.Email
{
    public interface IConfirmUserMessageBuilder : IMessageBuilder
    {
        void Build(ApplicationUser newUser, string url);
    }
}
