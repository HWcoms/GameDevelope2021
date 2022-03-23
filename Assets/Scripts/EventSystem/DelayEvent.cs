using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEvent : MonoBehaviour
{
    public float delayTime = 0f;

    EventSystemManager esmObj;

    // Start is called before the first frame update
    void Start()
    {
        esmObj = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystemManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(timer(delayTime));
    }

    IEnumerator timer(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        End();
    }
    void End()
    {
        esmObj.EndEvent();
    }
}
