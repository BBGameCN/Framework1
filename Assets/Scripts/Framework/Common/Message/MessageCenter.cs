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
        private Dictionary<string, List<MessageEvent>> m_dicMessageEvents = null;

        public override void Init()
        {
            m_dicMessageEvents = new Dictionary<string, List<MessageEvent>>();
        }

        #region Add & Remove Listener
        public void AddListener(string _messageName, MessageEvent _messageEvent)
        {
            List<MessageEvent> _list = null;
            if (m_dicMessageEvents.ContainsKey(_messageName))
            {
                _list = m_dicMessageEvents[_messageName];
            }
            else
            {
                _list = new List<MessageEvent>();
                m_dicMessageEvents.Add(_messageName, _list);
            }

            if(!_list.Contains(_messageEvent))
            {
                _list.Add(_messageEvent);
            }
        }

        public void RemoveListener(string _messageName, MessageEvent _messageEvent)
        {
            if(m_dicMessageEvents.ContainsKey(_messageName))
            {
                List<MessageEvent> _list = m_dicMessageEvents[_messageName];
                if(_list.Contains(_messageEvent))
                {
                    _list.Remove(_messageEvent);
                }
                if(_list.Count <= 0)
                {
                    m_dicMessageEvents.Remove(_messageName);
                }
            }
        }

        public void RemoveAllListener()
        {
            m_dicMessageEvents.Clear();
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
            if(m_dicMessageEvents == null || !m_dicMessageEvents.ContainsKey(_message.Name))
            {
                return;
            }
            List<MessageEvent> _list = m_dicMessageEvents[_message.Name];
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

