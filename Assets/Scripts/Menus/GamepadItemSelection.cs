using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamepadItemSelection : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Color _hoverColor;
    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Color _disabledHoveredColor;
    [SerializeField]
    private Color _disabledColor;
    [SerializeField]
    private GameObject _previousPanel;
    [SerializeField]
    private SaveGame _gameSaver;

    private GamepadMenuInputHandler _gamepadInput;
    private PauseMenuHandler _pauseMenuHandler;
    private int _selectedIndex;
    private float _changeSelectionDelay = 15f;
    private float _nextSelectionChange = 0;
    private float _clickDelay = 5f;
    private float _nextClick;
    private float sliderValueStep = 0.05f;

    private void Start()
    {
        _gamepadInput = GetComponent<GamepadMenuInputHandler>();
        _pauseMenuHandler = GetComponentInParent<PauseMenuHandler>();
        _gamepadInput.OnChangeSelection += ChangeSelection;
        _gamepadInput.OnSelect += Select;
        _gamepadInput.OnBack += Back;
        _gamepadInput.OnChangeSliderValue += ChangeSliderValue;


        _selectedIndex = 0;

    }

    private void OnEnable()
    {
        _selectedIndex = 0;
        DeselectAll();
        buttons[_selectedIndex].GetComponentInChildren<Text>().color = _hoverColor;
        _nextClick = 0;
    }

    private void Update()
    {
        _nextSelectionChange++;
        _nextClick++;
    }

    private void OnDestroy()
    {
        _gamepadInput.OnChangeSelection -= ChangeSelection;
        _gamepadInput.OnSelect -= Select;
        _gamepadInput.OnBack -= Back;
        _gamepadInput.OnChangeSliderValue -= ChangeSliderValue;
    }

    private void ChangeSliderValue(float direction)
    {
        Slider sliderToChange = buttons[_selectedIndex].GetComponentInChildren<Slider>();
        if (sliderToChange != null)
        {
            if (direction > 0)
                sliderToChange.value += sliderValueStep;
            else
                sliderToChange.value -= sliderValueStep;
            sliderToChange.onValueChanged.Invoke(sliderToChange.value);
        }
    }

    private void Select()
    {
        if (_nextClick > _clickDelay)
        {
            if (_selectedIndex != -1)
            {
                buttons[_selectedIndex].onClick.Invoke();
                _nextClick = 0;
            }
        }
    }

    private void Back()
    {
        if (_nextClick >= _clickDelay)
        {
            if (_previousPanel != null)
            {
                _gameSaver.Save();
                gameObject.SetActive(false);
                _previousPanel.SetActive(true);
            }
            else
            {
                _pauseMenuHandler.OpenCloseMenu();                
            }
        }
    }

    private void ChangeSelection(float yDirection)
    {
        if (_selectedIndex != -1)
        {
            if (_nextSelectionChange >= _changeSelectionDelay)
            {
                if(buttons[_selectedIndex].IsInteractable())
                    buttons[_selectedIndex].GetComponentInChildren<Text>().color = _normalColor;
                else
                    buttons[_selectedIndex].GetComponentInChildren<Text>().color = _disabledColor;

                if (yDirection > 0)
                    SelectUp();
                else
                    SelectDown();

                if (buttons[_selectedIndex].IsInteractable())
                    buttons[_selectedIndex].GetComponentInChildren<Text>().color = _hoverColor;
                else
                    buttons[_selectedIndex].GetComponentInChildren<Text>().color = _disabledHoveredColor;

                _nextSelectionChange = 0;
            }
        }

    }

    private void SelectUp()
    {
        if (_selectedIndex <= 0)
            _selectedIndex = buttons.Length - 1;
        else
            _selectedIndex--;
    }

    private void SelectDown()
    {
        if (_selectedIndex == buttons.Length - 1)
            _selectedIndex = 0;
        else
            _selectedIndex++;
    }

    public void DeselectAll()
    {
        foreach (Button button in buttons)
        {
            if(button.IsInteractable())
                button.GetComponentInChildren<Text>().color = _normalColor;
            else
            {
                button.GetComponentInChildren<Text>().color = Color.gray;
            }
        }

    }




}
