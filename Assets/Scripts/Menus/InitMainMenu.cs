using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InitMainMenu : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;
    private GameData data;


    private void Start()
    {
        data = GetComponent<LoadGame>().GameData;

        if (data.Level != -1)
        {
            continueButton.interactable = true;
            continueButton.GetComponentInChildren<Text>().color = Color.white;
        }
    }
}
