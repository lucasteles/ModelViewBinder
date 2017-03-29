using System;

namespace ModelViewBinder
{

    public interface ITargetWithValue : ITargetWithValue<object>
    {

    }

    public interface ITargetWithValue<TValue> : ITargetWithChangeEvent
    {
        TValue Value { get; set; }
    }

    public interface ITargetWithChangeEvent
    {
        event EventHandler ValueChanged;
    }

    public interface ITargetWithEnabled
    {
        bool Enabled { get; set; }
    }
}