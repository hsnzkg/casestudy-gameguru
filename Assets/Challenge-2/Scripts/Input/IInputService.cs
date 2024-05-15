using System;
using UnityEngine;

namespace Case_2
{
    public interface IInputService
    {
        public void RegisterActionToPress(Action a);
        public void UnRegisterActionToPress(Action a);
        public void Activate();
        public void Deactivate();
    }
}

