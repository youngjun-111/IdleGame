using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Image chr;
    public Text skillName;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //��ųī���� ������ �ʱ�ȭ
    public void CardUISet(Skill skill)
    {
        chr.sprite = skill.skillImage;
        skillName.text = skill.skillName;
    }
}
