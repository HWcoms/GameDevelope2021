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

    void Start()
    {
        hp = maxHp;
        stamina = maxStamina;

        playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();
        PlayerWeaponDamage = playerWeaponScript.getDamage();

        hpImg = transform.Find("Canvas/EnemyHPBar/EnemyHPFill").GetComponent<Image>();

        hpbgImg = transform.Find("Canvas/EnemyHPBar/EnemyHPbgFill").GetComponent<Image>();

        //hpText = this.transform.Find("Canvas/EnemyHPBar/EnemyHP").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        /*
        if(hpText != null)
            hpText.text = hp.ToString();
        */

        checkHP();
    }

    private void OnTriggerStay(Collider other)
    {
        if (getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerWeaponCollider" && playerWeaponScript.getHitDetector())
        {
            //if (isDealready)
            {
                if (changeHp(-PlayerWeaponDamage))
                {
                    //playerWeaponScript.switchCollider(false);
                    playerWeaponScript.switchHitDetector(false);
                    //print("hit detact disabled");

                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    //hitParticleText.text = ((int)attackDamgage).ToString();

                    //isDealready = false;
                    //GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
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
        isDead = true;
    }
    public bool getDead()
    {
        return isDead;
    }
}
