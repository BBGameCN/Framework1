/*
 *
 *		Title:
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
    public class SubPool
    {
        private Transform m_parent;

        private GameObject m_prefab;

        List<GameObject> m_objects = new List<GameObject>();

        public string Name
        {
            get { return m_prefab.name; }
        }

        public SubPool(Transform _parent, GameObject _prefab)
        {
            this.m_parent = _parent;
            this.m_prefab = _prefab;
        }

        public GameObject Spawn()
        {
            GameObject go = null;

            foreach(GameObject obj in m_objects)
            {
                if(obj.activeSelf == false)
                {
                    go = obj;
                    break;
                }
            }

            if(go == null)
            {
                go = GameObject.Instantiate<GameObject>(m_prefab);
                go.transform.parent = m_parent;
                m_objects.Add(go);
            }

            go.SetActive(true);
            go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
            return go;
        }

        public void Unspawn(GameObject _go)
        {
            if(m_objects.Contains(_go))
            {
                _go.SendMessage("OnUpspawn", SendMessageOptions.DontRequireReceiver);
                _go.SetActive(false);
            }
        }

        public void UnspawnAll()
        {
            foreach(GameObject item in m_objects)
            {
                if(item.activeSelf)
                {
                    Unspawn(item);
                }
            }
        }

        public bool Contains(GameObject _go)
        {
            return m_objects.Contains(_go);
        }
    }

}

