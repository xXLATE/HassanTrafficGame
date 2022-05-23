using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCarManager : MonoBehaviour
{
    public List<Sprite> carSprites = new List<Sprite>();

    private Image _carImage;
    private GameObject _menu;
    private int _indexOfSelectCar;

    private void OnEnable()
    {
        _carImage = GameObject.Find("CarImage").GetComponent<Image>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        _menu = GameObject.Find("Menu");

        var selectedCar = PlayerPrefs.GetString("SelectedCar");

        foreach (var sprite in carSprites)
        {
            if (sprite.name == selectedCar)
            {
                _carImage.sprite = sprite;
                _indexOfSelectCar = carSprites.IndexOf(sprite);
            }
        }

    }

    public void NextCar()
    {
        if (_indexOfSelectCar + 1 < carSprites.Count)
            _indexOfSelectCar++;
        else
            _indexOfSelectCar = 0;

        _carImage.sprite = carSprites[_indexOfSelectCar];
    }

    public void PrevCar()
    {
        if (_indexOfSelectCar - 1 >= 0)
            _indexOfSelectCar--;
        else
            _indexOfSelectCar = carSprites.Count - 1;

        _carImage.sprite = carSprites[_indexOfSelectCar];
    }

    public void SelectCar()
    {
        PlayerPrefs.SetString("SelectedCar", _carImage.sprite.name);
        gameObject.SetActive(false);
        _menu.SetActive(true);
    }
}
