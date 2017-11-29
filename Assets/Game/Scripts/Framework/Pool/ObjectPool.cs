using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 总对象池（包含多个子对象池,单例）
/// </summary>
public class ObjectPool : Singleton<ObjectPool>
{
    //装所有子对象池的字典
    private Dictionary<string, SubPool> m_Pools = new Dictionary<string, SubPool>();

    /// <summary>
    /// 取对象(通过传入的名字取到特定对象)
    /// </summary>
    /// <param name="name">预制体的名字</param>
    /// <returns></returns>
    public GameObject Spawn(string name)
    {
        //先判断对象池中是否有该对象的对象池(如果没有对象池，先创建对象池，然后进行下一步，如果有对象池，直接进行下一步)
        if (!m_Pools.ContainsKey(name))
        {
            RegisterNew(name);
        }

        //从该对象的对象池中去对象
        SubPool pool = m_Pools[name];
        return pool.Spawn();
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        SubPool pool = null;
        foreach (SubPool p in m_Pools.Values)
        {
            if (p.Contains(go))
            {
                pool = p;
                break;
            }
        }
        pool.Unspawn(go);
    }

    //回收所有对象
    public void UnspawnAll(GameObject go)
    {
        foreach (SubPool p in m_Pools.Values)
        {
            p.UnspawnAll();
        }
    }

    //创建新的子对象池
    private void RegisterNew(string name)
    {
        //预设路径
        string path = Application.dataPath + "ReSources/" + name;

        //加载预设
        GameObject prefab = Resources.Load<GameObject>(path);

        //创建子对象池
        SubPool pool = new SubPool(prefab);
        m_Pools.Add(pool.Name, pool);
    }
}