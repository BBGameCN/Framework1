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
using System;

namespace Framework
{
    #region Global delegate
    public delegate void OnTouchEventHandle(GameObject _listener, object _args, params object[] _params);

    public delegate void MessageEvent(Message _message);

    public delegate void StateChangedEvent(object _sender, EnumObjectState _newState, EnumObjectState _oldState);
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

    public enum EnumObjectState
    {
        None,
        Initial,
        Loading,
        Ready,
        Disabled,
        Closing
    }

    #endregion

    #region MVC
    public enum EnumModelType
    { }

    public enum EnumViewType
    {
        None = -1,
        TestOne,
        TestTwo
    }

    public enum EnumControllerType
    { }

    public enum EnumMVCEventType
    { }
    #endregion

    #region Static class & Const

    public static class UIPathDefines
    {
        public const string UI_PREFAB = "Prefabs/";

        public static string GetPrefabPathByType(EnumViewType _uiType)
        {
            string _path = string.Empty;
            switch (_uiType)
            {
                case EnumViewType.None:
                        break;
                case EnumViewType.TestOne:
                    _path = UI_PREFAB + "TestOne";
                    break;
                case EnumViewType.TestTwo:
                    _path = UI_PREFAB + "TestTwo";
                    break;
                default:
                    Debug.LogError("Not Find EnumUIType! type:" + _uiType.ToString());
                    break;
            }
            return _path;
        }

        public static Type GetUIScriptByType(EnumViewType _uiType)
        {
            Type _scriptType = null;
            switch (_uiType)
            {
                case EnumViewType.None:
                    break;
                case EnumViewType.TestOne:
                    _scriptType = typeof(TestOne);
                    break;
                case EnumViewType.TestTwo:
                    _scriptType = typeof(TestTwo);
                    break;
                default:
                    Debug.LogError("Not Find EnumUIType! type: " + _uiType.ToString());
                    break;
            }
            return _scriptType;
        }

    }

    #endregion

    public class Defines
    {

    }
}

