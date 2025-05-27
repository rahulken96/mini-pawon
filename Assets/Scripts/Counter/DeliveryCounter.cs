using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void getInteract(PemainController pemain)
    {
        if (pemain.HasObjBendaDapur())
        {
            if (pemain.GetObjBendaDapur().TryGetPiring(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverResep(plateKitchenObject);

                pemain.GetObjBendaDapur().DestroyBendaDapur();
            }
        }
    }
}
