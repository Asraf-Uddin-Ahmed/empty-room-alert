﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;

namespace EmptyRoomAlert.Foundation.Core.Services.Email
{
    public interface IConfirmUserMessageBuilder : IMessageBuilder
    {
        void Build(User newUser, string url);
    }
}
