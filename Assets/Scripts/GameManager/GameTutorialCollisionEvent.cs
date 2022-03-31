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
    public float fadeSpeed = 15.0f;

    public List<GameObject> wallObjs;


    // Start is called before the first frame update
    void Start()
    {
        getWallObjs();

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

    void getWallObjs()
    {
        wallObjs.Clear();

        Transform parentWall = transform.Find("Walls");
        wallObjs = AllChilds(parentWall.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(oncePlayed && !isDelay)  //if not control by delay
        {
            if(Input.GetKeyDown(KeyCode.R))
                StartCoroutine(offDisplay(0.0f));
        }

        if(wallObjs.Count == 0 || !wallObjs[0].activeSelf) return;

        foreach(GameObject wallObj in wallObjs)
        {
            //print("rendered");

            changeAlpha(wallObj);
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
            StartCoroutine(wallFadeIn(fadeSpeed));

            

            if(isDelay)
            {
                StartCoroutine(offDisplay(delayTime));
            }
        }
    }

    void changeAlpha(GameObject obj)
    {
        Color col;

        MeshRenderer meshRenderer = null;
        Renderer particleSystemRenderer = null;

        try
        {
            meshRenderer = obj.GetComponent<MeshRenderer>();
            particleSystemRenderer = obj.GetComponent<ParticleSystem>().GetComponent<Renderer>();
        }
        catch
        {
        }

        if (meshRenderer == null && particleSystemRenderer == null) return;

        if (meshRenderer != null && meshRenderer.material.shader.name.Equals("HDRP/Unlit"))
        {
            col = meshRenderer.material.GetColor("_UnlitColor");

            //print(meshRenderer.material.name + ": " + meshRenderer.material.shader.name);
            col.a = wallAlpha;
            obj.GetComponent<MeshRenderer>().material.SetColor("_UnlitColor", col);
        }


        //print(meshRenderer.material.name);
        if (particleSystemRenderer != null && particleSystemRenderer.material.shader.name.Equals("Slash/Bird"))
        {
            col = particleSystemRenderer.material.GetColor("_TintColor");

            //print(particleSystemRenderer.material.name + ": " + particleSystemRenderer.material.shader.name);
            col.a = wallAlpha;

            particleSystemRenderer.material.SetColor("_TintColor", col);
        }
    }

    IEnumerator offDisplay(float delay)
    {
        //StartCoroutine(wallFadeOut());
        //yield return new WaitForSeconds(delay);

        yield return StartCoroutine(wallFadeOut(fadeSpeed));

        foreach (GameObject childs in tutObjList)
        {
            childs.SetActive(false);
        }
    }

    IEnumerator wallFadeIn(float speed = 15.0f, float delay = 0.05f)
    {
        while(wallAlpha < 1.0f)
        {
            //print(wallAlpha);

            yield return new WaitForSeconds(delay);
            wallAlpha += 0.01f * speed;
        }
        wallAlpha = 1;
    }

    IEnumerator wallFadeOut(float speed = 15.0f, float delay = 0.05f)
    {
        while(wallAlpha >= 0.0f)
        {
            //print(wallAlpha);

            yield return new WaitForSeconds(delay);
            wallAlpha -= 0.01f * speed;
        }
        wallAlpha = 0;
    }

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }
    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }
}
