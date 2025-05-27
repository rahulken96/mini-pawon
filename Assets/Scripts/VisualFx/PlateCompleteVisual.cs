using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<BendaDapur_GameObject> bendaDapurGameObjectList;

    [Serializable]
    public struct BendaDapur_GameObject
    {
        public BendaDapur BendaDapur;
        public GameObject gameObject;
    }

    private void Start()
    {
        plateKitchenObject.OnTambahanBahan += PlateKitchenObject_OnTambahanBahan;

        foreach (BendaDapur_GameObject objBendaDapurGameObject in bendaDapurGameObjectList)
        {
            objBendaDapurGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnTambahanBahan(object sender, PlateKitchenObject.OnTambahanBahanEventArgs e)
    {
        foreach(BendaDapur_GameObject objBendaDapurGameObject in bendaDapurGameObjectList)
        {
            if (objBendaDapurGameObject.BendaDapur == e.bendaDapur)
            {
                objBendaDapurGameObject.gameObject.SetActive(true);
            }
        }
    }
}
