﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
