using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    [SerializeField] private bool isSkillReady;

    public SkillSet SubSkill;

    [System.Serializable]
    public class SkillSet
    {
        //[HideInInspector] public RuntimeAnimatorController TargetAnimation;
        public GameObject Prefab;
        public Transform BonePosition;
        public Transform BoneRotation;
        public float SkillCoolDown = 0;
        public bool isSkillReady;

        public float StartDelay = 3;
        public float DestroyDelay = 5;
        [HideInInspector] public GameObject CurrentInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        isSkillReady = true;
        SubSkill.isSkillReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SubSkill"))
        {
            if(SubSkill.isSkillReady)
                UseSubSkill();
        }
    }

    void UseSubSkill()
    {
        if(SubSkill.Prefab != null)
        {
            StartCoroutine(C_Skill(SubSkill));
        }
        else
        {
            print("sub skill prefab is null");
        }
    }

    IEnumerator C_Skill(SkillSet skillSet)
    {
        StartCoroutine(SkillCoolDown(skillSet));

        GameObject skillPrefab = skillSet.Prefab;
        yield return new WaitForSeconds(skillSet.StartDelay);
        skillPrefab.SetActive(true);

        yield return new WaitForSeconds(skillSet.DestroyDelay);
        skillPrefab.SetActive(false);
    }

    IEnumerator SkillCoolDown(SkillSet skillSet)
    {
        skillSet.isSkillReady = false;

        yield return new WaitForSeconds(skillSet.SkillCoolDown);

        skillSet.isSkillReady = true;
    }
}