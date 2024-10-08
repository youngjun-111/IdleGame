using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DamagePopup : MonoBehaviour
{
    public GameObject canvas;
    
    GameObject Player;
    //일반 데미지, 크리데미지 구분
    public Color color = Color.red;

    void Start()
    {
        //텍스트에 아래 코드로 데미지를 표시할 수 있게 완성

        Player = GameObject.FindGameObjectWithTag("Player");

        Text tmp_text = GetComponent<Text>();

        if(color == Color.red)
        {
            tmp_text.text = Player.GetComponent<PlayerController>().att.ToAttackString();
        }
        else if (color == Color.yellow)
        {
            tmp_text.text = "CriticalHit! " + Player.GetComponent<PlayerController>().cirAtt.ToAttackString();
        }
        //.DoColor(color 목표값, float 변화시간)
        //.DoFade(float 목표값, float 변화시간)
        tmp_text.DOColor(color, 2);
        tmp_text.DOFade(0f, 2);
        //스케일 변화
        transform.DOPunchScale(Vector3.one, 1);
        //현재 위치에서 + 알파 . 완료되면 삭제
        transform.DOMove(transform.position + Vector3.up * 3, 3f).OnComplete(() => { Destroy(canvas); });
    }
}
