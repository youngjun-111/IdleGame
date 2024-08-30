using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public long att = 10;
    public long hp = 100;//���� HP
    long oriHp;

    public Vector2 startPos;

    Animator anim;
    public float curTime;


    GameObject Player;
    //���� ������
    //���� ��ġ
    public GameObject money;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //�������� ���� hp���� ��ƾ���
        //������ ü���� �÷��̾��� ���� ������ ���� ���� �����̶�
        //��� ������ ���߿� ������ �� �� 
        oriHp = hp;
        target = GameObject.Find("Gold").transform;
        //Debug.Log(target.position);

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            transform.Translate(Vector2.left * Time.deltaTime * GameManager.instance.gameSpeed);

            if(transform.position.x < -16)
            {
                gameObject.SetActive(false);//������Ʈ Ǯ ��SetActive

                transform.position = startPos;//ó�� ��ġ�� �ʱ�ȭ
            }
        }else
        {
            if (curTime >= 1)
            {
                float dis = Vector3.Distance(Player.transform.position, transform.position);

                if (dis <= 3)
                {
                    anim.SetBool("att", true);
                    Player.GetComponent<PlayerController>().Damage(att);
                    curTime = 0;
                }
            }
        }
    }
    
    public void Damage(long pAtt)
    {
        hp -= pAtt;//�Ű�����att(Player ���ݷ�)�� ����

        if (hp <= 0)//������ ü���� 0���� �۰ų� �������
        {
            int randCount = Random.Range(5, 10);
            for (int i = 0; i < randCount; i++)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                GameObject itemFx = Instantiate(money, screenPos, Quaternion.identity);
                itemFx.transform.SetParent(GameObject.Find("Canvas").transform);
                itemFx.GetComponent<ItemFx>().Explosion(screenPos, target.position, 150f);
            }

            gameObject.SetActive(false);//���� ǥ�� ���ִ°ɷ�
            transform.position = startPos;//�ٽ� ó�� ���� �������� �̵�
            hp = oriHp;//ü�� �ٽ� ó�� HP�� 
            GameManager.instance.isPlay = true;//�÷��̸� �ٽ� Ʈ��� ���༭ �÷��� ����
            GameManager.instance.SetMoney(Random.Range(50,100));
        }
        else
        {
            //������ �ؽ�Ʈ ó��
            DamageOn damageTxt = GetComponent<DamageOn>();
            damageTxt.DamageTxt();
        }
    }
    public void CriDamage(long criAtt)
    {
        hp -= criAtt;

        //ũ��Ƽ�� ������ ó��
        DamageOn damgeTxt = GetComponent<DamageOn>();
        damgeTxt.CriDamageTxt();
    }

}
