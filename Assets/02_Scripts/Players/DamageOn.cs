using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOn : MonoBehaviour
{
    public GameObject prefabDamage;//canvas������ �Ϲ� ������
    public GameObject criDamage;//ũ��Ƽ�� ������ canvas

    public void DamageTxt()
    {
        //����, ��ũ��Ʈ�� ���Ϳ� ����
        GameObject inst = Instantiate(prefabDamage, transform);//�̷��Ը� �ص� �ڽ����� ���� ����
        //inst.transform.SetParent(transform); //<<�̷��Ե� ����
    }
    public void CriDamageTxt()
    {
        GameObject inst = Instantiate(criDamage, transform);
        //inst.transform.SetParent(transform);
    }
}
