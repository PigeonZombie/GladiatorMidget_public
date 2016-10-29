using UnityEngine;
using System.Collections;

public class ThrowFireBall : MonoBehaviour {

    [SerializeField]
    private GameObject fireBall;
    private float nextShot;
    private int minShootingRythm = 10;
    private int maxShootingRyhtm = 30;
    private System.Random rnd;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();

        rnd = new System.Random();
        //nextShot = Time.time + rnd.Next(minShootingRythm, maxShootingRyhtm);
        nextShot = 1;


    }

    private void FixedUpdate()
    {
        if(Time.fixedTime >= nextShot)
        {
            GameObject newFireBall = (GameObject)Instantiate(fireBall, transform.position, transform.rotation, null);
            
            nextShot = Time.time + rnd.Next(minShootingRythm, maxShootingRyhtm);
        }
    }
	
}
