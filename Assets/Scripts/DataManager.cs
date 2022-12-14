using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string playerName = "";
    public List<HighscoreEntry> highscores = new List<HighscoreEntry>();
    public float paddleSpeed = 2.0f;

    private const int MAX_N_HIGHSCORES = 10;

    

    public static bool HasInstance => (instance != null);

    private void Awake()
    {
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
        LoadHighscoreData();
    }

    public void AddNewScoreEntry(int score)
    {
        HighscoreEntry scoreEntry = new HighscoreEntry(playerName, score);
        int i;
        for(i = highscores.Count - 1; i>=0; i--) //Decrementing (more efficient as we will have less chance to beat a highscore, than the opposite)
        {
            if (scoreEntry.Score < highscores[i].Score) //if current score is smaller
            {
                if (i + 1 >= MAX_N_HIGHSCORES) //if outside of highscores range
                    return;
                else //if in highscores range, then insert score
                {
                    break;
                }
            }

        }
        
        highscores.Insert(i + 1, scoreEntry);

        //making sure the number of scores is no more than the maximum number defined
        while (highscores.Count > MAX_N_HIGHSCORES)
        {
            highscores.RemoveAt(highscores.Count - 1);
        }

    }



    public struct HighscoreEntry
    {
        public HighscoreEntry(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }

        public string PlayerName { get; }
        public int Score { get; }
    };

    public void SaveHighscoreData()
    {
        HighScoresDataSave dataSave = new HighScoresDataSave();
        dataSave.highscoreNames = new List<string>();
        dataSave.highscoreScores = new List<int>();

        foreach(HighscoreEntry hs in highscores)
        {
            dataSave.highscoreNames.Add(hs.PlayerName);
            dataSave.highscoreScores.Add(hs.Score);
        }

        string json = JsonUtility.ToJson(dataSave);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }


    public void LoadHighscoreData()
    {
        HighScoresDataSave dataSave = new HighScoresDataSave();
        if (!File.Exists(Application.persistentDataPath + "/highscores.json"))
        {
            highscores.Clear();
            return;
        }

        string json = File.ReadAllText(Application.persistentDataPath + "/highscores.json");
        //Debug.Log(Application.persistentDataPath + "/highscores.json");
        //Debug.Log(json);

        dataSave = JsonUtility.FromJson<HighScoresDataSave>(json);

        highscores.Clear();
        if (dataSave.highscoreNames.Count != dataSave.highscoreScores.Count)
        {
            Debug.Log("Couldn't Load Data");
            return;
        }

        for (int i = 0; i<dataSave.highscoreNames.Count && i < MAX_N_HIGHSCORES; i++)
        {
            HighscoreEntry highscoreEntry = new HighscoreEntry(dataSave.highscoreNames[i], dataSave.highscoreScores[i]);
            highscores.Add(highscoreEntry);
        }
    }

    [System.Serializable]
    public class HighScoresDataSave
    {
        public List<string> highscoreNames;
        public List<int> highscoreScores;
    }

    [System.Serializable]
    public class SettingsDataSave
    {
        public int paddleSpeed;
    }

    public void LoadSettingsData()
    {
        if (!File.Exists(Application.persistentDataPath + "/settings.json"))
            return;

        string json = File.ReadAllText(Application.persistentDataPath + "/settings.json");
        SettingsDataSave settings = JsonUtility.FromJson<SettingsDataSave>(json);
        paddleSpeed = settings.paddleSpeed;
    }

    public void SaveSettingsData()
    {
        SettingsDataSave settings = new SettingsDataSave();
        settings.paddleSpeed = (int)paddleSpeed;

        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }
}
