using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       GetComponentInChildren<TextMeshPro>().isOverlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
