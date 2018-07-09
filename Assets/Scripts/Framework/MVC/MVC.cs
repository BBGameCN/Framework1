using System.Collections;
using System;
using System.Collections.Generic;

namespace Framework
{
    public static class MVC
    {
        private static Dictionary<EnumModelType, Model> m_Models = new Dictionary<EnumModelType, Model>();
        private static Dictionary<EnumViewType, View> m_Views = new Dictionary<EnumViewType, View>();
        private static Dictionary<EnumMVCEventType, Type> m_CommandMap = new Dictionary<EnumMVCEventType, Type>();

        public static void RegisterModel(Model _model)
        {
            m_Models[_model.ModelType] = _model;
        }

        public static void RegisterView(View _view)
        {
            _view.RegisterAttentionEvent();

            m_Views[_view.ViewType] = _view;
        }

        public static void RegisterController(EnumMVCEventType _eventType, Type _controllerType)
        {
            m_CommandMap[_eventType] = _controllerType;
        }

        public static T GetModel<T>() where T : Model
        {
            foreach(Model m in m_Models.Values)
            {
                if(m is T)
                {
                    return (T)m;
                }
            }
            return null;
        }

        public static T GetView<T>() where T : View
        {
            foreach(View v in m_Views.Values)
            {
                if(v is T)
                {
                    return (T)v;
                }
            }
            return null;
        }

        public static void SendEvent(EnumMVCEventType _eventType, object _data = null)
        {
            if(m_CommandMap.ContainsKey(_eventType))
            {
                Type t = m_CommandMap[_eventType];
                Controller c = Activator.CreateInstance(t) as Controller;
                c.Execute(_data);
            }

            foreach(View v in m_Views.Values)
            {
                if(v.AttentionEvents.Contains(_eventType))
                {
                    v.HandleEvent(_eventType, _data);
                }
            }
        }
    }
}

