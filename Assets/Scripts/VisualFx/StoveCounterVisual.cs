using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private GameObject stoveOnGameObject;

    private void Start()
    {
        stoveCounter.OnStateMasak += StoveCounter_OnStateMasak;
    }

    private void StoveCounter_OnStateMasak(object sender, StoveCounter.OnStateMasakEventArgs e)
    {
        bool showVisual = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);
        particlesGameObject.SetActive(showVisual);
        stoveOnGameObject.SetActive(showVisual);
    }
}
