using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public string type;
    public float percentage;
    public float duration;
    public float currentTime;
    public Image icon;

    PlayerController player;
    private void Awake()
    {
        icon = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //�ٸ������� �ʱ�ȭ �� �� �ִ� �Լ�
    public void Init(string type, float per, float du)
    {
        this.type = type;
        percentage = per;
        duration = du;
        currentTime = duration;
        icon.fillAmount = 1;
        Execute();
    }
    
    //������ �����ų �Լ�
    public void Execute()
    {
        //�÷��̾ ���� ȿ�� ����
        player.onBuff.Add(this);
        player.ChooseBuff(type);
        //�Ŀ� �ڷ�ƾ ����
        StartCoroutine(Activation());
    }

    //����Ȱ�� �ڷ�ƾ
    //�ڷ�ƾ���� ����� ���ð�
    //��°ſ� ��� �ð��� ���� �����ٷ��� WaitForSeconds�� ���� ��������
    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    IEnumerator Activation()
    {
        //0.1�ʿ� 0.1��ŭ icon.fillAmount�� ����
        while(currentTime > 0)
        {
            //�ʾ��Ʈ�� 0.1�ʸ��� 0.1�� �����
            currentTime -= 0.1f;
            //���� �پ��� ǥ��
            icon.fillAmount = currentTime / duration;
            yield return seconds;//0.1����
        }
        icon.fillAmount = 0;
        currentTime = 0;

        DeActivation();
    }

    //������ ������ ó���� �Լ�
    public void DeActivation()
    {
        //������ ���ش�����
        player.onBuff.Remove(this);
        player.ChooseBuff(type);
        //���� ��Ű��
        Destroy(gameObject);
    }
}
