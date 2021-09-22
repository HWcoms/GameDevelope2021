using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollider : MonoBehaviour
{
    [SerializeField] private PlayerWeapon playerWeaponScript;

    // Start is called before the first frame update
    void Start()
    {
        playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            playerWeaponScript.playHitParticle(other.transform);

            //if (isDealready)
            {
                //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();


                //isDealready = false;

            }
        }
    }
}
