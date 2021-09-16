using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class CharacterActionControl : MonoBehaviour
    {
        private Animator m_Animator;

        private bool moveAble;

        private bool attackAble;
        private bool rollAble;
        [SerializeField] private bool attackContinueAble;
        [SerializeField]private bool isClicked;

        //roll(dodge) invincible
        private bool dodged;

        [Space(10)]
        [Header("----------------------------- Roll Invincible -----------------------------")]

        [SerializeField] private bool isInvincible;
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

        IEnumerator AttackCoroutine;
        IEnumerator AttackContinueAbleCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            m_Animator = GetComponent<Animator>();

            moveAble = true;

            attackAble = true;
            rollAble = true;
            attackContinueAble = false;

            TPUCscript = GetComponent<ThirdPersonUserControl>();
            CHscript = GetComponent<CharacterHealth>();

            isInvincible = false;
            dodged = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(CHscript.getHp() <= 0)
            {
                TPUCscript.setMoveAble(false);
                m_Animator.SetBool("dead", true);
                CHscript.Dead();

                return;
            }

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
                    setAttackInt(1);    //animtor attack -> true

                    /*
                    AttackCoroutine = attackWait(.9f);
                    AttackContinueAbleCoroutine = AttackContinueAbleDelay(.3f);
                    StartCoroutine(AttackCoroutine);
                    StartCoroutine(AttackContinueAbleCoroutine);
                    */
                }

                //attack2
                if(m_Animator.GetBool("attack"))        //while attack
                {
                    if(Input.GetMouseButtonDown(0) && attackContinueAble)
                    {
                        /*
                        StopCoroutine(AttackCoroutine);
                        StopCoroutine(AttackContinueAbleCoroutine);
                        setAttackContinueAble(false); //attackContinueAble = false;

                        StartCoroutine(AttackCoroutine);
                        StartCoroutine(AttackContinueAbleCoroutine);
                        */

                        setClicked(true); //m_Animator.SetBool("Clicked", true);
                    }
                }

                if (m_Animator.GetBool("attack"))
                {
                    if (getClicked())
                    {
                        setContinueAttack(true);
                    }
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


            if (Input.GetKey(KeyCode.LeftShift) && m_Animator.GetFloat("Forward") >= 0.9f)
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
        /*
        IEnumerator attackWait(float delay)
        {
            yield return new WaitForSeconds(delay);

            setAttackInt(0);    //animtor attack -> false


            yield return new WaitForSeconds(0.15f);
            attackAble = true;
            rollAble = true;
            attackContinueAble = false;

            TPUCscript.setMoveAble(true);
        }

        IEnumerator AttackContinueAbleDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            attackContinueAble = true;
        }
        */

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

        public void setContinueAttack(bool flag)
        {
            m_Animator.SetBool("ContinueAttack", flag);
        }

        public void playContinueAttackAnim(int flag)
        {
            bool fl = (flag == 0 ? false : true);

            if (fl)
                m_Animator.SetBool("ContinueAttackAnim", true);
            else
                m_Animator.SetBool("ContinueAttackAnim", false);
        }

        public void setContinueAttackInt(int flag)
        {
            bool fl = (flag == 0 ? false : true);

            m_Animator.SetBool("ContinueAttack", fl);
            print(fl);

            if(!fl)
            {
                setClicked(false);
                setAttackContinueAble(false);
            }
        }
        public bool getContinueAttack()
        {
            return m_Animator.GetBool("ContinueAttack");
        }

        public void setClicked(bool flag)
        {
            isClicked = flag;
        }
        
        public void setClickedInt(int flag)
        {
            bool fl = (flag == 0 ? false : true);

            isClicked = fl;
        }


        public bool getClicked()
        {
            return isClicked;
        }

        public void setAttackContinueAble(bool flag)
        {
            attackContinueAble = flag;
        }

        public void setAttackContinueAbleInt(int flag)
        {
            bool fl = (flag == 0 ? false : true);

            attackContinueAble = fl;
        }

        public bool getAttackContinueAble()
        {
            return attackContinueAble;
        }
        /*
        public bool checkAnimContinue()
        {
            if (getClicked())
                return true;
            else
                return false;
        }*/

        public void setAttackInt(int flag)
        {
            if (getContinueAttack()) return;

            bool fl = (flag == 0 ? false : true);

            m_Animator.SetBool("attack", fl);

            if(fl == false)
            {
                attackAble = true;
                rollAble = true;
                attackContinueAble = false;

                TPUCscript.setMoveAble(true);
            }
        }
    }
}
