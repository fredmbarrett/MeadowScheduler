using MeadowScheduler.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeadowScheduler.Data
{
    internal abstract class DataContextBase : MeadowBase
    {
        public int NextDeviceMessageId
        {
            get
            {
                return _nextMessageId++;
            }
        }
        int _nextMessageId = 1;
    }
}
