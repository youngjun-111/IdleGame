using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //���� ���� ����
    [Header("���ݰ���")]
    public long att = 1000;
    public float dex = 1000;
    public float cri = 1000;
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
    public Text creTxt;//ũ��Ƽ��Ȯ��

    //private ����
    GameObject mob = null;
    Animator anim;
    float currentTime = 0;
    void Start()
    {
        anim = GetComponent<Animator>();


        maxHp = hp;

        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            anim.SetBool("att", false);
        }else
        {
            //���� ����
            if(currentTime + (2 - (dex * 0.001f)) < Time.time)
            {
                currentTime = Time.time;
                anim.SetBool("att", true);
                mob.GetComponent<Monster>().Damage(att);
                int criRan = Random.Range(1, 101);
                if(criRan < (cri * 0.01f))
                {
                    mob.GetComponent<Monster>().CriDamage(att * criRan);
                }
            }
        }
        //��ø
        anim.speed = dex * 0.001f;
    }

    public void Damage(long monAtt)
    {
        hp_bar.fillAmount = hp/maxHp;

        hp -= (monAtt - (long)(def / 1000));

        if (hp <= 0)
        {
            noti.text = "ü���� 0�� �Ǹ� ���ݼӵ��� ������ ���� �˴ϴ�.";
            dex = 500;
            hp = 0;
            Die();
        }else
        {
            noti.text = "";
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
