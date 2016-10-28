using UnityEngine;
using System.Collections;

public class EntranceTrigger : MonoBehaviour {


    public delegate void PlayerEnteredArenaHandler();
    public event PlayerEnteredArenaHandler OnPlayerEnteredArena;


	private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponentInParent<MeshRenderer>().enabled = true;
            transform.parent.GetComponent<BoxCollider>().enabled = true;
            if (OnPlayerEnteredArena != null)
                OnPlayerEnteredArena();
            this.gameObject.SetActive(false);
        }
    }
}
