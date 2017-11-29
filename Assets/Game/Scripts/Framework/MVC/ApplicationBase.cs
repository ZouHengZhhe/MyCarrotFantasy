using System;
using UnityEngine;

public class ApplicationBase<T> : Singleton<T>
    where T : MonoBehaviour
{
    //注册控制器
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName)
    {
        MVC.SendEvent(eventName);
    }
}