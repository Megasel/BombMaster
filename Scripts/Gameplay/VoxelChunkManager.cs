using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class VoxelChunkManager : MonoBehaviour
{
    [SerializeField]
    public VoxelData voxelData = new VoxelData();
    public List<Chunk> chunksList = new List<Chunk>();

    private void Awake()
    {
        GameManager.instance.voxelChunkManager = this;
    }

    private void Start()
    {
        Configue();

    }

    //private void Update()
    //{
    //    //now the progress save is with the press key "S"
    //    //next you will make the call of the "SaveProgressData" at the end of level
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        StartCoroutine(SaveProgressData());
    //    }
    //}

    private void Configue()
    {
        int counterChunks = 0;
        var childs = GetComponentsInChildren<Transform>();

        foreach (Transform item in childs)
        {
            if (item.childCount>0) //is parent
            {
                //skip
                continue;
            }

            Chunk chunk = item.gameObject.AddComponent<Chunk>();
            chunk.Setup(counterChunks++,this);

            chunksList.Add(chunk);
            
            //Debug.Log(item.gameObject.name + " // " + ++counterChunks);
        }

    }

    public void RefreshProgressData()
    {
        voxelData = JsonUtility.FromJson<VoxelData>(YandexGame.savesData.ProgressData);//PlayerPrefs.GetString("ProgressData"));

        if (voxelData==null || voxelData.destroyedChunksIds == null || voxelData.destroyedChunksIds.Count==0)
        {
            voxelData = new VoxelData();
            return;
        }

        List<Chunk> tempList = new List<Chunk>();
        tempList.AddRange(chunksList);

        foreach (Chunk chunk in tempList)
        {
            if (voxelData.destroyedChunksIds.Contains(chunk.id))
            {
                chunk.AlreadyDamaged();
            }
        }

        Debug.Log("Data Refreshed: " + YandexGame.savesData.ProgressData);
    }
    public void ResetProgressData()
    {
        YandexGame.savesData.ProgressData = "";
        voxelData = JsonUtility.FromJson<VoxelData>(YandexGame.savesData.ProgressData);
        YandexGame.SaveProgress();
        Debug.Log("Data Reseted: " + YandexGame.savesData.ProgressData);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            RefreshProgressData();
        }
        
    }
    public void AddChunkToDamagedList(Chunk chunk,int id=-1)
    {
        if (id != -1) voxelData.destroyedChunksIds.Add(id);

        chunksList.Remove(chunk);
    }

    public void CheckIfLevelShouldEnds()
    {
        GameManager.instance.cubesCounterText.text = voxelData.destroyedChunksIds.Count.ToString() +"/"+ GameManager.instance.cubesCount[YandexGame.savesData.Level].ToString();
        StartCoroutine(waitToCheck());
        IEnumerator waitToCheck()
        {
            yield return new WaitForSeconds(.5f);

            if (chunksList.Count < 150)
            {
                List<Chunk> tempList = new List<Chunk>();
                tempList.AddRange(chunksList);
                foreach (Chunk item in tempList)
                {
                    item?.Explode();
                }
                if (!GameManager.instance.endLevel)
                {
                    StartCoroutine(GameManager.instance.LevelWon());
                }
                
            }
        }
        
    }
}

[System.Serializable]
public class VoxelData
{
    public List<int> destroyedChunksIds = new List<int>();
}
