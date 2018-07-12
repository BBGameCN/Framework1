/*
 *
 *		Title: UI基类
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
    public abstract class BaseUI : View
    {
        #region Cache gameObject & transform

        private Transform m_cachedTransform;

        public Transform CachedTransform
        {
            get
            {
                if(m_cachedTransform == null)
                {
                    m_cachedTransform = this.transform;
                }
                return m_cachedTransform;
            }
        }

        private GameObject m_cachedGameObject;

        public GameObject CachedGameObject
        {
            get
            {
                if(m_cachedGameObject == null)
                {
                    m_cachedGameObject = this.gameObject;
                }
                return m_cachedGameObject;
            }
        }

        #endregion

        #region UIType & EnumObjectState

        protected EnumObjectState m_state = EnumObjectState.None;

        public event StateChangedEvent StateChanged;

        public EnumObjectState State
        {
            protected set
            {
                if(m_state != value)
                {
                    EnumObjectState _oldState = m_state;
                    m_state = value;
                    if(StateChanged != null)
                    {
                        StateChanged(this, m_state, _oldState);
                    }
                }
            }

            get
            {
                return m_state;
            }
        }

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            this.State = EnumObjectState.Initial;
            this.CachedTransform.SetParent(GameObject.Find("Canvas").transform, false);
            _OnAwake();
        }

        private void Start()
        {
            _OnStart();
        }

        private void Update()
        {
            if(this.State == EnumObjectState.Ready)
            {
                _OnUpdate();
            }
        }


        #endregion


        /// <summary>
        /// UI层级置顶
        /// </summary>
        protected virtual void _SetDepthToTop()
        {
            this.CachedTransform.SetAsLastSibling();
        }

        public void Release()
        {
            this.State = EnumObjectState.Closing;
            GameObject.Destroy(CachedGameObject);
            _OnRelease();
        }

        #region Life Cycle
        protected virtual void _OnAwake()
        {
            

        }

        protected virtual void _OnStart()
        {
            this._OnPlayOpenUIAudio();
            this.State = EnumObjectState.Loading;
        }

        protected virtual void _OnUpdate()
        {

        }

        protected virtual void _OnRelease()
        {
            this._OnPlayCloseUiAudio();
        }

        #endregion

        /// <summary>
        /// 播放打开界面音乐
        /// </summary>
        protected virtual void _OnPlayOpenUIAudio()
        {

        }

        /// <summary>
        /// 播放关闭界面音乐
        /// </summary>
        protected virtual void _OnPlayCloseUiAudio()
        {

        }

        protected virtual void SetUI(params object[] _uiParams)
        {
            this.State = EnumObjectState.Loading;
        }

        public virtual void SetUIparam(params object[] _uiParams)
        {

        }

        protected virtual void _OnLoadData()
        {

        }

        public void SetUIWhenOpening(params object[] _uiParams)
        {
            SetUI(_uiParams);
            StartCoroutine(this._AsyncOnLoadData());
        }

        private IEnumerator _AsyncOnLoadData()
        {
            yield return null;
            if(this.State == EnumObjectState.Loading)
            {
                this._OnLoadData();
                this.State = EnumObjectState.Ready;
            }
        }

    }
}

