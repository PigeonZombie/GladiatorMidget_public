using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    private void Start()
    {
        GetComponent<Animation>()["jump"].wrapMode = WrapMode.Loop;
    }
}
