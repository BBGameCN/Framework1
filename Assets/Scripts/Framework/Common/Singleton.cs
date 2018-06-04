/*
 *
 *		Title:单例模板
 *
 *		Description:
 *
 *		Author: yhb
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
        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        protected Singleton()
        {
            if(_instance != null)
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

