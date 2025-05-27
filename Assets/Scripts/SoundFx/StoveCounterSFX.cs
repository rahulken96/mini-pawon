using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSFX : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter; 

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateMasak += StoveCounter_OnStateMasak;
    }

    private void StoveCounter_OnStateMasak(object sender, StoveCounter.OnStateMasakEventArgs e)
    {
        bool playSound = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);

        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
