﻿using Ratul.Utility.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Foundation.Core.Services.Email
{
    public interface IMessageBuilder
    {
        MessageSettings GetText();
        MessageSettings GetHtml();
    }
}
