using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //공격 관련 변수
    [Header("공격관련")]
    public long att = 10;
    public float dex = 1;
    public float cri = 1;
    public long cirAtt;
    float criDamage = 1.5f;
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
    public Text criTxt;//크리티컬확률

    //private 변수
    GameObject mob = null;
    Animator anim;
    float currentTime = 0;
    //일시적으로 값을 올렸다가 다시 빼줘야함
    public List<Buff> onBuff = new List<Buff>();

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
            if(anim.GetBool("att") == false)
            {
                anim.speed = 1f;
            }
        }
        else
        {
            //공속 조정
            if (currentTime + dex < Time.time)
            {
                //공격 속도 설정
                currentTime = Time.time;
                //공격 속도에 맞게 애니메이션 재생
                anim.SetBool("att", true);
                //초기 민첩성
                anim.speed = dex;
                //크리티컬 확률 1% 로하고 업그레이드시 0.1%씩 증가
                if (Random.Range(1, 101) <= cri)
                {
                    Critical();
                }
                else
                {
                    mob.GetComponent<Monster>().Damage(att);
                }
            }
        }
    }

    void UpdateUI()
    {
        attackTxt.text = "현재 공격력 : " + att;
        hpTxt.text = "현재 체력 : " + hp;
        defTxt.text = "현재 방어력 : " + def;
        dexTxt.text = "현재 민첩성 : " + dex;
        criTxt.text = "현재 치명타 확률 : " + cri + " %";
    }

    //크리티컬 데미지
    public void Critical()
    {
        cirAtt = att * (long)criDamage;
        mob.GetComponent<Monster>().CriDamage(cirAtt);
    }

    public void Damage(long monAtt)
    {
        hp_bar.fillAmount = hp / maxHp;

        hp -= (monAtt - (long)def);

        if (hp <= 0)
        {
            noti.text = "공격속도가 최저로 리셋 되었습니다.";
            dex = 1;
            hp = maxHp;
            Die();
        }
        else
        {
            noti.text = "";
        }
    }

    #region 버프관련
    //버프 적용 함수
    public float BuffChange(string type, float origin)
    {
        //리스트에 버프가 담기면...
        if(onBuff.Count > 0)
        {
            float temp = 0;
            //버프수 만큼 반복을 통해 버프를 (버프의 타입이 같다면) 누적 시킨다.
            for(int i = 0; i < onBuff.Count; i++)
            {
                //버프의 타입이 (매개변수와)같다면
                if (onBuff[i].type.Equals(type))
                {
                    temp += origin * onBuff[i].percentage;
                }
            }
            //누적된 값을 반환
            return origin + temp;
        }
        //버프가 끝나면(버프가 없다면) 기본값으로 돌려준다.
        else
        {
            return origin;
        }
    }
    //버프값을 빼주는 함수
    public float mBuffChange(string type, float origin)
    {
        if(onBuff.Count > 0)
        {
            float temp = 0;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if (onBuff[i].type.Equals(type))
                {
                    temp += origin * onBuff[i].percentage;
                }
            }
            return origin - temp;
        }
        else
        {
            return origin;
        }
    }

    //버프의 타입에 따른 공격력(att), 민첩(dex)에 버프 적용 시키는 함수
    public void ChooseBuff(string type)
    {
        switch (type)
        {
            case "Atk":
                att = (long)BuffChange(type, att);
                break;
            case "dex":
                dex = BuffChange(type, dex);
                break;
        }
    }
    //버프의 타입에 따른 공격력 민첩 버프를 다시 빼주는 함수
    public void MinusBuff(string type)
    {
        switch (type)
        {
            case "Atk":
                att = (long)mBuffChange(type, att);
                break;
            case "dex":
                dex = mBuffChange(type, dex);
                break;
        }
    }
    #endregion

    #region 업그레이드

    //데미지 업그레이드
    public void AttackUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("돈이 부족");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            att += 5;
            attackTxt.text = "현재 공격력 : " + att;
        }
    }

    //체력 업그레이드
    public void HpUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("돈이 부족");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            hp += 100;
            if (hp >= maxHp)
            {
                maxHp = hp;
            }

            hpTxt.text = "현재 체력 : " + hp;
        }
    }

    //방어력 업그레이드
    public void DefUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("돈이 부족");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            def += 1;
            defTxt.text = "현재 방어력 : " + def;
        }
    }

    //덱스 업그레이드
    public void DexUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("돈이 부족");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            dex += 0.01f;
            GameManager.instance.gameSpeed += (dex);
            dexTxt.text = "현재 민첩성 : " + dex;
        }
    }

    //크리티컬 확률 업그레이드
    public void CriticalUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("돈이 부족");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            cri += 0.1f;
            criTxt.text = "현재 치명타 확률 : " + cri + " %";
        }
    }
    #endregion

    void Die()
    {
        GameManager.instance.isPlay = false;
        anim.SetTrigger("dead");
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.isPlay = false;
            mob = collision.gameObject;
        }
    }
}

