/*
 *
 *		Title:消息类
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
using System;

namespace Framework
{
    public class Message : IEnumerable<KeyValuePair<string, object>>{

        private Dictionary<string, object> m_dicDatas = null;

        public string Name { get; private set; }
        public object Sender { get; private set; }
        public object Content { get; private set; }

        #region message[key] = value or data = message[key]
        public object this[string key]
        {
            get
            {
                if(m_dicDatas == null || !m_dicDatas.ContainsKey(key))
                {
                    return null;
                }
                return m_dicDatas[key];
            }
            set
            {
                if(m_dicDatas == null)
                {
                    m_dicDatas = new Dictionary<string, object>();
                }
                if(m_dicDatas.ContainsKey(key))
                {
                    m_dicDatas[key] = value;
                }
                else
                {
                    m_dicDatas.Add(key, value);
                }
            }
        }
        #endregion

        #region Message Construction Function
        public Message(string _name, object _sender)
        {
            Name = _name;
            Sender = _sender;
            Content = null;
        }

        public Message(string _name, object _sender, object _content)
        {
            Name = _name;
            Sender = _sender;
            Content = _content;
        }

        public Message(string _name, object _sender, object _content, params object[] _dicParams)
        {
            Name = _name;
            Sender = _sender;
            Content = _content;
            if(m_dicDatas.GetType() == typeof(Dictionary<string, object>))
            {
                foreach(object _param in _dicParams)
                {
                    foreach(KeyValuePair<string, object> kvp in _param as Dictionary<string, object>)
                    {
                        this[kvp.Key] = kvp.Value;
                    }
                }
            }
        }

        public Message(Message _message)
        {
            this.Name = _message.Name;
            this.Sender = _message.Sender;
            this.Content = _message.Content;
            foreach (KeyValuePair<string, object> kvp in _message.m_dicDatas)
            {
                this[kvp.Key] = kvp.Value;
            }
        }
        #endregion

        #region Add & Remove
        public void Add(string key, object value)
        {
            this[key] = value;
        }

        public void Remove(string key)
        {
            if(m_dicDatas != null && m_dicDatas.ContainsKey(key))
            {
                m_dicDatas.Remove(key);
            }
        }
        #endregion

        public void Send()
        {
            MessageCenter.Instance.SendMessage(this);
        }


        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            if(m_dicDatas == null)
            {
                yield break;
            }
            foreach(KeyValuePair<string, object> kvp in m_dicDatas)
            {
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return m_dicDatas.GetEnumerator();
        }
    }
}

