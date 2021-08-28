using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItemUsage : MonoBehaviour
{
    private CharacterHealth CHscript;
    [SerializeField] private TextMeshProUGUI HealCountText;

    [SerializeField] private int healItemCount = 5;
    [SerializeField] private float healAmount = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        CHscript = GetComponent<CharacterHealth>();

        HealCountText = GameObject.Find("Heal Count").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HealCountText.text = getHealItemCount().ToString();

        //use heal item
        if (Input.GetKeyDown(KeyCode.E))
        {
            useHealItem(1);
        }
    }

    public int getHealItemCount()
    {
        return healItemCount;
    }

    public void setHealItemCount(int value)
    {
        healItemCount = value;
    }

    public int useHealItem (int count)
    {
        /*
        if (healItemCount <= 0)
        {

        }
        */

        healItemCount -= count;

        CHscript.changeHp(healAmount);

        return count;
    }
}
