using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoresDisplayer : MonoBehaviour
{
    public GameObject highScoreEntryTemplate;

    private void Start()
    {
        DisplayHighscores();
    }

    public void DisplayHighscores()
    {
        if (!DataManager.HasInstance)
            return;

        int place = 1;
        foreach(DataManager.HighscoreEntry hse in DataManager.instance.highscores)
        {
            HighscoreEntryDisplayer hseDisplayer = Instantiate(highScoreEntryTemplate,gameObject.transform).GetComponent<HighscoreEntryDisplayer>();
            hseDisplayer.setPlaceDisplay(place);
            hseDisplayer.setNameDisplay(hse.PlayerName);
            hseDisplayer.setScoreDisplay(hse.Score);

            place++;
        }
    }
}
