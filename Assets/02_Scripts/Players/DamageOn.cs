using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOn : MonoBehaviour
{
    public GameObject prefabDamage;//canvas


    public void DamageTxt()
    {
        //����, ��ũ��Ʈ�� ���Ϳ� ����
        GameObject inst = Instantiate(prefabDamage, transform);//�̷��Ը� �ص� �ڽ����� ���� ����


        //inst.transform.SetParent(transform); << �̷��Ե� ����
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
