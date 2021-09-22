using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    

    private GameObject hitDetector;
    [SerializeField] private Collider WeaponCollider;
    [SerializeField] private bool isHitDetected;

    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject hitParticleTextObj;
    private TextMeshPro hitParticleText;

    void Start()
    {
        hitDetector = GameObject.FindGameObjectWithTag("Player").transform.Find("hitDetector").gameObject;
        //WeaponCollider = this.GetComponent<Collider>();
        WeaponCollider = hitDetector.GetComponent<Collider>();

        isHitDetected = false;

        hitParticleText = hitParticleTextObj.GetComponentInChildren<TextMeshPro>();
        hitParticleText.text = "0";
    }

    void Update()
    {

    }

    /*
    private void OnTriggerStay(Collider other)
    {

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            //if (isDealready)
            {
                //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                

                //isDealready = false;
                
            }
        }
    }
    */

    public void playHitParticle(Transform pos)
    {
        hitParticleText.text = ((int)damage).ToString();
        GameObject.Instantiate(hitParticleTextObj, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(pos.transform.position), transform.rotation);
    }

    public bool getHitDetector()
    {
        return isHitDetected;
    }

    public float getDamage()
    {
        return damage;
    }

    public void switchCollider()
    {
        WeaponCollider.enabled = !WeaponCollider.enabled;
    }

    public void switchCollider(bool flag)
    {
        WeaponCollider.enabled = flag;
    }

    public void switchHitDetector()
    {
        isHitDetected = !isHitDetected;
    }

    public void switchHitDetector(int flag)
    {
        bool fl = (flag == 0 ? false : true);

        isHitDetected = fl;
    }

    public void switchHitDetector(bool flag)
    {
        isHitDetected = flag;
    }
}
