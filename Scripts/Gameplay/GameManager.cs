using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int ammoCount = 9999;
    public AudioSource aud;
    public List<int> levels = new List<int>();
    public ParticleSystem confetti;
    public float explodeRadius = 3f;
    public int winLeftAmmount = 1;
    public Text cubesCounterText;
    public int[] cubesCount;
    public GameObject leaderboard;
    public bool isStarted = false;
    [Header("will be assigned automatically")]
    public VoxelChunkManager voxelChunkManager;
    public UILevel uiLevel;

    public bool endLevel = false;
    
    private void Awake()
    {
        cubesCount =  new int[16] { 0,933, 668, 2264, 2978, 1096, 2445, 865, 1110, 997, 4696, 1427, 1475, 1180, 1068, 2251 };
        Screen.SetResolution(1080, 1920, true);
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        isStarted = true;
    }
    public void LoadShootingLevel()
    {
        endLevel = false;
        SceneManager.LoadScene(YandexGame.savesData.prevLevel);
        // SceneManager.LoadScene(PlayerPrefs.GetInt("prevLevel"));
    }
    public void LevelEnds()
    {
        if (endLevel) return;
        endLevel = true;

        uiLevel.loseScreen.SetActive(true);
        //voxelChunkManager.SaveProgressData();

        Debug.Log("LevelEnds");
    }

    public IEnumerator LevelWon()
    {
        if (endLevel) yield return null;
        endLevel = true;
        Debug.Log("WON");
        confetti.Play();
        aud.Play();
        YandexGame.savesData.countBlocks += voxelChunkManager.voxelData.destroyedChunksIds.Count;
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("CountBlocks", YandexGame.savesData.countBlocks);
       
        yield return new WaitForSeconds(0.5f);
        leaderboard.SetActive(true);
        uiLevel.wonScreen.SetActive(true);
        voxelChunkManager.ResetProgressData();
        if(YandexGame.savesData.prevLevel >= 15 && YandexGame.savesData.Level >= 15)
        {
            YandexGame.savesData.prevLevel = 1;//PlayerPrefs.SetInt("prevLevel", -1);
            YandexGame.savesData.Level = 1;
        }
        else
        {
            YandexGame.savesData.prevLevel++;//PlayerPrefs.SetInt("prevLevel", -1);
            YandexGame.savesData.Level++;
        }
        //PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        YandexGame.SaveProgress();
        Debug.Log("LevelWon");
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(YandexGame.savesData.FirstTime + " - FirstTime");
            Debug.Log(YandexGame.savesData.Level + " - Level");
            Debug.Log(YandexGame.savesData.ProgressData + " - ProgressData");
            Debug.Log(YandexGame.savesData.prevLevel + " - prevLevel");
            Debug.Log(YandexGame.savesData.countBlocks + " - countBlocks");
        }
        if (Input.GetKeyDown(KeyCode.O))
            SceneManager.LoadScene(YandexGame.savesData.prevLevel);
    }
}
