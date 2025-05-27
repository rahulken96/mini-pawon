using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string PEMAIN_PREFS_VOLUME_SUARA = "VolumeSuara";

    [SerializeField] private AudioClipSO audioClipSO;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PEMAIN_PREFS_VOLUME_SUARA, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnResepBenar += Delivery_OnResepBenar;
        DeliveryManager.Instance.OnResepSalah += Delivery_OnResepSalah;
        CuttingCounter.OnAnyKepotong += Cutting_OnAnyKepotong;
        PemainController.Instance.OnAmbilSesuatu += Pemain_OnAmbilSesuatu;
        BaseCounter.OnAnyObjDitempatkan += BaseCounter_OnAnyObjDitempatkan;
        TrashCounter.OnAnyObjDibuang += TrashCounter_OnAnyObjDibuang;
    }

    private void TrashCounter_OnAnyObjDibuang(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;

        PlaySound(audioClipSO.tempatSampah, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjDitempatkan(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;

        PlaySound(audioClipSO.objJatuh, baseCounter.transform.position);
    }

    private void Pemain_OnAmbilSesuatu(object sender, System.EventArgs e)
    {
        PlaySound(audioClipSO.objAmbil, PemainController.Instance.transform.position);
    }

    private void Cutting_OnAnyKepotong(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;

        PlaySound(audioClipSO.motong, cuttingCounter.transform.position);
    }

    private void Delivery_OnResepBenar(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;

        PlaySound(audioClipSO.benarOrderan, deliveryCounter.transform.position);
    }

    private void Delivery_OnResepSalah(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;

        PlaySound(audioClipSO.salahOrderan, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArr, Vector3 position, float volume = 1f)
    {
        int indexAudioClip = Random.Range(0, audioClipArr.Length);
        PlaySound(audioClipArr[indexAudioClip], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayLangkahKakiSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipSO.langkahKaki, position, volume);
    }

    public void UbahVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PEMAIN_PREFS_VOLUME_SUARA, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
