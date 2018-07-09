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
    public class SoundManager :  DDOLSingleton<SoundManager>
    {

        private AudioSource m_bgSound;
        private AudioSource m_effectSound;

        private string m_resourceDir = "";

        public string ResourceDir
        {
            get { return m_resourceDir; }
            set { m_resourceDir = value; }
        }

        public float BgVolume
        {
            get { return m_bgSound.volume; }
            set { m_bgSound.volume = value; } 
        }

        public float EffectVolume
        {
            get { return m_effectSound.volume; }
            set { m_effectSound.volume = value; }
        }

        private void Awake()
        {
            m_bgSound = this.gameObject.AddComponent<AudioSource>();
            m_bgSound.playOnAwake = false;
            m_bgSound.loop = true;

            m_effectSound = this.gameObject.AddComponent<AudioSource>();
        }

        public void PlayBg(string _audioName)
        {
            string _oldName;
            if(m_bgSound.clip == null)
            {
                _oldName = "";
            }
            else
            {
                _oldName = m_bgSound.clip.name;
            }
            
            if(_oldName != _audioName)
            {
                string _path;
                if(string.IsNullOrEmpty(ResourceDir))
                {
                    Debug.LogWarning("Dont set BgSound ResourcePath!");
                    _path = _audioName;
                }
                else
                {
                    _path = ResourceDir + "/" + _audioName;
                }

                AudioClip _clip = Resources.Load<AudioClip>(_path);

                if(_clip != null)
                {
                    m_bgSound.clip = _clip;
                    m_bgSound.Play();
                }
            }
        }

        public void StopBg()
        {
            m_bgSound.Stop();
            m_bgSound.clip = null;
        }

        public void PlayEffect(string _audioName)
        {
            string _path;
            if (string.IsNullOrEmpty(ResourceDir))
            {
                Debug.LogWarning("Dont set EffectSound ResourcePath!");
                _path = _audioName;
            }
            else
            {
                _path = ResourceDir + "/" + _audioName;
            }

            AudioClip _clip = Resources.Load<AudioClip>(_path);

            m_effectSound.PlayOneShot(_clip);
        }
    }
}

