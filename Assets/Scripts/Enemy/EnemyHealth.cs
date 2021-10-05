using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyHealth : MonoBehaviour
{
    [Space(10)]
    [Header("----------------------------- Health status -----------------------------")]

    [SerializeField] private bool isDead = false;

    [SerializeField] private float maxHp = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;

    [SerializeField] private float hp = 100.0f;
    [SerializeField] private float stamina = 100.0f;

    [SerializeField] private PlayerWeapon playerWeaponScript;
    [SerializeField] private float PlayerWeaponDamage;

    [SerializeField] private bool isInvincible;

    private float currentHealthPct;
    private float currentStaminaPct;

    [SerializeField] private bool isDamaged;
    [SerializeField] private bool isShowedParticle;


    [Space(10)]
    [Header("----------------------------- reward status -----------------------------")]

    [SerializeField]
    public float spirit = 50.0f;

    [Space(10)]
    [Header("--------------------------- Status Bar (Visual) -----------------------------")]

    [SerializeField] private float updateSpeedSeconds = 0.3f;
    [SerializeField] private float hpbgDelay = 0.5f;

    [SerializeField] private Image hpImg;
    //private Image staminaImg;
    [SerializeField] private Image hpbgImg;

    //UIText
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI staminaText;

    [Space(10)]
    [Header("----------------------------- Weapon Collider -----------------------------")]

    [SerializeField] private PlayerWeaponCollider PlayerWeaponColliderScript;
    public enum BodyTypeEnum {FLESH, WOOD, STONE, METAL};
    [SerializeField] private BodyTypeEnum bodyTypes;
    void Start()
    {
        hp = maxHp;
        stamina = maxStamina;

        playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();
        

        isDamaged = false;
        isShowedParticle = false;

        hpImg = transform.Find("Canvas/EnemyHPBar/EnemyHPFill").GetComponent<Image>();

        hpbgImg = transform.Find("Canvas/EnemyHPBar/EnemyHPbgFill").GetComponent<Image>();

        //hpText = this.transform.Find("Canvas/EnemyHPBar/EnemyHP").GetComponent<TextMeshProUGUI>();

        PlayerWeaponColliderScript = GameObject.FindGameObjectWithTag("PlayerWeaponCollider").GetComponent<PlayerWeaponCollider>();
    }

    void Update()
    {
        /*
        if(hpText != null)
            hpText.text = hp.ToString();
        */

        checkHP();

        //print(getDamaged());
    }

    private void OnTriggerStay(Collider other)
    {
        if (getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerWeapon" && playerWeaponScript.getHitDetector())
        {

            if (!getDamaged())
            {
                PlayerWeaponDamage = playerWeaponScript.getDamage();

                PlayerWeaponColliderScript.addEnemyDamaged(this.gameObject);
                setDamaged(true);
                if (changeHp(-PlayerWeaponDamage))
                {
                    if(hp <= 0f)    //dead by player weapon
                    {
                        //call getSpirit from player script
                        PlayerSpirit spiritScript = GameObject.FindWithTag("Player").GetComponent<PlayerSpirit>();
                        spiritScript.changePlayerSpirit(spirit);
                    }
                }
            }
        }
    }

    public void checkHP()
    {
        if (hp <= 0)
            Dead();
    }

    public bool changeHp(float value)       //if hit return true, dodge return false
    {
        if (value < 0)
        {
            if (getInvincible())
            {
                return false;
            }
        }

        hp += value;

        if (hp < 0.0f) hp = 0.0f;
        else if (hp > maxHp) hp = maxHp;

        currentHealthPct = (float)hp / (float)maxHp;
        hpImg.fillAmount = currentHealthPct;
        handleHealthbgChange(currentHealthPct, hpbgDelay);

        return true;
    }

    private void handleHealthbgChange(float percent, float delay)
    {
       StartCoroutine(ChangeHpbgToPct(percent, delay));
    }

    private IEnumerator ChangeHpbgToPct(float percent, float delay)
    {
        yield return new WaitForSeconds(delay);

        float preChangePct = hpbgImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            hpbgImg.fillAmount = Mathf.Lerp(preChangePct, percent, elapsed / updateSpeedSeconds);
            yield return null;
        }

        hpbgImg.fillAmount = percent;
    }

    public bool getInvincible()
    {
        return isInvincible;
    }

    public void Dead()
    {
        setDead(true);
    }

    public void setDead(bool flag)
    {
        isDead = flag;
    }

    public bool getDead()
    {
        return isDead;
    }

    public bool getDamaged()
    {
        return isDamaged;
    }

    public void setDamaged(bool fl)
    {
        isDamaged = fl;
    }

    public bool getShowedParticle()
    {
        return isShowedParticle;
    }

    public void setShowedParticle(bool fl)
    {
        isShowedParticle = fl;
    }

    public BodyTypeEnum getBodyTpye()
    {
        return bodyTypes;
    }

    public float getSpirit()
    {
        return spirit;
    }
    public void setSpirit(float value)
    {
        spirit = value;
    }
}
