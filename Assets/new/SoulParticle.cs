using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulParticle : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;

    public float spiritAmount = 5.0f;

    public Vector3 SpawnOffset;
    public Vector3 EndOffset;
    public float delay = 2.0f;

    private GameObject particle;

    public bool isAnimDone = false;

    private Animator anim;

    public Vector3 AfterSpawnPos;
    bool getPosOnce = true;

    public float yPosLerpOffset = 5.0f;

    public float lerpDuration = 3.0f;

    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //get monster component from EnemyHealth.cs Later
        //monster = GameObject.FindWithTag("Boss");   //remove later(for test)

        player = GameObject.FindWithTag("Player");

        particle = this.gameObject;

        StartCoroutine(SpawnParticle(delay));

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //particle.transform.position = Vector3.Lerp(particle.transform.position, player.transform.position, Time.deltaTime);
        
        if(isAnimDone)
        {
            if(getPosOnce) getCurrentPos();

            FollowPlayer();
        }
    }

    IEnumerator SpawnParticle(float delay) {
        particle.transform.position = monster.transform.position + SpawnOffset;

        yield return new WaitForSeconds(delay);

        SpawnMotion();
    }

    void SpawnMotion()
    {
        anim.SetBool("IsMove", true);
        print("particle spawning motion");
    }

    void FollowPlayer()
    {
        //print("particle moving to player");
/*
        particle.transform.position = new Vector3(
            Mathf.Lerp(particle.transform.position.x, player.transform.position.x,Time.deltaTime * speed ),
            Mathf.Lerp(particle.transform.position.y, particle.transform.position.y + yPosLerpOffset,Time.deltaTime * speed ),
            Mathf.Lerp(particle.transform.position.z, player.transform.position.z,Time.deltaTime * speed )
        );
*/
        particle.transform.position = Vector3.Lerp(particle.transform.position, player.transform.position + EndOffset, Time.deltaTime * speed);
    }

    public void setAnimDone(int flag)
    {
        if(flag == 1)
            isAnimDone = true;
        else
            isAnimDone = false;
    }

    public void getCurrentPos()
    {
        AfterSpawnPos = particle.transform.position;

        getPosOnce = false;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && isAnimDone)
        {
            //player get soul
            player.GetComponent<PlayerSpirit>().changePlayerSpirit(spiritAmount);

            //print("destroy this particle");
            Destroy(this.gameObject);
        }
    }
}
