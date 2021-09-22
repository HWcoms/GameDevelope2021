using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class alphaFadeOut : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmpText;
    public float fadeSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        tmpText = this.GetComponent<TextMeshPro>();
    }

    void textFadeOut()
    {
        StartCoroutine(fadeOutAnim(fadeSpeed, tmpText));
    }

    private IEnumerator fadeOutAnim(float timeSpeed, TextMeshPro text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
        Destroy(this.transform.parent.gameObject);
    }
}
