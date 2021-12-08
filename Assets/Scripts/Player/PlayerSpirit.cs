using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpirit : MonoBehaviour
{
    public float playerSpirit = 0.0f;

    [SerializeField] private TextMeshProUGUI SpiritCountText;

    private void Start()
    {
        SpiritCountText = GameObject.Find("Spirit Count").gameObject.GetComponent<TextMeshProUGUI>();

        refreshSpiritCount();
    }

    public void changePlayerSpirit(float amount)
    {
        playerSpirit += amount;
        //print("current spirit : " + playerSpirit);

        refreshSpiritCount();
    }
    public float getPlayerSpirit()
    {
        return playerSpirit;
    }

    public void refreshSpiritCount()
    {
        SpiritCountText.text = playerSpirit.ToString();
    }
}
