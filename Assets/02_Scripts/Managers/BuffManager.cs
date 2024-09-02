using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    //�̱���
    public static BuffManager instance;

    private void Awake()
    {
        instance = this;
    }

    //�������� ���������� ������
    public GameObject buffPrefab;

    public void CreateBuff(string type, float per, float du, Sprite icon)
    {
        //�Ű� ���� Ÿ�Կ� ���� �� per, du, icon ���� �������� �Լ�
        //��ư�� ������ �� ����âǥ�� �гο��� �ش� ��ư�� ������ �ִ� type�� per,du, icon ���� ����Ǽ� ȭ��󿡼� ������ ���ƴٴ°� ǥ�����ֱ� ���� BuffManager�ȿ� �ִ� CreateBuff �Լ�

        //buffPrefab����
        GameObject go = Instantiate(buffPrefab, transform);
        //buff ��ũ��Ʈ�� ������ �ʱ�ȭ �� ���� ����
        go.GetComponent<Buff>().Init(type, per, du);
        //��������Ʈ ������ ����
        go.GetComponent<Image>().sprite = icon;
    }
}
