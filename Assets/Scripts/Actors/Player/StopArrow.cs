using UnityEngine;
using System.Collections;

public class StopArrow : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<Rigidbody>().isKinematic = true;
    }
}
