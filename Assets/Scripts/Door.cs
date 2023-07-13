using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action DoorOpened;
    public event Action DoorClosed;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (!target.TryGetComponent(out Rogue rogue))
            return;

        CheckEnterDirection(target);
    }

    private void CheckEnterDirection(Collider2D target)
    {
        float enterDirection = transform.position.x - target.transform.position.x;
        switch (enterDirection)
        {
            case < 0:
                DoorClosed?.Invoke();
                break;

            case > 0:
                DoorOpened?.Invoke();
                break;
        }
    }
}