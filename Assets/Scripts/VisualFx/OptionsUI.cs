using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button BtnSuara;
    [SerializeField] private Button BtnMusik;
    [SerializeField] private Button BtnTutup;
    [SerializeField] private TextMeshProUGUI TextSuara;
    [SerializeField] private TextMeshProUGUI TextMusik;

    private void Awake()
    {
        Instance = this;

        BtnSuara.onClick.AddListener(() =>
        {
            SoundManager.Instance.UbahVolume();
            UpdateVisual();
        });
        
        BtnMusik.onClick.AddListener(() =>
        {
            MusicManager.Instance.UbahVolume();
            UpdateVisual();
        });

        BtnTutup.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;

        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        TextSuara.text = "Suara : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        TextMusik.text = "Musik : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
