using UnityEngine;
using System.Collections;

public class PlayerPunch : MonoBehaviour {

    [SerializeField]
    private float punchRate = 0.1f;

    private Animation anim;
    private float nextPunch = 0.0f;

	private void Start () {
        anim = GetComponent<Animation>();
	}

    private void Update() 
    {
	    if(Input.GetKeyDown(KeyCode.F) && Time.time > nextPunch)
        {
            anim.Play("punch2");
            nextPunch = Time.time + punchRate;
        }
	}

}
