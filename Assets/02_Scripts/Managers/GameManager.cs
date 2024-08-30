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
        float duration = 1f;//ī���ÿ� �ɸ��� �ð�
        float offset = (target - current) / duration;//1000/0.5 = 2000

        while(current < target)
        {
            current += offset * Time.deltaTime;

            moneyText.text = string.Format("{0:n0}", (int)current);
            yield return null;
        }

        current = target;

        moneyText.text = string.Format("{0:n0}", (int)current);

    }


    public void SetMoney(long Money)
    {
        money += Money;
        StartCoroutine(Count(money, money - Money));
    }
}
