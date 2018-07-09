using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    public class PoolManager : Singleton<PoolManager>
{

        private string m_resourceDir = "";
        private Transform m_parent;
        public Transform Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }
        public string ResourceDir
        {
            get { return m_resourceDir; }
            set { m_resourceDir = value; }
        }

        private Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

        private void _RegisterNewSubPool(string _poolName)
        {
            string _path = "";
            if(string.IsNullOrEmpty(ResourceDir))
            {
                Debug.LogWarning("Dont set Pool Prefab path");
                _path = _poolName;
            }
            else
            {
                _path = ResourceDir + "/" + _poolName;
            }

            GameObject _prefab = Resources.Load<GameObject>(_path);

            SubPool pool = new SubPool(Parent, _prefab);
            m_pools.Add(pool.Name, pool);
        }

        public GameObject Spawn(string _name)
        {
            if(m_pools.ContainsKey(_name) == false)
            {
                _RegisterNewSubPool(_name);
            }
            SubPool _pool = m_pools[_name];
            return _pool.Spawn();
        }

        public void Unspawn(GameObject _go)
        {
            SubPool _pool = null;
            foreach(SubPool p in m_pools.Values)
            {
                if(p.Contains(_go))
                {
                    _pool = p;
                    break;
                }
            }
            _pool.Unspawn(_go);
        }

        public void UnspawnAll()
        {
            foreach(SubPool p in m_pools.Values)
            {
                p.UnspawnAll();
            }
        }

    }
}

