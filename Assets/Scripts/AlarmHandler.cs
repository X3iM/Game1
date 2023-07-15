using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmHandler : MonoBehaviour
{
    [SerializeField] private float _volumeChangeRate = 0.01f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0;

    private AudioSource _audioSource;
    private float _targetVolume;
    private Coroutine _job;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Door[] doors = FindObjectsOfType<Door>();

        foreach (Door door in doors)
        {
            door.DoorOpened += OnDoorOpen;
            door.DoorClosed += DoorClosed;
        }
    }

    private void OnDoorOpen()
    {
        if (_job != null)
            StopCoroutine(_job);

        _targetVolume = _maxVolume;
        _audioSource.Play();
        _job = StartCoroutine(ChangeVolumeAlarm());
    }
    
    private void DoorClosed()
    {
        if (_job != null)
            StopCoroutine(_job);

        _targetVolume = _minVolume;
        _job = StartCoroutine(ChangeVolumeAlarm());
    }

    private IEnumerator ChangeVolumeAlarm()
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeRate);
        while (Mathf.Abs(_audioSource.volume - _targetVolume) > 0)
        {
            float volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, Time.deltaTime);
            _audioSource.volume = volume;
            yield return wait;
        }
    }
}