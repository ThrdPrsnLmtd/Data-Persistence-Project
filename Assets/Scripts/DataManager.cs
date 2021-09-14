using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string PlayerName = "";

    public string HighScore_Name = "No One";

    public int HighScore_Score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string HighScore_Name;

        public int HighScore_Score;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScore_Name = HighScore_Name;
        data.HighScore_Score = HighScore_Score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore_Name = data.HighScore_Name;
            HighScore_Score = data.HighScore_Score;
        }
    }
}
