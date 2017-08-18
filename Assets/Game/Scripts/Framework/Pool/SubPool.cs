using System;
using System.Collections.Generic;
using UnityEngine;

//子对象池类(子对象池的名字标识和该子对象池中的预制体的名字相同)
public class SubPool
{
    //预设
    public GameObject m_prefab;

    //集合
    public List<GameObject> m_objects=new List<GameObject>();

    //子对象池的名字标识
    public string Name
    {
        get { return m_prefab.name; }
    }

    //构造
    public SubPool(GameObject prefab)
    {
        this.m_prefab = prefab;
    }

    //取对象
    public GameObject Spawn()
    {
        GameObject go = null;

        //如果集合中有多余的对象，给go赋值(m_objects中有子对象在Hierarchy面板中隐藏，即有多余对象)
        foreach (GameObject obj in m_objects)
        {
            if (!obj.activeInHierarchy)
            {
                go = obj;
                break;
            }
        }

        //依然未为go赋值（m_objects中无子对象在Hierarchy面板中隐藏，即无多余对象）
        if (go==null)
        {
            go=GameObject.Instantiate(m_prefab);
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver); //发消息，已取出对象
        return go;

    }

    //回收对象
    public void UnSpawn(GameObject go)
    {
        if (Contains(go))
        {
            go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //回收所有对象
    public void UnSpawnAll()
    {
        foreach (GameObject obj in m_objects)
        {
            if (obj.activeInHierarchy)
            {
                UnSpawn(obj);
            }
        }
    }

    //是否包含对象
    public bool Contains(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
