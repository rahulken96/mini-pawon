using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler onPemainGetObj;

    [SerializeField] private BendaDapur objBendaDapur;

    public override void getInteract(PemainController pemain)
    {
        if (!pemain.HasObjBendaDapur())
        {
            FungsiBendaDapur.MunculkanBendaDapur(objBendaDapur, pemain);

            onPemainGetObj?.Invoke(this, EventArgs.Empty); //Mengambil event yang terjadi pada pemain untuk animasi buka/tutup cointainer
        }
    }
}
