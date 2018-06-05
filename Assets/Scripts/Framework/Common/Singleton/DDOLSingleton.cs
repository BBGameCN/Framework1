﻿/*
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
        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObject.Find("DDOLGameObject");
                    if (go == null)
                    {
                        go = new GameObject("DDOLGameObject");
                        DontDestroyOnLoad(go);
                    }
                    _instance = go.AddComponent<T>();
                }
                return _instance;
            }
        }

        protected DDOLSingleton() { }

        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}
