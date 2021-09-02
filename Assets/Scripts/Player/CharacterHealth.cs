using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [Space(10)]
    [Header("----------------------------- Health status -----------------------------")]
    [SerializeField] private bool isDead = false;

    [SerializeField] private float maxHp = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;

    [SerializeField] private float hp;
    [SerializeField] private float stamina;

    
    private float currentHealthPct;
    private float currentStaminaPct;
    private Image hpImg;
    private Image staminaImg;

    [Space(10)]
    [Header("--------------------------- Status Bar (Visual) -----------------------------")]

    [SerializeField] private float updateSpeedSeconds = 0.3f;

    //UIText
    private Text hpText;
    private Text staminaText;

    [Space(10)]
    [Header("----------------------------- Rezen status -----------------------------")]

    //rezenTimer
    [SerializeField] private float hpRezenDelay = 7.0f;
    [SerializeField] private float hpRezenSpeed = 0.05f;

    [SerializeField] private float staminaRezenDelay = 4.5f;
    [SerializeField] private float staminaRezenSpeed = 0.25f;

    /*[SerializeField]*/ private float hpTimer;
    /*[SerializeField]*/ private float staminaTimer;
    private bool isHpRezen;
    private bool isStaminaRezen;

    //dodge
    private UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl CACscript;




    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        stamina = maxStamina;

        currentHealthPct = (float) hp / (float) maxHp;
        currentStaminaPct = (float)stamina / (float)maxStamina;

        isHpRezen = false;
        isStaminaRezen = false;

        CACscript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl>();

        hpImg = GameObject.Find("HPFill").GetComponent<Image>();
        staminaImg = GameObject.Find("STAFill").GetComponent<Image>();

        hpText = GameObject.Find("HPnum").GetComponent<Text>();
        staminaText = GameObject.Find("Staminanum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = ((int)hp).ToString() + " / " + maxHp.ToString();
        staminaText.text = ((int)stamina).ToString() + " / " + maxStamina.ToString();

        if (isDead) return;

        if(isHpRezen)
        {
            rezenStartTimer(hpRezenSpeed, 1);
        }
        if (isStaminaRezen)
        {
            rezenStartTimer(staminaRezenSpeed, 2);
        }   
    }

    public bool changeHp(float value)       //if hit return true, dodge return false
    {
        if (value < 0)
        {
            if (CACscript.getInvincible())
            {
                return false;
            }
            isHpRezen = true;
            hpTimer = hpRezenDelay;
        }

        hp += value;

        if (hp < 0.0f) hp = 0.0f;
        else if (hp > maxHp) hp = maxHp;

        currentHealthPct = (float)hp / (float)maxHp;
        handleHealthChange(currentHealthPct);

        return true;
    }

    public void changeHp(float value, int mode) //mode 1: small changes, mode2: DOT damage
    {
        if (value < 0)
        {
            if (CACscript.getInvincible() && mode != 2) //when dodge
            {
                return;
            }
            isHpRezen = true;
            hpTimer = hpRezenDelay;
        }

        hp += value;

        if (hp < 0.0f) hp = 0.0f;
        else if (hp > maxHp) hp = maxHp;

        currentHealthPct = (float)hp / (float)maxHp;
        if (mode == 1)
        {
            hpImg.fillAmount = currentHealthPct;
        }
        else if (mode == 2)
        {
            handleStaminaChange(currentStaminaPct);
        }
    }

    public void changeStamina(float value)
    {
        stamina += value;

        if (stamina < 0.0f) stamina = 0.0f;
        else if (stamina > maxStamina) stamina = maxStamina;

        currentStaminaPct = (float)stamina / (float)maxStamina;
        handleStaminaChange(currentStaminaPct);

        if (value < 0)
        {
            isStaminaRezen = true;
            staminaTimer = staminaRezenDelay;
        }
            
    }

    public void changeStamina(float value, int mode)
    {
        stamina += value;

        if (stamina < 0.0f) stamina = 0.0f;
        else if (stamina > maxStamina) stamina = maxStamina;

        currentStaminaPct = (float)stamina / (float)maxStamina;
        staminaImg.fillAmount = currentStaminaPct;

        if (value < 0)
        {
            isStaminaRezen = true;
            staminaTimer = staminaRezenDelay;
        }

    }

    public float getHp()
    {
        return hp;
    }

    public float getStamina()
    {
        return stamina;
    }

    private void handleHealthChange(float percent)
    {
        StartCoroutine(ChangeHpToPct(percent));
    }

    private void handleStaminaChange(float percent)
    {
       StartCoroutine(ChangeStaToPct(percent));
    }

    private IEnumerator ChangeHpToPct(float percent)
    {
        float preChangePct = hpImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            hpImg.fillAmount = Mathf.Lerp(preChangePct, percent, elapsed / updateSpeedSeconds);
            yield return null;
        }

        hpImg.fillAmount = percent;
    }

    private IEnumerator ChangeStaToPct(float percent)
    {
        float preChangePct = staminaImg.fillAmount;
        float elapsed = 0f;
        
        while(elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            staminaImg.fillAmount = Mathf.Lerp(preChangePct, percent, elapsed / updateSpeedSeconds);
            yield return null;
        }
        
        staminaImg.fillAmount = percent;
    }

    private void rezenStartTimer(float rezenSpeed, int mode)    //mode 1: hp, mode 2: stamina
    {
        if(mode == 1)
        {
            hpTimer -= 0.01f;

            if (hpTimer < 0.0f) changeHp(0.5f * rezenSpeed, 1);

            if (maxHp <= hp) isHpRezen = false;
        }
        else
        {
            staminaTimer -= 0.01f; 

            if(staminaTimer < 0.0f) changeStamina(0.5f * rezenSpeed, 1);

            if (maxStamina <= stamina) isStaminaRezen = false;
        }
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
