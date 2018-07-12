/*
 *
 *		Title:单例模板
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
using System;
using UnityEngine;

namespace Framework
{
    public abstract class Singleton<T> where T : class, new()
    {
        protected static T m_instance = null;

        public static T Instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = new T();
                }
                return m_instance;
            }
        }

        protected Singleton()
        {
            if(m_instance != null)
            {
                throw new SingletonException("This " + (typeof(T).ToString()) + "Singleton Instance is not null !!!");
            }
            Init();
        }

        public virtual void Init()
        {

        }
    }
}

