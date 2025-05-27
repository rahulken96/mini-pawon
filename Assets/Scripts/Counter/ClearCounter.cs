using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void getInteract(PemainController pemain)
    {
        //Cek jika ada benda di atas counter
        if (!HasObjBendaDapur())
        {
            //Cek jika pemain bawa barang
            if (pemain.HasObjBendaDapur())
            {
                pemain.GetObjBendaDapur().SetBendaDapurParent(this);
            }
        }
        else
        {
            //Cek jika pemain sedang bawa barang
            if (pemain.HasObjBendaDapur())
            {
                //Cek jika pemain membawa piring
                if (pemain.GetObjBendaDapur().TryGetPiring(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddBahanBahan(GetObjBendaDapur().GetBendaDapurSO()))
                    {
                        GetObjBendaDapur().DestroyBendaDapur();
                    }
                }
                else
                {
                    //Cek jika counter terdapat piring di atasnya
                    if (GetObjBendaDapur().TryGetPiring(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddBahanBahan(pemain.GetObjBendaDapur().GetBendaDapurSO()))
                        {
                            pemain.GetObjBendaDapur().DestroyBendaDapur();
                        }
                    }
                }
            }
            else
            {
                GetObjBendaDapur().SetBendaDapurParent(pemain);
            }
        }
    }
}
