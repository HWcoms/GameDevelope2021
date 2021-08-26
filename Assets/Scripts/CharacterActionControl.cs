using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class CharacterActionControl : MonoBehaviour
    {
        Animator m_Animator;

        protected bool moveAble;

        protected bool attackAble;
        protected bool rollAble;

        //roll(dodge) invincible
        private bool dodged;

        [Space(10)]
        [Header("----------------------------- Roll Invincible -----------------------------")]

        [SerializeField] protected bool isInvincible;
        [SerializeField] private float delayBeforeInvincible = 0.1f;
        [SerializeField] private float invincibleTime = 0.5f;
        private float invTimer;
        

        ThirdPersonUserControl TPUCscript;

        CharacterHealth CHscript;

        [Space(10)]
        [Header("----------------------------- Stamina Cost -----------------------------")]

        //Stamina Cost
        [SerializeField] private float runStaminaCost = 0.05f;
        [SerializeField] private float rollStaminaCost = 20.0f;
        [SerializeField] private float attackStaminaCost = 10.0f;
        [SerializeField] private float jumpStaminaCost = 20.0f;

        // Start is called before the first frame update
        void Start()
        {
            m_Animator = GetComponent<Animator>();

            moveAble = true;

            attackAble = true;
            rollAble = true;

            TPUCscript = GetComponent<ThirdPersonUserControl>();
            CHscript = GetComponent<CharacterHealth>();

            isInvincible = false;
            dodged = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_Animator.GetBool("OnGround")) return;

            if(CHscript.getStamina() <= 0)
            {
                TPUCscript.setStaminaAble(false);
            }
            else
            {
                TPUCscript.setStaminaAble(true);

                //attack1
                if (Input.GetMouseButtonDown(0) && attackAble)
                {
                    CHscript.changeStamina(-attackStaminaCost);

                    TPUCscript.setMoveAble(false);

                    attackAble = false;
                    rollAble = false;
                    m_Animator.SetBool("attack", true);

                    StartCoroutine(attackWait(.9f));
                }

                //roll
                if (Input.GetMouseButtonDown(1) && rollAble)
                {
                    dodged = true;

                    invTimer = delayBeforeInvincible + invincibleTime;

                    CHscript.changeStamina(-rollStaminaCost);

                    TPUCscript.setMoveAble(false);

                    attackAble = false;
                    rollAble = false;
                    m_Animator.SetBool("roll", true);

                    StartCoroutine(rollWait(.8f));
                }
            }

            if(dodged)
            {
                StartCoroutine(Dodge());
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CHscript.changeStamina(-runStaminaCost, 1);
            }
            else
            {
                // changeStamina(0.1f);
            }

            if (Input.GetKeyDown(KeyCode.Space) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded") )
            {
                CHscript.changeStamina(-jumpStaminaCost);
            }

        }

        IEnumerator attackWait(float delay)
        {
            yield return new WaitForSeconds(delay);

            m_Animator.SetBool("attack", false);


            yield return new WaitForSeconds(0.15f);
            attackAble = true;
            rollAble = true;

            TPUCscript.setMoveAble(true);
        }

        IEnumerator rollWait(float delay)
        {
            yield return new WaitForSeconds(delay);

            m_Animator.SetBool("roll", false);

            yield return new WaitForSeconds(0.3f);
            attackAble = true;
            rollAble = true;

            TPUCscript.setMoveAble(true);
        }

        IEnumerator Dodge()
        {
            invTimer -= Time.deltaTime;

            yield return new WaitForSeconds(0.01f);

            if (invTimer <= 0.0f)
            {
                invTimer = 0.0f;

                isInvincible = false;
                dodged = false;
            }
            else if (invTimer <= invincibleTime)
            {
                isInvincible = true;
            }
            
        }

        public bool getInvincible()
        {
            return isInvincible;
        }
    }
}
