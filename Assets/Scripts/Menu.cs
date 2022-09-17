using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Button startButton;

    private void Awake()
    {
        if(DataManager.HasInstance)
        {
            nameInput.text = DataManager.instance.playerName;
        }
        NameChanged();
    }

    public void NameChanged()
    {
        if(nameInput.text.Length == 0)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void StartGame()
    {
        if(!DataManager.HasInstance)
        {
            return;
        }

        DataManager.instance.playerName = nameInput.text;

        SceneManager.LoadScene("main");
    }

    public void DisplayHighscores()
    {
        if(DataManager.HasInstance)
            DataManager.instance.playerName = nameInput.text;

        SceneManager.LoadScene("Highscoreboard");
    }

    public void LoadSettings()
    {
        if (DataManager.HasInstance)
            DataManager.instance.playerName = nameInput.text;

        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
