using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    //������Ʈ Ǯ��
    public List<GameObject> mobPool = new List<GameObject>();
    //���� ������Ʈ �迭
    public GameObject[] mobs;

    public int objCnt = 1;

    private void Awake()
    {
        for(int i = 0; i < mobs.Length; i++)
        {
            for(int j = 0; j<objCnt; j++)
            {
                mobPool.Add(CreateObj(mobs[i], transform));
            }
        }
    }

    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);

        copy.SetActive(false);

        return copy;
    }

    void Start()
    {
        //���� ������ƮǮ �ڷ�ƾ
        StartCoroutine(CreateMob());
    }

    //������Ʈ Ǯ������ �ε����� �����ָ� ����
    IEnumerator CreateMob()
    {
        while (true)
        {
            mobPool[DeactiveMob()].SetActive(true);

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }


    //��Ȱ��ȭ�� ������Ʈ ���� �ε��� �̾Ƴ�
    int DeactiveMob()
    {
        List<int> num = new List<int>();

        for(int i = 0; i < mobPool.Count; i++)
        {
            //activeSelf�� �𸥴ٸ�
            //���� ����Ʈ�� ��Ȱ��ȭ�� �������� �ִٸ�
            if (!mobPool[i].activeSelf)
            {
                num.Add(i);
            }
        }

        int x = 0;

        if(num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }


    void Update()
    {

    }

}
