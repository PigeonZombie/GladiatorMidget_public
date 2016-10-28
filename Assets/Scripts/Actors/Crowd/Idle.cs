using UnityEngine;
using System.Collections;

public class Idle : MonoBehaviour {

	private void Start ()
	{
	    GetComponent<Animation>()["idle_normal"].wrapMode = WrapMode.Loop;
	}
	

}
