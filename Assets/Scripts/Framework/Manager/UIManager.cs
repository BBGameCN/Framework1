﻿/*
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
        public EnumViewType UIType
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

        public UIInfoData(EnumViewType _uiType, string _path, params object[] _uiParams)
        {
            this.UIType = _uiType;
            this.Path = _path;
            this.UIParams = _uiParams;
            this.ScriptType = UIPathDefines.GetUIScriptByType(_uiType);
        }
    }

    public class UIManager : Singleton<UIManager>
    {

        private Dictionary<EnumViewType, GameObject> m_dicOpenUIs = null;

        private Stack<UIInfoData> m_stackOpenUIs = null;

        public override void Init()
        {
            m_dicOpenUIs = new Dictionary<EnumViewType, GameObject>();
            m_stackOpenUIs = new Stack<UIInfoData>();
        }

        #region GetUIScript & GetUIObject
        public T GetUIComponent<T>(EnumViewType _uiType) where T : BaseUI
        {
            GameObject _retObj = GetUIObject(_uiType);
            if(_retObj != null)
            {
                return _retObj.GetComponent<T>();
            }
            return null;
        }

        public GameObject GetUIObject(EnumViewType _uiType)
        {
            GameObject _retObj = null;
            if(!m_dicOpenUIs.TryGetValue(_uiType, out _retObj))
            {
                Debug.LogError("_dicOpenUis TryGetValue Failure! _uiType : " + _uiType.ToString());
            }
            return _retObj;
        }

        #endregion

        #region Preload UI Prefab
        public void PreloadUI(EnumViewType[] _uiTypes)
        {
            for(int i = 0; i < _uiTypes.Length; i++)
            {
                PreloadUI(_uiTypes[i]);
            }
        }

        public void PreloadUI(EnumViewType _uiType)
        {
            string _path = UIPathDefines.GetPrefabPathByType(_uiType);
            ResourceManager.Instance.LoadCoroutine(_path,null);
        }
        #endregion

        #region OpenUI

        public void OpenUI(EnumViewType[] _uiTypes)
        {
            OpenUI(false, _uiTypes, null);
        }

        public void OpenUI(EnumViewType _uiType, params object[] _uiParams)
        {
            EnumViewType[] _uiTypes = new EnumViewType[1];
            _uiTypes[0] = _uiType;
            OpenUI(false, _uiTypes, _uiParams);
        }

        public void OpenUICloseOthers(EnumViewType[] _uiTypes)
        {
            OpenUI(true, _uiTypes, null);
        }


        public void OpenUI(bool _isCloseOthers, EnumViewType[] _uiTypes, params object[] _uiParams)
        {
            if(_isCloseOthers)
            {
                CloseUIAll();
            }

            //Push _uiTypes in Stack;
            for(int i = 0; i < _uiTypes.Length; i++)
            {
                EnumViewType _uiType = _uiTypes[i];
                if(!m_dicOpenUIs.ContainsKey(_uiType))
                {
                    string _path = UIPathDefines.GetPrefabPathByType(_uiType);
                    m_stackOpenUIs.Push(new UIInfoData(_uiType, _path, _uiParams));
                }
            }

            //Open UI
            if(m_stackOpenUIs.Count > 0)
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
                _uiInfoData = m_stackOpenUIs.Pop();
                _uiObject = ResourceManager.Instance.LoadInstance(_uiInfoData.Path) as GameObject;
                BaseUI _baseUI = _uiObject.GetComponent<BaseUI>();
                if (_uiObject != null)
                {                 
                    if(_baseUI == null)
                    {
                        _baseUI = _uiObject.AddComponent(_uiInfoData.ScriptType) as BaseUI;
                        _baseUI.SetUIWhenOpening(_uiInfoData.UIParams);
                    }
                }
                else
                {
                    //打开时设置参数
                    _baseUI.SetUIWhenOpening(_uiInfoData.UIParams);
                }
                m_dicOpenUIs.Add(_uiInfoData.UIType, _uiObject);

            } while (m_stackOpenUIs.Count > 0);

            yield return 0;
        }
        #endregion

        #region CloseUI

        public void CloseUI(EnumViewType _uiType)
        {
            GameObject _uiObj = null;
            if(!m_dicOpenUIs.TryGetValue(_uiType, out _uiObj))
            {
                Debug.LogError("_dicOpenUIs TryGetValue Failure! _uiType : " + _uiType.ToString());
                return;
            }
            CloseUI(_uiType, _uiObj);
        }

        public void CloseUI(EnumViewType[] _uiTypes)
        {
            for(int i = 0; i < _uiTypes.Length; i++)
            {
                CloseUI(_uiTypes[i]);
            }
        }

        public void CloseUI(EnumViewType _uiType, GameObject _uiObj)
        {
            if(_uiObj == null)
            {
                m_dicOpenUIs.Remove(_uiType);
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
                    m_dicOpenUIs.Remove(_uiType);
                    GameObject.Destroy(_uiObj);                
                }
            }
        }

        public void CloseUIAll()
        {
            List<EnumViewType> _keyList = new List<EnumViewType>(m_dicOpenUIs.Keys);
            foreach(EnumViewType _uiType in _keyList)
            {
                GameObject _uiObj = m_dicOpenUIs[_uiType];
                CloseUI(_uiType, _uiObj);
            }
            m_dicOpenUIs.Clear();
        }

        private void _CloseUIHandler(object _sender, EnumObjectState _newState, EnumObjectState _oldState)
        {
            if(_newState == EnumObjectState.Closing)
            {
                BaseUI _baseUI = _sender as BaseUI;
                m_dicOpenUIs.Remove(_baseUI.ViewType);
                _baseUI.StateChanged -= _CloseUIHandler;
            }
        }

        #endregion

    }
}

