using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreUI : MonoBehaviour
{
    public HighscoresDisplayer hsDisplayer;

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void ResetHighscore()
    {
        if (!DataManager.HasInstance)
            return;

        Debug.Log("ResetHighscore with instance");
        Debug.Log("BEFORE: highscores = " + DataManager.instance.highscores.Count);


        DataManager.instance.highscores.Clear();
        DataManager.instance.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloading scene
        Debug.Log("AFTER: highscores = " + DataManager.instance.highscores.Count);

    }
}
