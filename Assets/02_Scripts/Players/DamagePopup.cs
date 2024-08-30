using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DamagePopup : MonoBehaviour
{
    public GameObject canvas;
    GameObject Player;

    void Start()
    {
        //�ؽ�Ʈ�� �Ʒ� �ڵ�� �������� ǥ���� �� �ְ� �ϼ�

        Player = GameObject.FindGameObjectWithTag("Player");

        Text tmp_text = GetComponent<Text>();
        tmp_text.text = Player.GetComponent<PlayerController>().att.ToAttackString();
        //.DoColor(color ��ǥ��, float ��ȭ�ð�)
        //.DoFade(float ��ǥ��, float ��ȭ�ð�)
        tmp_text.DOColor(Color.red, 1f);
        tmp_text.DOFade(0f, 1f);
        //������ ��ȭ
        transform.DOPunchScale(Vector3.one, 1);
        //���� ��ġ���� + ���� . �Ϸ�Ǹ� ����

        transform.DOMove(transform.position + Vector3.up * 2, 1).OnComplete(() => { Destroy(canvas); });

    }

    void Update()
    {
        
    }
}
