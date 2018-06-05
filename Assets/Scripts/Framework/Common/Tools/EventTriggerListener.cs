/*
 *
 *		Title:UGUI事件监听
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
using UnityEngine.EventSystems;

namespace Framework
{

    public class TouchHandle
    {
        private event OnTouchEventHandle _eventHandle = null;
        private object[] _handleParams;


        public TouchHandle()
        {

        }

        public TouchHandle(OnTouchEventHandle _handle, params object[] _params)
        {
            SetHandle(_handle, _params);
        }

        public void SetHandle(OnTouchEventHandle _handle, params object[] _params)
        {
            DestroyHandle();
            _eventHandle += _handle;
            _handleParams = _params;
        }

        public void CallEventHandle(GameObject _listener, object _args)
        {
            if(_eventHandle != null)
            {
                _eventHandle(_listener, _args, _handleParams);
            }
        }

        public void DestroyHandle()
        {
            if(_eventHandle != null)
            {
                _eventHandle -= _eventHandle;
                _eventHandle = null;
            }
        }
    }

    public class EventTriggerListener : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler,

    ISelectHandler,
    IUpdateSelectedHandler,
    IDeselectHandler,

    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IScrollHandler,
    IMoveHandler
    {
        #region TouchHandle define
        public TouchHandle onClick;
        public TouchHandle onDoubleClick;
        public TouchHandle onDown;
        public TouchHandle onEnter;
        public TouchHandle onExit;
        public TouchHandle onUp;
        public TouchHandle onSelect;
        public TouchHandle onUpdateSelect;
        public TouchHandle onDeSelect;
        public TouchHandle onDrag;
        public TouchHandle onDragEnd;
        public TouchHandle onDrop;
        public TouchHandle onScroll;
        public TouchHandle onMove;
        #endregion

        public static EventTriggerListener Get(GameObject go)
        {
            return go.GetOrAddComponent<EventTriggerListener>();
        }

        private void RemoveAllHandle()
        {
            this._RemoveHandle(onClick);
            this._RemoveHandle(onDoubleClick);
            this._RemoveHandle(onDown);
            this._RemoveHandle(onEnter);
            this._RemoveHandle(onExit);
            this._RemoveHandle(onUp);
            this._RemoveHandle(onDrop);
            this._RemoveHandle(onDrag);
            this._RemoveHandle(onDragEnd);
            this._RemoveHandle(onScroll);
            this._RemoveHandle(onMove);
            this._RemoveHandle(onUpdateSelect);
            this._RemoveHandle(onSelect);
            this._RemoveHandle(onDeSelect);
        }

        private void _RemoveHandle(TouchHandle _handle)
        {
            if(_handle != null)
            {
                _handle.DestroyHandle();
                _handle = null;
            }
        }

        private void OnDestroy()
        {
            RemoveAllHandle();
        }

        public void SetEventHandle(EnumTouchEventType _type, OnTouchEventHandle _handle, params object[] _params)
        {
            switch (_type)
            {
                case EnumTouchEventType.OnClick:
                    if(onClick == null)
                    {
                        onClick = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnDoubleClick:
                    if (onDoubleClick == null)
                    {
                        onDoubleClick = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnDown:
                    if(onDown == null)
                    {
                        onDown = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnUp:
                    if (onUp == null)
                    {
                        onUp = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnEnter:
                    if (onEnter == null)
                    {
                        onEnter = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnExit:
                    if (onExit == null)
                    {
                        onExit = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnSelect:
                    if (onSelect == null)
                    {
                        onSelect = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnUpdateSelect:
                    if (onUpdateSelect == null)
                    {
                        onUpdateSelect = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnDeSelect:
                    if (onDeSelect == null)
                    {
                        onDeSelect = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnDrag:
                    if (onDrag == null)
                    {
                        onDrag = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnDragEnd:
                    if (onDragEnd == null)
                    {
                        onDragEnd = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnDrop:
                    if (onDrop == null)
                    {
                        onDrop = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnScroll:
                    if (onScroll == null)
                    {
                        onScroll = new TouchHandle(_handle, _params);
                    }
                    break;
                case EnumTouchEventType.OnMove:
                    if (onMove == null)
                    {
                        onMove = new TouchHandle(_handle, _params);
                    }
                    break;
            }
        }

        public void SetEventHandle(EnumTouchEventType _type, OnTouchEventHandle _handle)
        {
            SetEventHandle(_type, _handle, null);
        }

        #region Call
        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
            {
                onClick.CallEventHandle(this.gameObject, eventData);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null)
            {
                onDown.CallEventHandle(this.gameObject, eventData);
            }              
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null)
            {
                onUp.CallEventHandle(this.gameObject, eventData);
            }            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null)
            {
                onEnter.CallEventHandle(this.gameObject, eventData);
            }
                
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null)
            {
                onExit.CallEventHandle(this.gameObject, eventData);
            }           
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (onSelect != null)
            {
                onSelect.CallEventHandle(this.gameObject, eventData);
            }            
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (onUpdateSelect != null)
            {
                onUpdateSelect.CallEventHandle(this.gameObject, eventData);
            }            
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (onDeSelect != null)
            {
                onDeSelect.CallEventHandle(this.gameObject, eventData);
            }       
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null)
            {
                onDrag.CallEventHandle(this.gameObject, eventData);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (onDrop != null)
            {
                onDrop.CallEventHandle(this.gameObject, eventData);
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (onScroll != null)
            {
                onScroll.CallEventHandle(this.gameObject, eventData);
            }      
        }

        public void OnMove(AxisEventData eventData)
        {
            if (onMove != null)
            {
                onMove.CallEventHandle(this.gameObject, eventData);
            }
                
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (onDragEnd != null)
            {
                onDragEnd.CallEventHandle(this.gameObject, eventData);
            }              
        }
        #endregion
    }
}

