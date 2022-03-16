using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialCollisionEvent : MonoBehaviour
{
    public List<GameObject> tutObjList = new List<GameObject>();

    int childCount;

    [SerializeField] bool oncePlayed = false;


    
    public bool isDelay = true; //if not trigger by some condition

    /*
    public bool isKey = false;
    public bool isCondition = false;
    */

    public float delayTime = 3.0f;

    private float wallAlpha = 0.0f;

    public GameObject[] wallObjs;


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
        foreach(GameObject obj in wallObjs)
        {
            /*
            Color32 col = obj.material.GetColor("_Color");
            col.a = wallAlpha;
            obj.material.SetColor("_Color", col);*/
        }

        if(oncePlayed && !isDelay)  //if not control by delay
        {
            if(Input.GetKeyDown(KeyCode.R))
                StartCoroutine(offDisplay(0.5f));
        }

        if(wallObjs.Length == 0 || !wallObjs[0].activeSelf) return;

        foreach(GameObject wallObj in wallObjs)
        {
            print("rendered");
            //print(wallObjs[0].name);

            Color col = wallObj.GetComponent<MeshRenderer>().material.color;

            print(wallObj.GetComponent<MeshRenderer>().material.color);
            print(wallObj.GetComponent<MeshRenderer>().material.name);

            col.a = 0;//(int) (wallAlpha * 100);
            wallObj.GetComponent<MeshRenderer>().material.color = col;
        }
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

            //alpha
            StartCoroutine(wallFadeIn());

            

            if(isDelay)
            {
                StartCoroutine(offDisplay(delayTime));
            }
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

    IEnumerator wallFadeIn(float speed = 1.0f, float delay = 0.1f)
    {
        while(wallAlpha < 1.0f)
        {
            //print(wallAlpha);

            yield return new WaitForSeconds(delay);
            wallAlpha += 0.01f * speed;
        }
    }

    IEnumerator wallFadeOut(float speed = 1.0f, float delay = 0.1f)
    {
        while(wallAlpha >= 0.0f)
        {
            //print(wallAlpha);

            yield return new WaitForSeconds(delay);
            wallAlpha -= 0.01f * speed;
        }
    }
}
