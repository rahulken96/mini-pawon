using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image imageProgressBar;

    private IProgressBar hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IProgressBar>();

        if(hasProgress != null)
        {
            hasProgress.OnProgressStatus += HasProgress_OnProgressStatus; ;
        
            imageProgressBar.fillAmount = 0f;

            Hide();
        }
    }

    private void HasProgress_OnProgressStatus(object sender, IProgressBar.OnProgressEventArgs e)
    {
        imageProgressBar.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
