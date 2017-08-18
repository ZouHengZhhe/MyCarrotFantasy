using System;
using System.Collections.Generic;

//对象池接口
public interface IReusable<T>
{
    //当取出时调用
    void OnSpawn();

    //当回收时调用
    void UnOnSpawn();
}
