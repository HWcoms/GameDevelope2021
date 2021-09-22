using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class alphaFadeOut : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color meshColor;

    public float updateSpeedSeconds = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        meshColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fadeOut()
    {
        StartCoroutine(fadeOutAnim());
    }

    private IEnumerator fadeOutAnim()
    {
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            meshColor.a = Mathf.Lerp(1.0f, 0.0f, elapsed / updateSpeedSeconds);
            yield return null;
        }

        //hpbgImg.fillAmount = percent;
    }
}
