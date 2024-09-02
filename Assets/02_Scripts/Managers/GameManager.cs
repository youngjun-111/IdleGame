using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameSpeed = 3f;
    public bool isPlay = true;//적과 마주칠경우 멈춤

    public long money = 1000;
    public Text moneyText;

    public static GameManager instance;
    void Start()
    {
        instance = this;//싱글톤
        moneyText.text = money.ToAttackString() + " : Gold";
    }

    void Update()
    {
        if (isPlay)
        {
            gameSpeed = 3f;
        }
        else//적과 마주쳣을 경우
        {
            gameSpeed = 0f;
        }
    }

    //숫자 카운팅 애니메이션
    IEnumerator Count(float target, float current)
    {
        //카운팅에 걸리는 시간
        float duration = Mathf.Lerp(0.2f, 0f, 0.2f);
        //float duration = 0.2f;
        // 매 프레임마다 증가시킬 금액 계산
        float offset = (target - current) / duration;

        while(current < target)
        {
            //현재 금액을 프레임 간격에 따라 증가
            current += offset * Time.deltaTime; 
            //현재 금액을 반올림하여 천 단위 구분 기호와 함께 표시
            moneyText.text = string.Format("{0:n0}", (int)current) + " : Gold";
            yield return null;
        }
        //루프 종료 후, 현재 금액을 목표 금액으로 맞춰줌
        current = target;
        //목표 금액을 반올림하여 천 단위 구분 기호와 함께 금액을 목표 금액으로 맞춤
        moneyText.text = string.Format("{0:n0}", (int)current) + " : Gold";

    }

    public void SetMoney(long Money)
    {
        // 현재 보유한 금액에 추가 금액을 더함
        money += Money;
        // 증가한 금액으로 카운팅 코루틴 시작
        StartCoroutine(Count(money, money - Money));
    }
}
