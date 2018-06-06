/*
 *
 *		Title:UI管理
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
    public class UIInfoData
    {
        public EnumUIType UIType
        {
            get;
            private set;
        }

        public Type ScriptType
        {
            get;
            private set;
        }

        public string Path
        {
            get;
            private set;
        }

        public object[] UIParams
        {
            get;
            private set;
        }

        public UIInfoData(EnumUIType _uiType, string _path, params object[] _uiParams)
        {
            this.UIType = _uiType;
            this.Path = _path;
            this.UIParams = _uiParams;
            this.ScriptType = UIPathDefines.GetUIScriptByType(_uiType);
        }
    }

    public class UIManager : Singleton<UIManager>
    {

        private Dictionary<EnumUIType, GameObject> _dicOpenUIs = null;

        private Stack<UIInfoData> _stackOpenUIs = null;

        public override void Init()
        {
            _dicOpenUIs = new Dictionary<EnumUIType, GameObject>();
            _stackOpenUIs = new Stack<UIInfoData>();
        }

        #region GetUIScript & GetUIObject


        public T GetUIComponent<T>(EnumUIType _uiType) where T : BaseUI
        {
            GameObject _retObj = GetUIObject(_uiType);
            if(_retObj != null)
            {
                return _retObj.GetComponent<T>();
            }
            return null;
        }

        public GameObject GetUIObject(EnumUIType _uiType)
        {
            GameObject _retObj = null;
            if(!_dicOpenUIs.TryGetValue(_uiType, out _retObj))
            {
                Debug.LogError("_dicOpenUis TryGetValue Failure! _uiType : " + _uiType.ToString());
            }
            return _retObj;
        }

        #endregion

        #region Preload UI Prefab
        public void PreloadUI(EnumUIType[] _uiTypes)
        {
            for(int i = 0; i < _uiTypes.Length; i++)
            {
                PreloadUI(_uiTypes[i]);
            }
        }

        public void PreloadUI(EnumUIType _uiType)
        {
            string _path = UIPathDefines.GetPrefabPathByType(_uiType);
            ResourceManager.Instance.LoadCoroutine(_path,null);
        }
        #endregion

        #region OpenUI

        public void OpenUI(EnumUIType[] _uiTypes)
        {
            OpenUI(false, _uiTypes, null);
        }

        public void OpenUI(EnumUIType _uiType, params object[] _uiParams)
        {
            EnumUIType[] _uiTypes = new EnumUIType[1];
            _uiTypes[0] = _uiType;
            OpenUI(false, _uiTypes, _uiParams);
        }

        public void OpenUICloseOthers(EnumUIType[] _uiTypes)
        {
            OpenUI(true, _uiTypes, null);
        }


        public void OpenUI(bool _isCloseOthers, EnumUIType[] _uiTypes, params object[] _uiParams)
        {
            if(_isCloseOthers)
            {
                CloseUIAll();
            }

            //Push _uiTypes in Stack;
            for(int i = 0; i < _uiTypes.Length; i++)
            {
                EnumUIType _uiType = _uiTypes[i];
                if(!_dicOpenUIs.ContainsKey(_uiType))
                {
                    string _path = UIPathDefines.GetPrefabPathByType(_uiType);
                    _stackOpenUIs.Push(new UIInfoData(_uiType, _path, _uiParams));
                }
            }

            //Open UI
            if(_stackOpenUIs.Count > 0)
            {
                CoroutineController.Instance.StartCoroutine(this._AsyncLoadData());
            }
        }

        private IEnumerator<int> _AsyncLoadData()
        {
            UIInfoData _uiInfoData = null;
            GameObject _uiObject = null;

            do
            {
                _uiInfoData = _stackOpenUIs.Pop();
                _uiObject = ResourceManager.Instance.LoadInstance(_uiInfoData.Path) as GameObject;
                BaseUI _baseUI = _uiObject.GetComponent<BaseUI>();
                if (_uiObject != null)
                {                 
                    if(_baseUI == null)
                    {
                        _baseUI = _uiObject.AddComponent(_uiInfoData.ScriptType) as BaseUI;
                    }
                }
                else
                {
                    //打开时设置参数
                    _baseUI.SetUIWhenOpening(_uiInfoData.UIParams);
                }
                _dicOpenUIs.Add(_uiInfoData.UIType, _uiObject);

            } while (_stackOpenUIs.Count > 0);

            yield return 0;
        }
        #endregion

        #region CloseUI

        public void CloseUI(EnumUIType _uiType)
        {
            GameObject _uiObj = null;
            if(!_dicOpenUIs.TryGetValue(_uiType, out _uiObj))
            {
                Debug.LogError("_dicOpenUIs TryGetValue Failure! _uiType : " + _uiType.ToString());
                return;
            }
            CloseUI(_uiType, _uiObj);
        }

        public void CloseUI(EnumUIType[] _uiTypes)
        {
            for(int i = 0; i < _uiTypes.Length; i++)
            {
                CloseUI(_uiTypes[i]);
            }
        }

        public void CloseUI(EnumUIType _uiType, GameObject _uiObj)
        {
            if(_uiObj == null)
            {
                _dicOpenUIs.Remove(_uiType);
            }
            else
            {
                BaseUI _baseUI = _uiObj.GetComponent<BaseUI>();
                if(_baseUI != null)
                {
                    _baseUI.StateChanged += _CloseUIHandler;
                    _baseUI.Release();
                }
                else
                {
                    _dicOpenUIs.Remove(_uiType);
                    GameObject.Destroy(_uiObj);                
                }
            }
        }

        public void CloseUIAll()
        {
            List<EnumUIType> _keyList = new List<EnumUIType>(_dicOpenUIs.Keys);
            foreach(EnumUIType _uiType in _keyList)
            {
                GameObject _uiObj = _dicOpenUIs[_uiType];
                CloseUI(_uiType, _uiObj);
            }
            _dicOpenUIs.Clear();
        }

        private void _CloseUIHandler(object _sender, EnumObjectState _newState, EnumObjectState _oldState)
        {
            if(_newState == EnumObjectState.Closing)
            {
                BaseUI _baseUI = _sender as BaseUI;
                _dicOpenUIs.Remove(_baseUI.GetUIType());
                _baseUI.StateChanged -= _CloseUIHandler;
            }
        }

        #endregion

    }
}

