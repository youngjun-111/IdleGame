using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public string type;
    public float percentage;
    public float duration;
    public float currentTime;
    public Image icon;

    PlayerController player;

    private void Awake()
    {
        icon = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //다른곳에서 초기화 할 수 있는 함수
    public void Init(string type, float per, float du)
    {
        this.type = type;
        percentage = per;
        duration = du;
        currentTime = duration;
        icon.fillAmount = 1;
        Execute();
    }
    
    public void Execute()
    {
        StartCoroutine(Activation());
    }

    //버프활성 코루틴
    //코루틴에서 사용할 대기시간
    //깎는거와 경과 시간을 구분 지어줄려고 WaitForSeconds를 새로 생성해줌
    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    IEnumerator Activation()
    {
        //0.1초에 0.1만큼 icon.fillAmount가 감소
        while(currentTime > 0)
        {
            //필어마운트를 0.1초마다 0.1을 깎아줌
            currentTime -= 0.1f;
            //버프 줄어드는 표시
            icon.fillAmount = currentTime / duration;
            yield return seconds;//0.1초후
        }
        icon.fillAmount = 0;
        currentTime = 0;

        DeActivation();
    }
    
    public void DeActivation()
    {
        Destroy(gameObject);
    }
}
