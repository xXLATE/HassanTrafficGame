using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private string _difficulty;
    private Text _scoreText;
    private Text _hightScoreText;
    private List<Sprite> _carSprites;
    private SoundManager _soundManager;

    private float _score = 0;
    public float Score
    {
        get
        {
            return System.Convert.ToInt32(_scoreText.text.Split(' ')[1]);
        }
        set
        {
            _score += value;
            _scoreText.text = "Ñ÷¸ò: " + Mathf.RoundToInt(_score).ToString();
        }
    }

    public int HightScore
    {
        get
        {
            return System.Convert.ToInt32(_hightScoreText.text.Split(' ')[1]);
        }
        set
        {
            _hightScoreText.text = "Ðåêîðä: " + value.ToString();
        }
    }

    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _carSprites = FindObjectOfType<SelectCarManager>().carSprites;

        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            Globals.carsCount = 4;
            Globals.movementSpeed = 4;

            _difficulty = "Medium";
            PlayerPrefs.SetString("Difficulty", "Medium");
            PlayerPrefs.SetString("SelectedCar", "cars_70");
        }
        else
        {
            _difficulty = PlayerPrefs.GetString("Difficulty");

            if (_difficulty == "Easy")
            {
                Globals.carsCount = 3;
                Globals.movementSpeed = 3;
            }
            else if (_difficulty == "Medium")
            {
                Globals.carsCount = 4;
                Globals.movementSpeed = 4;
            }
            else if (_difficulty == "Hard")
            {
                Globals.carsCount = 5;
                Globals.movementSpeed = 5;
            }
        }

        gameObject.SetActive(false);
    }

    private void Start()
    {
        _soundManager.PlayTrafficSound();

        var player = FindObjectOfType<Player>();

        foreach (var sprite in _carSprites)
        {
            if (sprite.name == PlayerPrefs.GetString("SelectedCar"))
            {
                player.GetComponent<SpriteRenderer>().sprite = sprite;
                Destroy(player.GetComponent<CapsuleCollider2D>());
                var a = player.gameObject.AddComponent<CapsuleCollider2D>();
                a.direction = CapsuleDirection2D.Horizontal;
            }
        }

        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _hightScoreText = GameObject.Find("HightScoreText").GetComponent<Text>();

        _difficulty = PlayerPrefs.GetString("Difficulty");

        if (PlayerPrefs.HasKey($"HightScore{_difficulty}"))
            HightScore = PlayerPrefs.GetInt($"HightScore{_difficulty}");
    }

    private void FixedUpdate()
    {
        Score = Globals.movementSpeed * Time.fixedDeltaTime;

        if (Score >= HightScore)
            HightScore = System.Convert.ToInt32(Score);
    }

    public void CheckHightScore()
    {
        _soundManager.PlayCrashSound();

        if (Score == HightScore)
            PlayerPrefs.SetInt($"HightScore{_difficulty}", System.Convert.ToInt32(Score));
    }
}
