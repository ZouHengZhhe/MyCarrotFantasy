using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSendMessage : MonoBehaviour
{
    public Button btn;

    void Start()
    {
        btn=this.transform.GetChild(0).GetComponent<Button>();
        print("按钮的名字："+btn.name);
        btn.onClick.AddListener(OnBtnClick);
    }

    void OnBtnClick()
    {
        print("点击按钮！");
        this.gameObject.SendMessage("A","nihao" ,SendMessageOptions.DontRequireReceiver);
    }

    void A(string str,string str1)
    {
        print(str+"  "+str1);
    }

}
