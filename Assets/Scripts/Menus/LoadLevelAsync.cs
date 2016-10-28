using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevelAsync : MonoBehaviour {

    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private GameObject loadingImage;



    AsyncOperation async;
	
	
	public void ClickToLoadAsync(int level)
    {
        if (level != -1)
        {
            loadingImage.SetActive(true);
            StartCoroutine(LoadLevelWithBar(level));
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit called");
    }

    IEnumerator LoadLevelWithBar(int level)
    {
       async = Application.LoadLevelAsync(level);
       while(!async.isDone)
       {
           loadingSlider.value = async.progress;
           yield return null;
       }
    }
}
