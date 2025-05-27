using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI jumlahOrderanSelesaiText;


    private void Start()
    {
        GameManager.Instance.OnStateGame += GameManager_OnStateGame;

        Hide();
    }

    private void GameManager_OnStateGame(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();

            jumlahOrderanSelesaiText.text = DeliveryManager.Instance.GetJumlahOrderanYangDiselesaikan().ToString(); 
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
