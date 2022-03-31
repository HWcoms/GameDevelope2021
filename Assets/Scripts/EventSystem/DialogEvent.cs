using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    EventSystemManager esmObj;

    DialogPrint dPrintScript;

    public int id = -1;
    public string idString;

    void OnEnable()
    {
        dPrintScript.ViewUI();

        StartCoroutine(WaitforDialogLoad());
    }

    // Start is called before the first frame update
    void Start()
    {
        esmObj = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystemManager>();
        dPrintScript = GameObject.Find("DialogPanel").GetComponent<DialogPrint>();

        StartCoroutine(WaitforDialogLoad());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Next();
        }
    }

    void StartDialogPrint()
    {
        if (id != -1)
        {
            print("loading id: " + id);
            dPrintScript.PrintDialog(id);
        }
        else if(idString.Length != 0)
        {
            print("loading idString: ");
            dPrintScript.PrintDialog(idString);
        }
        else
        {
            print("id or idString is anavailable value");
        }
    }

    void Next()
    {
        if (!dPrintScript.NextDialog())
            esmObj.EndEvent();
    }

    IEnumerator WaitforDialogLoad()
    {
        while (!dPrintScript.getDialogLoaded())
        {
            yield return new WaitForSeconds(0.5f);
        }

        StartDialogPrint();
    }
}
