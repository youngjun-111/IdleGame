using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Skill> deck = new List<Skill>();//카드덱 리스트로만들어줌
    public int total = 0;//카드들의 가중치 총 합(확률)
    Coroutine sc;

    public GameObject skillPrefab;
    public Transform parent;

    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            //스크립트가 활성화 되면 카드덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
    }

    //랜덤하게 선택한 카드를 담을 리스트
    public List<Skill> result = new List<Skill>();

    public List<GameObject> skillob = new List<GameObject>();
    IEnumerator ResultSelect()
    {
        for (int i = 0; i < 20; i++)
        {
            //가중치 랜덤 결과 리스트에 넣기
            result.Add(RandomCard());
            //비어 있는 카드를 생성
            GameObject skillUI = Instantiate(skillPrefab, parent);
            //생성된 카드에 리스트이 정보를 넣어준다.
            skillUI.GetComponent<SkillUI>().CardUISet(result[i]);
            skillob.Add(skillUI);
            yield return new WaitForSeconds(0.2f);
        }
        sc = null;
    }

    public Skill RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(Random.Range(0, total));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if(selectNum <= weight)
            {
                Skill temp = new Skill(deck[i]);
                return temp;
            }
        }
        return null;
    }

    public void RandomStart() 
    { 
        if(sc != null)
        {
            StopCoroutine(sc);
            for (int i = 0; i < skillob.Count; i++)
            {
                Destroy(skillob[i]);
            }
            result.Clear();
            sc = StartCoroutine(ResultSelect());
        }
    }
}
