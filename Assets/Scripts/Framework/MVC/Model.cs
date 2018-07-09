/*
 *
 *		Title:
 *
 *		Description:
 *
 *		Author: 
 *
 *		Date: 2018.x
 *
 *		Modify:
 *
*/
using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    public abstract class Model
    {
        public abstract EnumModelType ModelType { get; }

        protected void _SendEvent(EnumMVCEventType _eventType, object _data = null)
        {
            MVC.SendEvent(_eventType, _data);
        }

    }
}

