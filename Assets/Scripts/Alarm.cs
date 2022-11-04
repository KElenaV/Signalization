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

        while(_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _changeVolumeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        _audioSource.Play();

        while(_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _changeVolumeSpeed * Time.deltaTime);
            yield return null;
        }
        _audioSource.Stop();
    }

    public void SwitchOn()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(IncreaseVolume());
    }

    public void SwitchOff()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(DecreaseVolume());
    }
}
