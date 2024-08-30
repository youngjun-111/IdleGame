using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public long att = 10;
    Animator anim;
    float currentTime = 0;

    public long hp = 100;
    public long maxHp = 100;
    public Image hp_bar;


    GameObject mob = null;
    // Start is called before the first frame update
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
            if(currentTime + 0.5f < Time.time)
            {
                currentTime = Time.time;
                anim.SetBool("att", true);
                mob.GetComponent<Monster>().Damage(att);
            }
        }
    }

    public void Damage(long monAtt)
    {
        hp -= monAtt;

        hp_bar.fillAmount = hp/maxHp;

        if (hp <= 0)
        {
            Die();
        }else
        {
            
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
