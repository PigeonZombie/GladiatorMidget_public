using UnityEngine;
using System.Collections;

public class FireballBehavior : MonoBehaviour {

    [SerializeField]
    private float speed = 100;
    [SerializeField]
    private string[] tagsToIgnoreCollision;
    [SerializeField]
    private GameObject explosion;
    private Vector3 target;

	private void Start()
    {
        target = new Vector3(108, -94, -310);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision:"+col.collider.tag);
        bool impact = true;
        foreach(string tag in tagsToIgnoreCollision)
        {
            if(tag == col.collider.tag)
            {
                impact = false;
                break;
            }
        }

        if (impact)
            Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
