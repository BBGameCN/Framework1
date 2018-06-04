/*
 *
 *		Title:测试脚本
 *
 *		Description:
 *
 *		Author: yhb
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
