using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    private Text _selectDiffText;
    private Animator _menuAnimator;

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _selectCar;
    [SerializeField] private GameObject _carObject;
    [SerializeField] private GameObject _information;
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private GameObject _settingsButtons;
    [SerializeField] private GameObject _containerButtons;
    [SerializeField] private GameObject _containerObjects;
    [SerializeField] private List<Text> _difficultyButtons = new List<Text>();

    private void Start()
    {
        _menuAnimator = GetComponent<Animator>();
        Time.timeScale = 1;

        if (Globals.restart)
            OnPlayButtonClick();

        Globals.restart = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            OnPlayButtonClick();
    }

    public void OnPlayButtonClick()
    {
        _game.SetActive(true);
        _menuAnimator.SetTrigger("MenuOff");
        _containerObjects.SetActive(true);
        _containerButtons.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnSettingsButtonClick()
    {
        _menuButtons.SetActive(false);
        _information.SetActive(true);
        _settingsButtons.SetActive(true);
        _carObject.SetActive(true);
        _information.GetComponent<Animator>().SetBool("On", true);

        var difficulty = PlayerPrefs.GetString("Difficulty");

        if (difficulty == "Easy")
            _selectDiffText = _difficultyButtons[0];
        else if (difficulty == "Medium")
            _selectDiffText = _difficultyButtons[1];
        else if (difficulty == "Hard")
            _selectDiffText = _difficultyButtons[2];

        _selectDiffText.color = Color.green;
    }

    public void OnEasyButtonClick()
    {
        SetDifficulty("Easy", 3);
    }

    public void OnMediumButtonClick()
    {
        SetDifficulty("Medium", 4);
    }

    public void OnHardButtonClick()
    {
        SetDifficulty("Hard", 5);
    }

    private void SetDifficulty(string diff, int diffValue)
    {
        PlayerPrefs.SetString("Difficulty", diff);

        Globals.carsCount = diffValue;
        Globals.movementSpeed = diffValue;

        _selectDiffText.color = Color.white;
        _selectDiffText = _difficultyButtons[diffValue - 3];
        _selectDiffText.color = Color.green;

        _menuButtons.SetActive(true);
        _settingsButtons.SetActive(false);
        _carObject.SetActive(false);
        _information.GetComponent<Animator>().SetBool("On", false);
    }

    public void OffMenu()
    {
        _menu.SetActive(false);
        _containerButtons.SetActive(true);
        _containerObjects.SetActive(false);
    }

    public void OnSelectCarButtonClick()
    {
        _selectCar.SetActive(true);
        _menuAnimator.SetTrigger("MenuOff");
        _containerObjects.SetActive(true);
        _containerButtons.SetActive(false);
    }
}
