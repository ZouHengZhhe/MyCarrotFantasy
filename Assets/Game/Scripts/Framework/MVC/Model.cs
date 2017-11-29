public abstract class Model
{
    public abstract string Name { get; } //每个Model都有自己的名字

    protected void SendEvent(string eventName, object data = null) //发送事件函数
    {
        MVC.SendEvent(eventName, data);
    }
}