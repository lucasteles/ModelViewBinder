using System;

namespace ModelViewBinder
{
    public interface IControlWithValue : IControlWithChangeEvent
    {
        object Value { get; set; }
    }

    public interface IControlWithChangeEvent
    {
        event EventHandler ValueChanged;
    }

    public interface IControlWithEnabled
    {
        bool Enabled { get; set; }
    }
}