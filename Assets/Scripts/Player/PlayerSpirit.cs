using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpirit : MonoBehaviour
{
    public float playerSpirit { get; set; }

    public void changePlayerSpirit(float amount)
    {
        playerSpirit += amount;
        print("current spirit : " + playerSpirit);
    }
    public float getPlayerSpirit()
    {
        return playerSpirit;
    }
}
