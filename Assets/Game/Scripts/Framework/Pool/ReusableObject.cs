//抽象类，不能实例化，但能声明（只能被继承）
public abstract class ReUsableObject : IReusable
{
    public abstract void OnSpawn();

    public abstract void OnUnSpawn();
}