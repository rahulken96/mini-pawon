using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PemainSFX : MonoBehaviour
{
    private PemainController pemain;
    private float langkahKakiTimer;
    private float langkahKakiTimerMax = .1f;

    private void Awake()
    {
        pemain = GetComponent<PemainController>();
    }

    private void Update()
    {
        langkahKakiTimer -= Time.deltaTime;

        if (langkahKakiTimer < 0f)
        {
            langkahKakiTimer = langkahKakiTimerMax;

            if (pemain.walkStatus())
            {
                SoundManager.Instance.PlayLangkahKakiSound(pemain.transform.position, 1.5f);
            }
        }
    }
}
