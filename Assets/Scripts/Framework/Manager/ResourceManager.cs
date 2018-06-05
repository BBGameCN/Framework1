/*
 *
 *		Title: 资源管理
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
using System;

namespace Framework
{
    public class AssetInfo
    {
        private UnityEngine.Object _object;
        public Type AssetType { get; set; }
        public string Path { get; set; }
        public int RefCount { get; set; }
        public bool IsLoaded
        {
            get
            {
                return _object != null;
            }
        }

        public UnityEngine.Object AssetObject
        {
            get
            {
                if (_object == null)
                {
                    _ResourcesLoad();
                }
                return _object;
            }
        }

        public IEnumerator GetCoroutineObject(Action<UnityEngine.Object> _loaded)
        {
            while (true)
            {
                yield return null;
                if (_object == null)
                {
                    _ResourcesLoad();
                    yield return null;
                }
                if (_loaded != null)
                {
                    _loaded(_object);
                }
                yield break;
            }
        }

        private void _ResourcesLoad()
        {
            try
            {
                _object = Resources.Load(Path);
                if (_object == null)
                {
                    Debug.Log("Resourses Load Failure! Path:" + Path);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> _loaded, Action<float> _progress)
        {
            // have object
            if (_object != null)
            {
                _loaded(_object);
                yield break;
            }

            //object null Not Load Resources
            ResourceRequest _resRequest = Resources.LoadAsync(Path);

            while (_resRequest.progress < 0.9f)
            {
                if (_progress != null)
                {
                    _progress(_resRequest.progress);
                }
                yield return null;
            }

            while (!_resRequest.isDone)
            {
                if (_progress != null)
                {
                    _progress(_resRequest.progress);
                }
                yield return null;
            }

            _object = _resRequest.asset;
            if (_loaded != null)
            {
                _loaded(_object);
            }

            yield return _resRequest;
        }

        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> _loaded)
        {
            return GetAsyncObject(_loaded, null);
        }
    }

    public class ResourceManager : Singleton<ResourceManager>
    {
        private Dictionary<string, AssetInfo> _dicAssetInfo = null;

        public override void Init()
        {
            //<path, >
            _dicAssetInfo = new Dictionary<string, AssetInfo>();
        }

        #region Load Resources & Instantiate Object

        public UnityEngine.Object LoadInstance(string _path)
        {
            UnityEngine.Object _obj = Load(_path);
            return _Instantiate(_obj);
        }

        /// <summary>
        /// 通过协程加载并实例化物体
        /// 需要自己提供获得实例函数
        /// (_obj)=>{go = _obj as GameObject;}
        /// </summary>
        /// <param name="_path">加载路径</param>
        /// <param name="_loaded">加载完成后执行的方法</param>
        public void LoadCoroutineInstance(string _path, Action<UnityEngine.Object> _loaded)
        {
            LoadCoroutine(_path, (_obj) => { _Instantiate(_obj, _loaded); });
        }

        public void LoadAsyncInstance(string _path, Action<UnityEngine.Object> _loaded)
        {
            LoadAsync(_path, (_obj) => { _Instantiate(_obj, _loaded); });
        }

        public void LoadAsyncInstance(string _path, Action<UnityEngine.Object> _loaded, Action<float> _progress)
        {
            LoadAsync(_path, (_obj) => { _Instantiate(_obj, _loaded); }, _progress);
        }

        #endregion

        #region Load Resources
        public UnityEngine.Object Load(string _path)
        {
            AssetInfo _assetInfo = _GetAssetInfo(_path);
            if (_assetInfo != null)
            {
                return _assetInfo.AssetObject;
            }
            return null;
        }
        #endregion

        #region Load Coroutine Resources
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_path">加载路径</param>
        /// <param name="_loaded">加载完成后执行的方法</param>
        public void LoadCoroutine(string _path, Action<UnityEngine.Object> _loaded)
        {
            AssetInfo _assetInfo = _GetAssetInfo(_path, _loaded);
            if(_assetInfo != null)
            {
                CoroutineController.Instance.StartCoroutine(_assetInfo.GetCoroutineObject(_loaded));
            }
        }
        #endregion

        #region Load Async Resources
        public void LoadAsync(string _path, Action<UnityEngine.Object> _loaded, Action<float> _progress)
        {
            AssetInfo _assetInfo = _GetAssetInfo(_path, _loaded);
            if(_assetInfo != null)
            {
                CoroutineController.Instance.StartCoroutine(_assetInfo.GetAsyncObject(_loaded, _progress));
            }
        }

        public void LoadAsync(string _path, Action<UnityEngine.Object> _loaded)
        {
            LoadAsync(_path, _loaded, null);
        }
        #endregion

        #region Get AssetInfo & Instantiate Object 
        private AssetInfo _GetAssetInfo(string _path, Action<UnityEngine.Object> _loaded)
        {
            if(string.IsNullOrEmpty(_path))
            {
                Debug.LogError("Error: null _path name");
                if(_loaded != null)
                {
                    _loaded(null);
                }
            }

            //Load Resrouces
            AssetInfo _assetInfo = null;
            if(!_dicAssetInfo.TryGetValue(_path, out _assetInfo))
            {
                _assetInfo = new AssetInfo();
                _assetInfo.Path = _path;
                _dicAssetInfo.Add(_path, _assetInfo);
            }
            _assetInfo.RefCount++;
            return _assetInfo;
        }

        private AssetInfo _GetAssetInfo(string _path)
        {
            return _GetAssetInfo(_path, null);
        }

        private UnityEngine.Object _Instantiate(UnityEngine.Object _obj, Action<UnityEngine.Object> _loaded)
        {
            UnityEngine.Object _retObj = null;
            if(_obj != null)
            {
                _retObj = MonoBehaviour.Instantiate(_obj);
                if(_retObj != null)
                {
                    if(_loaded != null)
                    {
                        _loaded(_retObj);
                        return null;
                    }
                    return _retObj;
                }
                else
                {
                    Debug.LogError("Error: null Instantiate _retObj");
                }
            }
            else
            {
                Debug.LogError("Error: null Resources Load return _obj");
            }
            return null;
        }

        private UnityEngine.Object _Instantiate(UnityEngine.Object _obj)
        {
            return _Instantiate(_obj, null);
        }
        #endregion
    }
}

