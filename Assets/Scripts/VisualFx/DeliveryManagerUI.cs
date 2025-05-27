using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform templateResep;

    private void Awake()
    {
        templateResep.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnSpawnOrderan += DeliveryManager_OnSpawnOrderan;
        DeliveryManager.Instance.OnCompletedOrderan += DeliveryManager_OnCompletedOrderan;

        UpdateVisual();
    }


    private void DeliveryManager_OnSpawnOrderan(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnCompletedOrderan(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == templateResep) continue;
            Destroy(child.gameObject);
        }

        foreach (ResepSO resepSO in DeliveryManager.Instance.GetOrderanResepSOList())
        {
            Transform resepTransform = Instantiate(templateResep, container);
            resepTransform.gameObject.SetActive(true);
            resepTransform.GetComponent<DeliveryManagerSingleUI>().SetResepSO(resepSO);
        }
    }
}
