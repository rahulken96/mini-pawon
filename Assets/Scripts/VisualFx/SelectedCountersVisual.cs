using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCountersVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArr;

    private void Start()
    {
        PemainController.Instance.OnSelectedCounter += Pemain_OnSelectedCounter;
    }

    private void Pemain_OnSelectedCounter(object sender, PemainController.OnSelectedCounterEventArgs e)
    {
        if (e.selectedCounterArgs == baseCounter)
        {
            setShow();
        }
        else
        {
            setHide();
        }
    }

    private void setShow()
    {
        foreach (GameObject visaulGameObject in visualGameObjectArr)
        {
            visaulGameObject.SetActive(true);
        }
    }
    private void setHide()
    {
        foreach (GameObject visaulGameObject in visualGameObjectArr)
        {
            visaulGameObject.SetActive(false);
        }
    }
}
