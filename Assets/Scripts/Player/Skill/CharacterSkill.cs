using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSkill : MonoBehaviour
{
    public bool isAttackReady;

    public SkillSet SubSkill;
    public SkillSet UltimateSkill;

    private UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl CACscript;
    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl TPUCscript;

    [System.Serializable]
    public class SkillSet
    {
        [Header("--------------- 스킬 프리팹 ---------------")]
        //[HideInInspector] public RuntimeAnimatorController TargetAnimation;
        public GameObject Prefab;
        public Transform PrefabPos;
        public Transform BoneTransform;

        [Header("--------------- 스킬 스탯 ---------------")]
        public float damage = 0f;

        public float attackAbleDelay = 1.0f;

        public float SkillCoolDown = 0;
        public bool isSkillCoolDown; //skill CoolDown

        public float PrefabStartDelay = 0;
        public float PrefabEndDelay = 5;

        [Header("--------------- 디버그 ---------------")]

        public List<GameObject> CurrentInstance;

        public AnimationClip CharacterAnimationClip;
        public CustomEffect customEffect;
    }

    [System.Serializable]
    public class CustomEffect
    {
        public GameObject Prefab;
        public Transform PrefabPos;
        public Transform BoneTransform;
        public float PrefabStartDelay = 0;
        public float PrefabEndDelay = 6;
    }

    // Start is called before the first frame update
    void Start()
    {
        SubSkill.isSkillCoolDown = true;
        SubSkill.CurrentInstance = new List<GameObject>();
        SubSkill.CurrentInstance.Clear();

        CACscript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl>();
        TPUCscript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        isAttackReady = CACscript.getAttackAble();

        if (Input.GetButtonDown("SubSkill"))
        {
            if (!isAttackReady) return;

            if(SubSkill.isSkillCoolDown)
            {
                UseSubSkill();
                RunAnimation(SubSkill);
            }

                
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
        StartCoroutine(attackAbleTimer(skillSet));

        StartCoroutine(SkillCoolDown(skillSet));

        StartCoroutine(DoCustomEffect(skillSet));

        GameObject skillPrefab = skillSet.Prefab;
        yield return new WaitForSeconds(skillSet.PrefabStartDelay);
        skillPrefab = Instantiate(skillPrefab, skillSet.PrefabPos.position, skillSet.PrefabPos.rotation);

        if(skillSet.BoneTransform != null)
        skillPrefab.transform.SetParent(skillSet.BoneTransform);

        SubSkill.CurrentInstance.Add(skillPrefab);

        yield return new WaitForSeconds(skillSet.PrefabEndDelay);

        SubSkill.CurrentInstance.Remove(skillPrefab);
        Destroy(skillPrefab);
    }

    IEnumerator SkillCoolDown(SkillSet skillSet)
    {
        skillSet.isSkillCoolDown = false;

        yield return new WaitForSeconds(skillSet.SkillCoolDown);

        skillSet.isSkillCoolDown = true;
    }

    IEnumerator attackAbleTimer(SkillSet skillSet)
    {
        CACscript.setAttackAble(false);
        TPUCscript.setMoveAble(false);

        yield return new WaitForSeconds(skillSet.attackAbleDelay);

        CACscript.setAttackAble(true);
        TPUCscript.setMoveAble(true);
    }

    void RunAnimation(SkillSet skillSet)
    {
        StartCoroutine(AnimControl(skillSet.attackAbleDelay));
    }

    IEnumerator AnimControl(float delay)
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("SubSkill",true);
        
        yield return new WaitForSeconds(delay);
        anim.SetBool("SubSkill",false);
    }

    IEnumerator DoCustomEffect(SkillSet skillSet)
    {
        if(skillSet.customEffect.Prefab == null) 
        {
            print("no custom effect!");
            yield break;
        }

        CustomEffect customPrefab = skillSet.customEffect;
        yield return new WaitForSeconds(customPrefab.PrefabStartDelay);
        GameObject customPrefabObj = Instantiate(customPrefab.Prefab, customPrefab.PrefabPos.position, customPrefab.PrefabPos.rotation);

        if(customPrefab.BoneTransform != null)
        customPrefabObj.transform.SetParent(customPrefab.BoneTransform);

        yield return new WaitForSeconds(customPrefab.PrefabEndDelay);

        Destroy(customPrefabObj);
    }
}