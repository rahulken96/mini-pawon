using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjDibuang;

    new public static void ResetStaticData()
    {
        OnAnyObjDibuang = null;
    }

    public override void getInteract(PemainController pemain)
    {
        if (pemain.HasObjBendaDapur())
        {
            pemain.GetObjBendaDapur().DestroyBendaDapur();

            OnAnyObjDibuang?.Invoke(this, EventArgs.Empty);
        }
    }
}
