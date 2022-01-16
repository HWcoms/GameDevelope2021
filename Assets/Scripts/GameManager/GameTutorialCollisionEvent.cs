using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialCollisionEvent : MonoBehaviour
{
    public List<GameObject> tutObjList = new List<GameObject>();

    int childCount;

    [SerializeField] bool oncePlayed = false;

    public float delayTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        oncePlayed = false;

        childCount = this.transform.childCount;

        for (int i = 0; i < childCount; ++i)
        {
            tutObjList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject childs in tutObjList)
        {
            childs.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !oncePlayed)
        {
            print("Tutorial enable");

            oncePlayed = true;

            foreach (GameObject childs in tutObjList)
            {
                childs.SetActive(true);
            }

            StartCoroutine(offDisplay(delayTime));
        }
    }

    IEnumerator offDisplay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject childs in tutObjList)
        {
            childs.SetActive(false);
        }
    }
}
