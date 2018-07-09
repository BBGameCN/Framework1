using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    public abstract class View : MonoBehaviour
    {
        public abstract EnumViewType ViewType { get; }

        [HideInInspector]
        public List<EnumMVCEventType> AttentionEvents = new List<EnumMVCEventType>();

        public virtual void RegisterAttentionEvent()
        {
            //AttentionEvents.Add();

        }

        public abstract void HandleEvent(EnumMVCEventType _eventType, object _data);

        protected T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>() as T;
        }

        protected void SendEvent(EnumMVCEventType _eventType, object _data = null)
        {
            MVC.SendEvent(_eventType, _data);
        }
    }
}

