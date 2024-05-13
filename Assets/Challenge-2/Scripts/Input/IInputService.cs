using System;
using UnityEngine;

public interface IInputService
{
    public void RegisterActionToPress(Action a);
    public void UnRegisterActionToPress(Action a);
    public void Activate();
    public void Deactivate();
}
