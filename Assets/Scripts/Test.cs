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
    }
	
	private void _TestButton(GameObject _listener, object _args, params object[] _params)
    {
        int i = (int)_params[0];
        string s = (string)_params[1];
        Debug.Log(i);
        Debug.Log(s);
    }
}
