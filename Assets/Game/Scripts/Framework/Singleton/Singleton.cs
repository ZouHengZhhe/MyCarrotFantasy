using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单例类基类（抽象类、泛型，其他只需继承此类即可成为单例类）
//继承该类的，相当于一个单例组件，如UIManager等
public abstract class Singleton<T> : MonoBehaviour
    where T:MonoBehaviour
{
    private static T m_instance = null;

    public static T Instance
    {
        get { return m_instance; }
    }

    protected virtual void Awake()
    {
        m_instance = this as T;
    }

}
