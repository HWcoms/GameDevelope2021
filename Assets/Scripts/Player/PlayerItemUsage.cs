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

    [SerializeField] private float itemUsableTime = 1.0f;   //item cooldown
    [SerializeField] private bool itemUsableNow = true;

    // Start is called before the first frame update
    void Start()
    {
        CHscript = GetComponent<CharacterHealth>();

        HealCountText = GameObject.Find("Heal Count").gameObject.GetComponent<TextMeshProUGUI>();

        itemUsableNow = true;
    }

    // Update is called once per frame
    void Update()
    {
        HealCountText.text = getHealItemCount().ToString();

        if(CHscript.getDead()) return;

        //use heal item
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(itemUsableNow)
            {
                useHealItem(1);
                setItemUsable(false);

                //item cooldown timer starts
                StartCoroutine(ItemCoolDown(itemUsableTime));
            }
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
        
        if (healItemCount - count < 0)
        {
            print("no items");
            return -1;
        }
        

        healItemCount -= count;

        CHscript.changeHp(healAmount);

        return count;
    }

    void setItemUsable(bool flag)
    {
        itemUsableNow = flag;
    }

    IEnumerator ItemCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        setItemUsable(true);
    }
}
