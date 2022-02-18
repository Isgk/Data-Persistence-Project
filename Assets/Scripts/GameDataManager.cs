using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public string playerNameIn;
    public static GameDataManager dataManager;
    public int bestScore;
    public string bestName;

    // Start is called before the first frame update
    private void Awake()
    {
        if (dataManager != null)
        {
            Destroy(gameObject);
            return;
        }
        dataManager = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayerNameInput(string p)
    {
        playerNameIn = p;
        Debug.Log(playerNameIn);
    }
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestName;
    }
    public void SaveBestScore()
    {
        SaveData scoreData = new SaveData();
        scoreData.bestScore = bestScore;
        scoreData.bestName = bestName;
        
        string scoreJson = JsonUtility.ToJson(scoreData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", scoreJson);
    }
    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("file exsist");
            string scoreJson = File.ReadAllText(path);
            SaveData scoreData = JsonUtility.FromJson<SaveData>(scoreJson);
            Debug.Log("bestScore" + scoreData.bestScore);
            bestScore = scoreData.bestScore;
            bestName = scoreData.bestName;
        }
    }

}
