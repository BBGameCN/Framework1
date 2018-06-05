/*
 *
 *		Title:处理单例模板错误
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

using System;

namespace Framework
{
    public class SingletonException : System.Exception
    {
        public SingletonException(string msg) : base(msg)
        {

        }
    }
}

