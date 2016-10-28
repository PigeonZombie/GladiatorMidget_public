using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    public GameObject loadingImage;

	void Start () {
	
	}
	
	public void LoadScene(int level)
    {
        loadingImage.SetActive(true);

        Application.LoadLevel(level);
    }
}
