using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSpirit : MonoBehaviour
{
    public float maxSpirit = 300.0f;
    public float playerSpirit = 0.0f;

    [SerializeField] private float currentSoulPct;

    [SerializeField] private TextMeshProUGUI SpiritCountText;

    [SerializeField] private float updateSpeedSeconds = 0.3f;
    [SerializeField] private float soulbgDelay = 0.0f;

    private Image soulImg;
    IEnumerator soulCoroutine;
    bool soulCoroutineRunning = false;

    private void Start()
    {
        SpiritCountText = GameObject.Find("Spirit Count").gameObject.GetComponent<TextMeshProUGUI>();

        refreshSpiritCount();

        soulImg = GameObject.Find("Sphere_SoulBG").GetComponent<Image>();
    }

    void Update()
    {
        refreshSpiritGUI();
    }

    public void changePlayerSpirit(float amount)
    {
        playerSpirit += amount;
        //print("current spirit : " + playerSpirit);

        refreshSpiritCount();

        refreshSpiritGUI();
    }
    public float getPlayerSpirit()
    {
        return playerSpirit;
    }

    public void refreshSpiritCount()
    {
        SpiritCountText.text = playerSpirit.ToString();
    }

    public void refreshSpiritGUI()
    {
        currentSoulPct = (float)playerSpirit / (float)maxSpirit;
        handleSoulChange(currentSoulPct, soulbgDelay);
    }

    private void handleSoulChange(float percent, float delay)
    {
        if (soulCoroutineRunning) return;
        //StopCoroutine(soulCoroutine);

        soulCoroutine = ChangeSoulToPct(percent, delay);
        
        StartCoroutine(soulCoroutine);
    }

    private IEnumerator ChangeSoulToPct(float percent, float delay)
    {
        soulCoroutineRunning = true;

        yield return new WaitForSeconds(delay);

        float preChangePct = soulImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            soulImg.fillAmount = Mathf.Lerp(preChangePct, percent, elapsed / updateSpeedSeconds);
            yield return null;
        }

        soulImg.fillAmount = percent;

        soulCoroutineRunning = false;
    }

}
