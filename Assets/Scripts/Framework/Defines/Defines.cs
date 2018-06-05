/*
 *
 *		Title:全局定义
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
using UnityEngine;

namespace Framework
{
    #region Global delegate
    public delegate void OnTouchEventHandle(GameObject _listener, object _args, params object[] _params);

    public delegate void MessageEvent(Message _message);
    #endregion

    #region Global enum
    public enum EnumTouchEventType
    {
        OnClick,
        OnDoubleClick,
        OnDown,
        OnUp,
        OnEnter,
        OnExit,
        OnSelect,
        OnUpdateSelect,
        OnDeSelect,
        OnDrag,
        OnDragEnd,
        OnDrop,
        OnScroll,
        OnMove,
    }

    public enum EnumMessageType
    {
        TestSendMessage
    }
    #endregion

    public class Defines
    {

    }
}

