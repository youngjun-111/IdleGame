using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DamagePopup : MonoBehaviour
{
    public GameObject canvas;
    
    GameObject Player;
    //�Ϲ� ������, ũ�������� ����
    public Color color = Color.red;

    void Start()
    {
        //�ؽ�Ʈ�� �Ʒ� �ڵ�� �������� ǥ���� �� �ְ� �ϼ�

        Player = GameObject.FindGameObjectWithTag("Player");

        Text tmp_text = GetComponent<Text>();

        if(color == Color.red)
        {
            tmp_text.text = Player.GetComponent<PlayerController>().att.ToAttackString();
        }
        else
        {
            tmp_text.text = "CriticalHit! " + Player.GetComponent<PlayerController>().cirAtt.ToAttackString();
        }
        //.DoColor(color ��ǥ��, float ��ȭ�ð�)
        //.DoFade(float ��ǥ��, float ��ȭ�ð�)
        tmp_text.DOColor(color, 1f);
        tmp_text.DOFade(0f, 1f);
        //������ ��ȭ
        transform.DOPunchScale(Vector3.one, 1);
        //���� ��ġ���� + ���� . �Ϸ�Ǹ� ����
        transform.DOMove(transform.position + Vector3.up * 2, 1).OnComplete(() => { Destroy(canvas); });
    }
}
