using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    // �� ������Ʈ�� �����ϴ� Ǯ(List)
    public List<GameObject> mobPool = new List<GameObject>();

    // ������ �� ������ �迭
    public GameObject[] mobs;

    // �� �� �����մ� ������ ����
    public int objCnt = 1;

    private void Awake()
    {
        // mobs �迭�� �ִ� �� �� �����տ� ���� objCnt ����ŭ Ǯ�� �����Ͽ� �߰�
        for (int i = 0; i < mobs.Length; i++)
        {
            for (int j = 0; j < objCnt; j++)
            {
                mobPool.Add(CreateObj(mobs[i], transform));
            }
        }
    }

    // �� �������� �ν��Ͻ�ȭ�ϰ� Ǯ�� �߰��� �� ����� �޼���
    GameObject CreateObj(GameObject obj, Transform parent)
    {
        // �� �������� ����(�ν��Ͻ�ȭ)
        GameObject copy = Instantiate(obj);
        // ������ ���� �� ��ũ��Ʈ�� �پ��ִ� ���� ������Ʈ�� �ڽ����� ����
        copy.transform.SetParent(parent);

        // ������ ������Ʈ�� ��Ȱ��ȭ ���·� ����
        copy.SetActive(false);

        return copy;
    }

    void Start()
    {
        // ������ �ð� �������� ���� Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        StartCoroutine(CreateMob());
    }

    // �� Ǯ���� ��Ȱ��ȭ�� ���� Ȱ��ȭ�ϴ� �ڷ�ƾ
    IEnumerator CreateMob()
    {
        while (true)
        {
            // ��Ȱ��ȭ�� �� �� �ϳ��� �����ϰ� Ȱ��ȭ
            mobPool[DeactiveMob()].SetActive(true);

            // ���� ���� Ȱ��ȭ�ϱ� ������ ���� �ð�(1~3��) ���
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    // ��Ȱ��ȭ�� ���� �ε����� �����ϰ� �����ϴ� �޼���
    int DeactiveMob()
    {
        // ��Ȱ��ȭ�� ���� �ε����� ������ ����Ʈ
        List<int> num = new List<int>();

        // Ǯ�� �ִ� ��� ���� �˻�
        for (int i = 0; i < mobPool.Count; i++)
        {
            // ���� �ش� ���� ��Ȱ��ȭ ���¶��
            if (!mobPool[i].activeSelf)
            {
                // �ش� �ε����� ����Ʈ�� �߰�
                num.Add(i);
            }
        }

        int x = 0;

        // ��Ȱ��ȭ�� ���� ������ ���
        if (num.Count > 0)
        {
            // ��Ȱ��ȭ�� ���� �� �����ϰ� �ϳ��� ����
            x = num[Random.Range(0, num.Count)];
        }

        return x; // ���õ� �ε��� ��ȯ
    }
}
