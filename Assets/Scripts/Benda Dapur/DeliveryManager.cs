using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnSpawnOrderan;
    public event EventHandler OnCompletedOrderan;
    public event EventHandler OnResepBenar;
    public event EventHandler OnResepSalah;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private ResepListSO resepListSO;

    private List<ResepSO> orderanList;
    private float spawnResepTimer;
    private float spawnResepTimerMax = 4f;
    private float spawnOrderanMax = 4;
    private int jumlahOrderanYangDiselesaikan;

    private void Awake()
    {
        Instance = this;

        orderanList = new List<ResepSO>();
    }

    private void Update()
    {
        spawnResepTimer -= Time.deltaTime;
        if (spawnResepTimer <= 0f )
        {
            spawnResepTimer = spawnResepTimerMax;

            if(orderanList.Count < spawnOrderanMax)
            {
                int resepIndex = UnityEngine.Random.Range(0, resepListSO.resepSOList.Count);
                ResepSO dataOrderan = resepListSO.resepSOList[resepIndex];

                orderanList.Add(dataOrderan);

                OnSpawnOrderan?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverResep(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < orderanList.Count; i++)
        {
            ResepSO dataOrderan = orderanList[i];

            //Cek jika resep memiliki jumlah yang sama dengan bahan makanan
            if (dataOrderan.bendaDapurList.Count == plateKitchenObject.GetBendaDapurList().Count)
            {
                bool isResepSamaYangDiPiring = true;

                //Melakukan perulangan unutk melihat bahan makanan
                foreach(BendaDapur objDataOrder in dataOrderan.bendaDapurList)
                {
                    bool isBahanMakananAda = false;

                    //Melakukan perulangan untuk melihat bahan makanan di piring
                    foreach (BendaDapur objPlateKitchenObject in plateKitchenObject.GetBendaDapurList())
                    {
                        //Cek jika bahan makanan yang di piring cocok dengan bahan makanan di resep
                        if (objPlateKitchenObject == objDataOrder)
                        {
                            isBahanMakananAda = true;
                            break;
                        }
                    }

                    //Cek jika resep bahan makanan tidak ada di piring
                    if (!isBahanMakananAda)
                    {
                        isResepSamaYangDiPiring = false;
                    }
                }

                //Cek pemain apakah mengirimkan pesanan sesuai denga resep yang ada
                if (isResepSamaYangDiPiring)
                {
                    jumlahOrderanYangDiselesaikan++;
                    orderanList.RemoveAt(i);

                    OnCompletedOrderan?.Invoke(this, EventArgs.Empty);
                    OnResepBenar?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnResepSalah?.Invoke(this, EventArgs.Empty);
    }

    public List<ResepSO> GetOrderanResepSOList ()
    {
        return orderanList;
    }

    public int GetJumlahOrderanYangDiselesaikan()
    {
        return jumlahOrderanYangDiselesaikan;
    }
}
