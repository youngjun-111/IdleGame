using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClickBuffUse : MonoBehaviour
{
    public string type;
    public float per;
    public float du;
    public Sprite icon;

    public void UseBuff()
    {
        int buffCount = GameObject.Find("Player").GetComponent<PlayerController>().onBuff.Count;
        if(buffCount >= 5)
        {
            return;
        }
        BuffManager.instance.CreateBuff(type, per, du, icon);
    }
}
