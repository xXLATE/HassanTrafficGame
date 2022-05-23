using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeOfDay : MonoBehaviour
{
    private Animator _night;
    [SerializeField] private GameObject _rain;
    [SerializeField] private List<GameObject> _lights = new List<GameObject>();

    private void Start()
    {
        _night = GetComponent<Animator>();

        StartCoroutine(TimeOfDayChanging());
        StartCoroutine(WeatherChanging());
    }

    private IEnumerator TimeOfDayChanging()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);

            var condition = !_night.GetBool("NightOn");

            _night.SetBool("NightOn", condition);

            foreach (var light in _lights)
                light.SetActive(condition);
        }
    }

    private IEnumerator WeatherChanging()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 120));
            _rain.SetActive(true);
            yield return new WaitForSeconds(Random.Range(10, 30));
            _rain.SetActive(false);
        }
    }
}
