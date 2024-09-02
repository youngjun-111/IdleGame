using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    // 몹 오브젝트를 저장하는 풀(List)
    public List<GameObject> mobPool = new List<GameObject>();

    // 생성할 몹 프리팹 배열
    public GameObject[] mobs;

    // 각 몹 프리팹당 생성할 수량
    public int objCnt = 1;

    private void Awake()
    {
        // mobs 배열에 있는 각 몹 프리팹에 대해 objCnt 수만큼 풀에 생성하여 추가
        for (int i = 0; i < mobs.Length; i++)
        {
            for (int j = 0; j < objCnt; j++)
            {
                mobPool.Add(CreateObj(mobs[i], transform));
            }
        }
    }

    // 몹 프리팹을 인스턴스화하고 풀에 추가할 때 사용할 메서드
    GameObject CreateObj(GameObject obj, Transform parent)
    {
        // 몹 프리팹을 복제(인스턴스화)
        GameObject copy = Instantiate(obj);
        // 복제된 몹을 이 스크립트가 붙어있는 게임 오브젝트의 자식으로 설정
        copy.transform.SetParent(parent);

        // 생성된 오브젝트를 비활성화 상태로 설정
        copy.SetActive(false);

        return copy;
    }

    void Start()
    {
        // 랜덤한 시간 간격으로 몹을 활성화하는 코루틴 시작
        StartCoroutine(CreateMob());
    }

    // 몹 풀에서 비활성화된 몹을 활성화하는 코루틴
    IEnumerator CreateMob()
    {
        while (true)
        {
            // 비활성화된 몹 중 하나를 랜덤하게 활성화
            mobPool[DeactiveMob()].SetActive(true);

            // 다음 몹을 활성화하기 전까지 랜덤 시간(1~3초) 대기
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    // 비활성화된 몹의 인덱스를 랜덤하게 선택하는 메서드
    int DeactiveMob()
    {
        // 비활성화된 몹의 인덱스를 저장할 리스트
        List<int> num = new List<int>();

        // 풀에 있는 모든 몹을 검사
        for (int i = 0; i < mobPool.Count; i++)
        {
            // 만약 해당 몹이 비활성화 상태라면
            if (!mobPool[i].activeSelf)
            {
                // 해당 인덱스를 리스트에 추가
                num.Add(i);
            }
        }

        int x = 0;

        // 비활성화된 몹이 존재할 경우
        if (num.Count > 0)
        {
            // 비활성화된 몹들 중 랜덤하게 하나를 선택
            x = num[Random.Range(0, num.Count)];
        }

        return x; // 선택된 인덱스 반환
    }
}
