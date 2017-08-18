using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//总对象池类
public class ObjectPool : Singleton<ObjectPool>
{
    public string ResourceDir = "";

    //装所有子对象池的字典,Key为子对象池的名字标识
    public Dictionary<string, SubPool> m_Pools = new Dictionary<string, SubPool>();

    /// <summary>
    /// 取对象
    /// </summary>
    /// <param name="name">子对象池的名字标识</param>
    /// <returns></returns>
    public GameObject Spawn(string name)
    {
        if (!m_Pools.ContainsKey(name))
        {
            RegisterNew(name);
        }
        SubPool pool = m_Pools[name];
        return pool.Spawn();

    }

    //回收对象
    public void UnSpawn(GameObject go)
    {
        SubPool p = null;
        foreach (SubPool pool in m_Pools.Values)
        {
            if (pool.Contains(go))
            {
                p = pool;
                break;
            }
        }
        p.UnSpawn(go);

    }

    //回收所有对象
    public void UnSpawn()
    {
        foreach (SubPool pool in m_Pools.Values)
        {
            pool.UnSpawnAll();
        }
    }

    /// <summary>
    /// 创建新的子对象池
    /// </summary>
    /// <param name="name">ReSource文件夹中的预制体的名字</param>
    void RegisterNew(string name)
    {
        //预设路径(ReSource文件夹中的预制体的路径)
        string path = "";
        if (string.IsNullOrEmpty(ResourceDir))
        {
            path = name;
        }
        else
        {
            path = ResourceDir + "/" + name;
        }

        //加载预设
        GameObject prefab = Resources.Load<GameObject>(path);

        SubPool pool = new SubPool(prefab);
        m_Pools.Add(pool.Name, pool);
    }

}
