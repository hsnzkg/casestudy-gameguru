using System;
using UnityEngine;

public class DesktopInputService : MonoBehaviour, IInputService
{
    private Action OnPress;
    private bool _isActive;
    public void Activate()
    {
        if (_isActive) return;
        _isActive = true;
    }

    public void Deactivate()
    {
        if(!_isActive) return;  
        _isActive = false;
    }

    public void RegisterActionToPress(Action a)
    {
        if (a != null) OnPress += a;
    }

    public void UnRegisterActionToPress(Action a)
    {
        if (a != null) OnPress -= a;
    }

    private void Update()
    {
        if (!_isActive) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPress?.Invoke();
        }
    }
}