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
using Framework;

public class TestTwo : BaseUI
{
    public override EnumViewType ViewType
    {
        get
        {
            return EnumViewType.TestTwo;
        }
    }

    public override void HandleEvent(EnumMVCEventType _eventType, object _data)
    {
        
    }
}
