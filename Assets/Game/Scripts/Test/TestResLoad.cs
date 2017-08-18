using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResLoad : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //GameObject go = Resources.Load<GameObject>("MyCube");
        GameObject go = Resources.Load<GameObject>("MyCube");
        GameObject obj=GameObject.Instantiate(go);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
