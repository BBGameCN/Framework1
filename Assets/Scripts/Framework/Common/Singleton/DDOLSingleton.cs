/*
 *
 *		Title:继承MonoBehavior的单例模板
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
    public abstract class DDOLSingleton<T> : MonoBehaviour where T : DDOLSingleton<T>
    {
        protected static T m_instance = null;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject go = GameObject.Find("DDOLGameObject");
                    if (go == null)
                    {
                        go = new GameObject("DDOLGameObject");
                        DontDestroyOnLoad(go);
                    }
                    m_instance = go.AddComponent<T>();
                }
                return m_instance;
            }
        }

        protected DDOLSingleton() { }

        private void OnApplicationQuit()
        {
            m_instance = null;
        }
    }
}

