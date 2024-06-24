using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartScene : MonoBehaviour
{
    bool isStarted = false;
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то запускаем Ваш метод
            GetData();

            // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }
    void GetData()
    {

        if (!isStarted)
        {
            Debug.Log(YandexGame.savesData.Level);
            SceneManager.LoadScene(YandexGame.savesData.Level);
            isStarted = true;

        }

    }

}
