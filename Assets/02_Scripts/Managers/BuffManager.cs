using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    //싱글톤
    public static BuffManager instance;

    private void Awake()
    {
        instance = this;
    }

    //생성해줄 버프아이콘 프리팹
    public GameObject buffPrefab;

    public void CreateBuff(string type, float per, float du, Sprite icon)
    {
        //매개 변수 타입에 따라서 각 per, du, icon 값을 변경해줄 함수
        //버튼을 눌렀을 때 버프창표시 패널에서 해당 버튼이 가지고 있던 type의 per,du, icon 값이 적용되서 화면상에서 버프가 사용됐다는걸 표시해주기 위한 BuffManager안에 있는 CreateBuff 함수

        //buffPrefab생성
        GameObject go = Instantiate(buffPrefab, transform);
        //buff 스크립트에 접근해 초기화 및 버프 실행
        go.GetComponent<Buff>().Init(type, per, du);
        //스프라이트 아이콘 적용
        go.GetComponent<Image>().sprite = icon;
    }
}
