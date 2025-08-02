using UnityEngine;


namespace TopDown.Generator
{
    public interface IActivatable
    {
        bool Active { get; }

        void Activate();
    }
}
