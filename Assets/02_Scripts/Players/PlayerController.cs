using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //���� ���� ����
    [Header("���ݰ���")]
    public long att = 10;
    public float dex = 1;
    public float cri = 1;
    public long cirAtt;
    [Header("��������")]
    public long hp = 100;
    public long maxHp = 100;
    public float def = 1;
    public Image hp_bar;

    //�˸� ǥ���� Text
    public Text noti;
    public Text attackTxt;//���ݷ�
    public Text hpTxt;//ü��
    public Text defTxt;//����
    public Text dexTxt;//��ø��
    public Text criTxt;//ũ��Ƽ��Ȯ��

    //private ����
    GameObject mob = null;
    Animator anim;
    float currentTime = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        maxHp = hp;
        currentTime = Time.time;
        UpdateUI();
    }

    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            anim.SetBool("att", false);
        }
        else
        {
            //���� ����
            if (currentTime + dex < Time.time)
            {
                //���� �ӵ� ����
                currentTime = Time.time;
                //���� �ӵ��� �°� �ִϸ��̼� ���
                anim.SetBool("att", true);
                //ũ��Ƽ�� Ȯ�� 1% ���ϰ� ���׷��̵�� 0.1%�� ����
               if(Random.Range(1, 101) <= cri)
                {
                    Critical();
                }
                else
                {
                    mob.GetComponent<Monster>().Damage(att);
                }
            }
        }
        //�ʱ� ��ø��
        anim.speed = dex;
    }

    void UpdateUI()
    {
        attackTxt.text = "���� ���ݷ� : " + att;
        hpTxt.text = "���� ü�� : " + hp;
        defTxt.text = "���� ���� : " + def;
        dexTxt.text = "���� ��ø�� : " + dex;
        criTxt.text = "���� ġ��Ÿ Ȯ�� : " + cri + " %";
    }

    //ũ��Ƽ�� ������
    public void Critical()
    {
        float ciritical = 1.5f;
        cirAtt = att * (long)ciritical;
        mob.GetComponent<Monster>().CriDamage(cirAtt);
    }

    public void Damage(long monAtt)
    {
        hp_bar.fillAmount = hp / maxHp;

        hp -= (monAtt - (long)(def / 1000));

        if (hp <= 0)
        {
            noti.text = "���ݼӵ��� ������ ���� �Ǿ����ϴ�.";
            dex = 1;
            hp = maxHp;
            Die();
        }
        else
        {
            noti.text = "";
        }
    }

    //������ ���׷��̵�
    public void AttackUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("���� ����");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            att += 5;
            attackTxt.text = "���� ���ݷ� : " + att;
        }
    }

    //ü�� ���׷��̵�
    public void HpUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("���� ����");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            hp += 100;
            if (hp >= maxHp)
            {
                maxHp = hp;
            }

            hpTxt.text = "���� ü�� : " + hp;
        }
    }

    //���� ���׷��̵�
    public void DefUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("���� ����");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            def += 1;
            defTxt.text = "���� ���� : " + def;
        }
    }

    //���� ���׷��̵�
    public void DexUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("���� ����");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            dex += 0.1f;

            GameManager.instance.gameSpeed += (dex);
            dexTxt.text = "���� ��ø�� : " + dex;
        }
    }

    //ũ��Ƽ�� Ȯ�� ���׷��̵�
    public void CriticalUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("���� ����");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            cri += 0.1f;
            criTxt.text = "���� ġ��Ÿ Ȯ�� : " + cri + " %";
        }
    }
    

    void Die()
    {
        GameManager.instance.isPlay = false;
        anim.SetTrigger("dead");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.isPlay = false;
            mob = collision.gameObject;
        }
    }
}

