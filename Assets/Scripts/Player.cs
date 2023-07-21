using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag(Constants.FRUIT_TAG))
        {
            Destroy(target.gameObject);
        }
    }
}