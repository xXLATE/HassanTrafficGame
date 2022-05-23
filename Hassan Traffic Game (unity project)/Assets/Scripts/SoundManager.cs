using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private AudioSource _trafficSound;

    private void Start()
    {
        _music.time = Random.Range(0, _music.clip.length);

        StartCoroutine(MusicTransition(_music));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _clickSound.Play();
    }

    private IEnumerator MusicTransition(AudioSource audio)
    {
        audio.Play();
        audio.volume = 0;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            audio.volume += 0.01f;
        }
    }

    public void PlayTrafficSound()
    {
        StartCoroutine(MusicTransition(_trafficSound));
    }

    public void PlayCrashSound()
    {
        _crashSound.PlayOneShot(_crashSound.clip);
        _trafficSound.Pause();
        _music.pitch = 0.2f;
        _music.volume = 0.3f;
    }
}
