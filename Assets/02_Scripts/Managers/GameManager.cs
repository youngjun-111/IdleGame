using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameSpeed = 3f;
    public bool isPlay = true;//���� ����ĥ��� ����

    public long money = 1000;
    public Text moneyText;

    public static GameManager instance;
    void Start()
    {
        instance = this;//�̱���
        moneyText.text = money.ToAttackString() + " : Gold";
    }

    void Update()
    {
        if (isPlay)
        {
            gameSpeed = 3f;
        }
        else//���� ���֫��� ���
        {
            gameSpeed = 0f;
        }
    }

    //���� ī���� �ִϸ��̼�
    IEnumerator Count(float target, float current)
    {
        //ī���ÿ� �ɸ��� �ð�
        float duration = Mathf.Lerp(0.2f, 0f, 0.2f);
        //float duration = 0.2f;
        // �� �����Ӹ��� ������ų �ݾ� ���
        float offset = (target - current) / duration;

        while(current < target)
        {
            //���� �ݾ��� ������ ���ݿ� ���� ����
            current += offset * Time.deltaTime; 
            //���� �ݾ��� �ݿø��Ͽ� õ ���� ���� ��ȣ�� �Բ� ǥ��
            moneyText.text = string.Format("{0:n0}", (int)current) + " : Gold";
            yield return null;
        }
        //���� ���� ��, ���� �ݾ��� ��ǥ �ݾ����� ������
        current = target;
        //��ǥ �ݾ��� �ݿø��Ͽ� õ ���� ���� ��ȣ�� �Բ� �ݾ��� ��ǥ �ݾ����� ����
        moneyText.text = string.Format("{0:n0}", (int)current) + " : Gold";

    }

    public void SetMoney(long Money)
    {
        // ���� ������ �ݾ׿� �߰� �ݾ��� ����
        money += Money;
        // ������ �ݾ����� ī���� �ڷ�ƾ ����
        StartCoroutine(Count(money, money - Money));
    }
}
