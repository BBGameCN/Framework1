using System.Collections;
using System;

namespace Framework
{
    public abstract class Controller
    {
        protected T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>() as T;
        }

        protected T GetView<T>() where T : View
        {
            return MVC.GetView<T>() as T;
        }

        protected void RegisterModel(Model _model)
        {
            MVC.RegisterModel(_model);
        }

        protected void RegisterView(View _view)
        {
            MVC.RegisterView(_view);
        }

        protected void RegisterController(EnumMVCEventType _eventType, Type _controllerType)
        {
            MVC.RegisterController(_eventType, _controllerType);
        }

        public abstract void Execute(object _data = null);

}
}

