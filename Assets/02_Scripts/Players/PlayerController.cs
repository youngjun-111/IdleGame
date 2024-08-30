using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //공격 관련 변수
    [Header("공격관련")]
    public long att = 1000;
    public float dex = 1000;
    public float cri = 1000;
    [Header("생존관련")]
    public long hp = 100;
    public long maxHp = 100;
    public float def = 1;
    public Image hp_bar;

    //알림 표시할 Text
    public Text noti;
    public Text attackTxt;//공격력
    public Text hpTxt;//체력
    public Text defTxt;//방어력
    public Text dexTxt;//민첩성
    public Text creTxt;//크리티컬확률

    //private 변수
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
            //공속 조정
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
        //민첩
        anim.speed = dex * 0.001f;
    }

    public void Damage(long monAtt)
    {
        hp_bar.fillAmount = hp/maxHp;

        hp -= (monAtt - (long)(def / 1000));

        if (hp <= 0)
        {
            noti.text = "체력이 0이 되면 공격속도가 최저로 리셋 됩니다.";
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
