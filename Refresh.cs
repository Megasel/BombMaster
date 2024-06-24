using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Refresh : MonoBehaviour
{
    float saveTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(refreshModel());
        
    }

    // Update is called once per frame
    IEnumerator refreshModel()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.voxelChunkManager.RefreshProgressData();
        yield return null;
        GameManager.instance.cubesCounterText.text = GameManager.instance.voxelChunkManager.voxelData.destroyedChunksIds.Count.ToString() + "/" + GameManager.instance.cubesCount[YandexGame.savesData.Level].ToString();

    }
    private void Update()
    {
        saveTime -= Time.deltaTime;
        if (saveTime < 0 && !GameManager.instance.endLevel)
        {
            YandexGame.savesData.ProgressData = JsonUtility.ToJson(GameManager.instance.voxelChunkManager.voxelData);
            YandexGame.SaveProgress();
            saveTime = 5;
            Debug.Log("Data Saved: " + YandexGame.savesData.ProgressData);
        }
        
    }
}
