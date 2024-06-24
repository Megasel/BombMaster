using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ResetSaves : MonoBehaviour
{
    public void ResetSave()
    {
        Debug.Log("YANDEX SAVES RESET");
        PlayerPrefs.DeleteAll();
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

    }
}
