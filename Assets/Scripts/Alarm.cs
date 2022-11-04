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

    private IEnumerator ChangeVolume(float targetVolume)
    {
        _audioSource.Play();

        while(_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeVolumeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void SwitchOn()
    {
        TryStopCoroutine();
        _audioSource.volume = _minVolume;
        _currentCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void SwitchOff()
    {
        TryStopCoroutine();
        _currentCoroutine = StartCoroutine(ChangeVolume(_minVolume));
        if(_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }

    private void TryStopCoroutine()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}