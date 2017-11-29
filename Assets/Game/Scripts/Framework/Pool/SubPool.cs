using System.Collections.Generic;
using UnityEngine;

//子对象池
public class SubPool
{
    //预设
    private GameObject m_prefab;

    //集合列表（用于装所有生成的预设体）
    private List<GameObject> m_objects = new List<GameObject>();

    //对象池名字（即对象池中预制体的名字，用于在总对象池中定义不同子对象池）
    public string Name
    {
        get { return m_prefab.name; }
    }

    //构造(构造时确定预制体)
    public SubPool(GameObject prefab)
    {
        this.m_prefab = prefab;
    }

    //取对象
    public GameObject Spawn()
    {
        GameObject obj = null;  //存储从对象池中取到的对象

        //看子对象池中是否有多余的对象提取
        foreach (GameObject go in m_objects)
        {
            if (!go.activeInHierarchy)
            {
                obj = go;
                break;
            }
        }

        if (obj == null)  //未从对象池中取到对象（表示对象池中没有多余的对象）
        {
            obj = GameObject.Instantiate(m_prefab);
            m_objects.Add(obj);
        }

        obj.SetActive(true);
        return obj;
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        //首先要确定要回收的该对象是否在对象池中
        if (m_objects.Contains(go))
        {
            go.SetActive(false);
        }
    }

    //回收所有对象
    public void UnspawnAll()
    {
        foreach (GameObject obj in m_objects)
        {
            obj.SetActive(false);
        }
    }

    //是否包含对象
    public bool Contains(GameObject go)
    {
        return m_objects.Contains(go);
    }
}