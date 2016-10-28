using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuHover : MonoBehaviour {

    [SerializeField]
    private Color hoveredColor;
    [SerializeField]
    private Color _disabledHoveredColor;
    [SerializeField]
    private Color _disabledColor;
    [SerializeField]
    private GamepadItemSelection[] _gamepadSelectors;

    public void Select(Text text)
    {
        foreach (GamepadItemSelection gamepadSelector in _gamepadSelectors)
        {
            if (gamepadSelector.gameObject.active)
                gamepadSelector.DeselectAll();

        }
        if(text.GetComponentInParent<Button>().IsInteractable())
            text.color = hoveredColor;
        else
            text.color = _disabledHoveredColor;
    }

    public void DeSelect(Text text)
    {
        if(text.GetComponentInParent<Button>().IsInteractable())
            text.color = Color.white;
        else
            text.color = _disabledColor;
    }

}
