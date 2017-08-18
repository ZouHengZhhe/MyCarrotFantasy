using System;
using System.Collections.Generic;
using UnityEngine;

//抽象类
public abstract class ReusableObject<T> : MonoBehaviour, IReusable<T>
{
    public abstract void OnSpawn();

    public abstract void UnOnSpawn();
}
