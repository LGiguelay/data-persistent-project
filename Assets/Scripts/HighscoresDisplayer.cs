using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoresDisplayer : MonoBehaviour
{
    public GameObject highScoreEntryTemplate;
    private List<GameObject> entriesHolder = new List<GameObject>(); 

    private void Start()
    {
        DisplayHighscores();
    }

    public void DisplayHighscores()
    {
        //while(entriesHolder.Count > 0) //Removing all current UI score entries
        //{
        //    int lastIndex = entriesHolder.Count - 1;
        //    Destroy(entriesHolder[lastIndex]);
        //    entriesHolder.RemoveAt(lastIndex);
        //}

        if (!DataManager.HasInstance)
            return;

        int place = 1;
        foreach(DataManager.HighscoreEntry hse in DataManager.instance.highscores)
        {
            GameObject entry = Instantiate(highScoreEntryTemplate, gameObject.transform);
            entriesHolder.Add(entry);


            HighscoreEntryDisplayer hseDisplayer = entry.GetComponent<HighscoreEntryDisplayer>();
            hseDisplayer.setPlaceDisplay(place);
            hseDisplayer.setNameDisplay(hse.PlayerName);
            hseDisplayer.setScoreDisplay(hse.Score);

            place++;
        }
    }
}
