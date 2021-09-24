using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [Range(0.1f, 2.0f)]
    public float modifiedTimeSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = modifiedTimeSpeed;

        if (Input.GetKeyDown(KeyCode.LeftBracket))
            modifiedTimeSpeed -= 0.1f;
        else if (Input.GetKeyDown(KeyCode.RightBracket))
            modifiedTimeSpeed += 0.1f;
        else if (Input.GetKeyDown(KeyCode.P))
            modifiedTimeSpeed = 1.0f;

        if (modifiedTimeSpeed <= 0.1f)
            modifiedTimeSpeed = 0.1f;
        else if (modifiedTimeSpeed >= 2.0f)
            modifiedTimeSpeed = 2.0f;
    }
}
