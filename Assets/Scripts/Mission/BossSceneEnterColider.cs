using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneEnterColider : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "BossDoorCol")
        {
            if(Input.GetButtonDown("Interact"))
            {
                SceneManager.LoadScene("BossScene");
            }
        }
    }
}
