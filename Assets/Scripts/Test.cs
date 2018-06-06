/*
 *
 *		Title:测试脚本
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
using UnityEngine.UI;
using Framework;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Test Load
        //float time = System.Environment.TickCount;
        //for (int i = 1; i < 1000; i++)
        //{
        //GameObject go = null;
        //1
        //go = ResourceManager.Instance.LoadInstance("TestCube") as GameObject;
        //go.transform.position = UnityEngine.Random.insideUnitSphere * 20;

        //2
        //ResourceManager.Instance.LoadAsyncInstance("TestCube",
        //    (_obj) =>
        //    {
        //        go = _obj as GameObject;
        //        go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
        //    });

        //3
        //ResourceManager.Instance.LoadCoroutineInstance("TestCube",
        //    (_obj) =>
        //    {

        //        go = _obj as GameObject;
        //        go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
        //    });

        //}
        //Debug.Log("Times： " + (System.Environment.TickCount - time) * 1000);

        //Test Button
        //EventTriggerListener _etl = EventTriggerListener.Get(GameObject.Find("TestButton"));
        //_etl.SetEventHandle(EnumTouchEventType.OnClick, _TestButton, 1, "TestButton");

        //Test Message
        //MessageCenter.Instance.AddListener(EnumMessageType.TestSendMessage.ToString(), _TestGetMessage);
        //StartCoroutine("_TestSendMessage");

        UIManager.Instance.OpenUI(EnumUIType.TestOne);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.CloseUI(EnumUIType.TestOne);
        }
    }

    private void _TestButton(GameObject _listener, object _args, params object[] _params)
    {
        int i = (int)_params[0];
        string s = (string)_params[1];
        Debug.Log(i);
        Debug.Log(s);
    }

    private IEnumerator _TestSendMessage()
    {
        int i = 0;
        while(true)
        {
            i++;
            yield return new WaitForSeconds(1.0f);
            Message _message = new Message(EnumMessageType.TestSendMessage.ToString(), this);
            _message["args"] = i;
            _message.Send();
        }
    }

    private void _TestGetMessage(Message _message)
    {
        int i = (int)_message["args"];
        GameObject.Find("TestText").GetComponent<Text>().text = "测试数据为:" + i;
    }
}
