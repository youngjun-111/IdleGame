using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Skill> deck = new List<Skill>();//ī�嵦 ����Ʈ�θ������
    public int total = 0;//ī����� ����ġ �� ��(Ȯ��)
    Coroutine sc;

    public GameObject skillPrefab;
    public Transform parent;

    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            //��ũ��Ʈ�� Ȱ��ȭ �Ǹ� ī�嵦�� ��� ī���� �� ����ġ�� �����ݴϴ�.
            total += deck[i].weight;
        }
    }

    //�����ϰ� ������ ī�带 ���� ����Ʈ
    public List<Skill> result = new List<Skill>();

    public List<GameObject> skillob = new List<GameObject>();
    IEnumerator ResultSelect()
    {
        for (int i = 0; i < 20; i++)
        {
            //����ġ ���� ��� ����Ʈ�� �ֱ�
            result.Add(RandomCard());
            //��� �ִ� ī�带 ����
            GameObject skillUI = Instantiate(skillPrefab, parent);
            //������ ī�忡 ����Ʈ�� ������ �־��ش�.
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
