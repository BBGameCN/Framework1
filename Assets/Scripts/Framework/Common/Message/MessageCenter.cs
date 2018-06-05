/*
 *
 *		Title:消息处理中心
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
    public class MessageCenter : Singleton<MessageCenter>
    {
        private Dictionary<string, List<MessageEvent>> _dicMessageEvents = null;

        public override void Init()
        {
            _dicMessageEvents = new Dictionary<string, List<MessageEvent>>();
        }

        #region Add & Remove Listener
        public void AddListener(string _messageName, MessageEvent _messageEvent)
        {
            List<MessageEvent> _list = null;
            if (_dicMessageEvents.ContainsKey(_messageName))
            {
                _list = _dicMessageEvents[_messageName];
            }
            else
            {
                _list = new List<MessageEvent>();
                _dicMessageEvents.Add(_messageName, _list);
            }

            if(!_list.Contains(_messageEvent))
            {
                _list.Add(_messageEvent);
            }
        }

        public void RemoveListener(string _messageName, MessageEvent _messageEvent)
        {
            if(_dicMessageEvents.ContainsKey(_messageName))
            {
                List<MessageEvent> _list = _dicMessageEvents[_messageName];
                if(_list.Contains(_messageEvent))
                {
                    _list.Remove(_messageEvent);
                }
                if(_list.Count <= 0)
                {
                    _dicMessageEvents.Remove(_messageName);
                }
            }
        }

        public void RemoveAllListener()
        {
            _dicMessageEvents.Clear();
        }
        #endregion

        #region Send Message

        public void SendMessage(Message _message)
        {
            _DoMessageDispatcher(_message);
        }

        public void SendMessage(string _name, object _sender)
        {
            SendMessage(new Message(_name, _sender));
        }

        public void SendMessage(string _name, object _sender, object _content)
        {
            SendMessage(new Message(_name, _sender, _content));
        }

        public void SendMessage(string _name, object _sender, object _content, params object[] _dicParams)
        {
            SendMessage(new Message(_name, _sender, _content, _dicParams));
        }

        private void _DoMessageDispatcher(Message _message)
        {
            if(_dicMessageEvents == null || !_dicMessageEvents.ContainsKey(_message.Name))
            {
                return;
            }
            List<MessageEvent> _list = _dicMessageEvents[_message.Name];
            for(int i = 0; i < _list.Count; i++)
            {
                MessageEvent _messageEvent = _list[i];
                if(_message != null)
                {
                    _messageEvent(_message);
                }
            }
             
        }
        #endregion
    }
}

