using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;
public class UILevel : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    public GameObject startScreen;
    public GameObject loseScreen;
    public GameObject wonScreen;
    public RectTransform crossHair;

    private void Start()
    {
        GameManager.instance.uiLevel = this;

        //GameManager.instance.uiLevel.ammoText.text = GameManager.instance.ammoCount.ToString();
    }

    private void Update()
    {
        if (startScreen.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startScreen.SetActive(false);
            }
        }
    }

    public void Replay()
    {
        GameManager.instance.leaderboard.SetActive(false);
        GameManager.instance.voxelChunkManager.ResetProgressData();
        YandexGame.FullscreenShow();
        GameManager.instance.LoadShootingLevel();
    }
}
