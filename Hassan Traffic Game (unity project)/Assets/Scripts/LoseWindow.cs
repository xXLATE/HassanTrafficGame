using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private Text _loseScoreText;

    private GameManager _gameManager;

    private void Start()
    {
        gameObject.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            OnRestartButtonClick();
    }

    private void OnEnable()
    {
        try
        {
            _loseScoreText.text = $"Ñ×¨Ò: {_gameManager.Score}\nÐÅÊÎÐÄ: {_gameManager.HightScore}";
        }
        catch { }
    }

    public void OnRestartButtonClick()
    {
        Globals.restart = true;
        SceneManager.LoadSceneAsync(0);
    }

    public void OnMenuButtonClick()
    {
        Globals.restart = false;
        SceneManager.LoadSceneAsync(0);
    }
}
