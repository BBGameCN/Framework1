using System;

namespace Framework
{
    public abstract class ApplicationBase : DDOLSingleton<ApplicationBase>
    {
        protected void RegisterController(EnumMVCEventType _eventType, Type _controllerType)
        {
            MVC.RegisterController(_eventType, _controllerType);
        }

        protected void SendEvent(EnumMVCEventType _eventType, object _data = null)
        {
            MVC.SendEvent(_eventType, _data);
        }
    }
}

