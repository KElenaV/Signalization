using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _changeVolumeSpeed = 0.5f;

    private float _minVolume = 0;
    private float _maxVolume = 1;
    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator IncreaseVolume()
    {
        _audioSource.volume = _minVolume;
        _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            ChangeVolume(_maxVolume);
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        _audioSource.Play();

        while (_audioSource.volume > _minVolume)
        {
            ChangeVolume(_minVolume);
            yield return null;
        }
        _audioSource.Stop();
    }

    private void ChangeVolume(float targetVolume)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeVolumeSpeed * Time.deltaTime);
    }

    public void SwitchOn()
    {
        TryStopCoroutine();
        _currentCoroutine = StartCoroutine(IncreaseVolume());
    }

    public void SwitchOff()
    {
        TryStopCoroutine();
        _currentCoroutine = StartCoroutine(DecreaseVolume());
    }

    private void TryStopCoroutine()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}