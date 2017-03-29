using System;
using System.Collections.Generic;
using System.Text;

namespace ModelViewBinder
{
    public interface IDisposeContainer
    {
        void RegisterDispose(IDisposable item);
    }
}
