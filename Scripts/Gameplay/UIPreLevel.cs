using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;
public class UIPreLevel : MonoBehaviour
{
    [SerializeField] private TextMeshPro _ammoText;

    private void Start()
    {
        if(YandexGame.SDKEnabled)
        {
            if (YandexGame.savesData.FirstTime == 0)
            {
                YandexGame.savesData.FirstTime = 1;//PlayerPrefs.SetInt("FirstTime", 1);
                YandexGame.savesData.Level = 0;//PlayerPrefs.SetInt("Level", 1);
                YandexGame.savesData.prevLevel = -1;//PlayerPrefs.SetInt("prevLevel", -1);
                YandexGame.SaveProgress();
            }

            int levelScene;
            if (YandexGame.savesData.prevLevel == -1)
            {
                if (YandexGame.savesData.Level <= GameManager.instance.levels.Count) levelScene = GameManager.instance.levels[YandexGame.savesData.Level - 1];
                else levelScene = GameManager.instance.levels[Random.Range(0, GameManager.instance.levels.Count)];

                YandexGame.savesData.prevLevel = levelScene;
                YandexGame.SaveProgress();
            }
        }
        
    }

    public void Launch()
    {
        YandexGame.FullscreenShow();
        GameManager.instance.LoadShootingLevel();
    }


}
