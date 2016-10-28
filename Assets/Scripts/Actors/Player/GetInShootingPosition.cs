using UnityEngine;
using System.Collections;

public class GetInShootingPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotation;
    [SerializeField]
    private Vector3 position;
    //[SerializeField]
    //private MeshRenderer _arrow;
    [SerializeField]
    private Transform _player;
    private Transform _originalParent;

    private void Start()
    {
        _originalParent = transform.parent;
    }

    public void GetInPosition()
    {
        transform.parent = _player;
        transform.localPosition = position;
        transform.localRotation = Quaternion.Euler(rotation);
        //_arrow.enabled = true;
    }

    public void GetBackInPosition()
    {
        transform.parent = _originalParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
       // _arrow.enabled = false;
    }
}
