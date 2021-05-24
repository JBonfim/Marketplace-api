using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Core.Communication
{
    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; }
    }
}
