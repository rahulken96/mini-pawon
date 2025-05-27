using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StoveCounter;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPiringSpawn;
    public event EventHandler OnPiringRemove;

    [SerializeField] private BendaDapur bendaDapur;

    private float spawnPiringTime;
    private float spawnPiringTimeMax = 4f;
    private int spawnPiringAmount;
    private int spawnPiringAmountMax = 4;

    private void Update()
    {
        spawnPiringTime += Time.deltaTime;

        //Cek waktu berlalu
        if (spawnPiringTime > spawnPiringTimeMax)
        {
            spawnPiringTime = 0f;

            //Cek apakah jumlah piring kurang dari max jumlah piring yang seharusnya
            if (spawnPiringAmount < spawnPiringAmountMax)
            {
                spawnPiringAmount++;

                OnPiringSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void getInteract(PemainController pemain)
    {
        //Cek jika pemain tidak bawa barang
        if (!pemain.HasObjBendaDapur())
        {
            if (spawnPiringAmount > 0)
            {
                spawnPiringAmount--;

                FungsiBendaDapur.MunculkanBendaDapur(bendaDapur, pemain);
                OnPiringRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
