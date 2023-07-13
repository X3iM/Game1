using System.Collections;
using UnityEngine;

public class AlarmHandler : MonoBehaviour
{
    [SerializeField] private float _volumeChangeRate = 0.01f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0;
    private AudioSource _audioSource;

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
        _audioSource.Play();
        StartCoroutine(IncreaseVolumeAlarm());
    }

    private IEnumerator IncreaseVolumeAlarm()
    {
        _audioSource.volume += _volumeChangeRate;
        
        if (_audioSource.volume < _maxVolume)
        {
            yield return new WaitForSeconds(_volumeChangeRate);
            yield return IncreaseVolumeAlarm();
        }
        yield return new WaitForSeconds(_volumeChangeRate); 
    }
    private void DoorClosed()
    {
        StartCoroutine(DecreaseVolumeAlarm());
    }

    private IEnumerator DecreaseVolumeAlarm()
    {
        _audioSource.volume -= _volumeChangeRate;
        
        if (_audioSource.volume > _minVolume)
        {
            yield return new WaitForSeconds(_volumeChangeRate);
            yield return DecreaseVolumeAlarm();
        }
        
        _audioSource.Stop();
        yield return new WaitForSeconds(_volumeChangeRate);
    }
}