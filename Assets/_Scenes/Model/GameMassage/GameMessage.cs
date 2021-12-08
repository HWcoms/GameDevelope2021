using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMessage : MonoBehaviour
{
    private CharacterHealth chScript;
    float stamina;

    [SerializeField]GameObject gameMsgObj;
    [SerializeField] TextMeshProUGUI gameMsgText;
    [SerializeField] Animator gameMsgAnim;

    bool isMsgOn = false;

    Coroutine msgCoroutine;

    public float fadeDelayTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        chScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();

        gameMsgObj = GameObject.Find("GameMessage");

        gameMsgText = gameMsgObj.GetComponent<TextMeshProUGUI>();
        gameMsgAnim = gameMsgObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina = chScript.getStamina();
    }

    public void msgSomething(string str)
    {
        gameMsgText.text = str;

        if (isMsgOn && msgCoroutine != null)
        {
            StopCoroutine(msgCoroutine);
        }

        msgCoroutine = StartCoroutine(msgFadeDelay(fadeDelayTime));
    }

    public void msgNoStamina()
    {
        print("no stamina!");

        gameMsgText.text = "스태미나가 부족합니다!";

        if(isMsgOn && msgCoroutine != null)
        {
            StopCoroutine(msgCoroutine);
        }

        msgCoroutine = StartCoroutine(msgFadeDelay(fadeDelayTime));
    }

    public void msgNoSoulsToHeal()
    {
        print("no Souls To heal!");

        gameMsgText.text = "회복에 필요한 영혼이 부족합니다!";

        if (isMsgOn && msgCoroutine != null)
        {
            StopCoroutine(msgCoroutine);
        }

        msgCoroutine = StartCoroutine(msgFadeDelay(fadeDelayTime));
    }

    public void setMsgOn(bool flag)
    {
        if (flag)
            isMsgOn = true;
        else
            isMsgOn = false;
    }

    IEnumerator msgFadeDelay(float delay)
    {
        setMsgOn(true);
        gameMsgAnim.SetBool("msg", true);

        yield return new WaitForSeconds(delay);

        setMsgOn(false);
        gameMsgAnim.SetBool("msg", false);
    }
}
