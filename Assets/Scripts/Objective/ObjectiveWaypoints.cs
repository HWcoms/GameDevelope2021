using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveWaypoints : MonoBehaviour
{
    public Image objectiveImage;
    public Transform TargetTemp;
    Vector3 Targetpos;

    public bool isMissionStart;

    private Transform player;
    private Text distText;

    // Start is called before the first frame update
    void Start()
    {
        objectiveImage = this.GetComponent<Image>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        distText = this.transform.GetChild(0).GetComponent<Text>();

        Targetpos = TargetTemp.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isMissionStart) {
            objectiveImage.enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        else {
            objectiveImage.enabled = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        TrackImage();

        GetDistance();
    }

    void TrackImage()
    {
        float minX = objectiveImage.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = objectiveImage.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(Targetpos);
        //Vector2 pos = Camera.main.WorldToScreenPoint(Target.GetComponent<EnemyTargetEffect>().GetEffectPos().position);

        if (Vector3.Dot((Targetpos - Camera.main.transform.position), Camera.main.transform.forward) < 0)
        {
            if(pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        objectiveImage.transform.position = pos;
    }

    void GetDistance()
    {
        float dist = Vector3.Distance(player.transform.position, Targetpos) * 2;
        //print("dist: " + dist);

        distText.text = ((int) dist).ToString() + " M";
    }
}
