using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//DOTween ����Ұ�� �ʿ��� using

public class ItemFx : MonoBehaviour
{
    public void Explosion(Vector2 from, Vector2 to, float explo_range)
    {
        //���� �̵�ȿ�� ��Ʈ���ҽ�
        //�Լ� ȣ�� �� �Էµ� ���ڸ� �� ��ũ��Ʈ�� ����� ������Ʈ ��ġ�� ����
        transform.position = from;
        //������ �����
        //�����ϰ� ����� �������� �����.
        Sequence seq = DOTween.Sequence();
        //������ Ʈ���� ������ ���� �߰�
        //������Ʈ �����Ϳ� ������
        //������Ʈ�� �̵��� �����ð����� ��ȭ�Ѵ�
        //(��ǥ��, ��ȭ�ð�), ��ȭ ����(���ϴ� ����)
        seq.Append(transform.DOMove(from + Random.insideUnitCircle * explo_range, 0.25f).SetEase(Ease.OutExpo));
        seq.Append(transform.DOMove(to, 0.5f).SetEase(Ease.InExpo));

        //(�̸�����)�Լ��� ȣ��Ǹ�
        seq.AppendCallback(() =>
        {
            //�̿�����Ʈ ����
            Destroy(gameObject);
        });
    }
}
