using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnTambahanBahan += PlateKitchenObject_OnTambahanBahan;
    }

    private void PlateKitchenObject_OnTambahanBahan(object sender, PlateKitchenObject.OnTambahanBahanEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(BendaDapur objBendaDapur in plateKitchenObject.GetBendaDapurList())
        {
            Transform iconTemplateTransform = Instantiate(iconTemplate, transform);
            iconTemplateTransform.gameObject.SetActive(true);
            iconTemplateTransform.GetComponent<PlateSingleIconUI>().SetBendaDapur(objBendaDapur);
        }
    }
}
