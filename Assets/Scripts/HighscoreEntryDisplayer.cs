using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreEntryDisplayer : MonoBehaviour
{
    public TMPro.TMP_Text placeText;
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text scoreText;


    public void setDisplay(int place, string name, int score)
    {
        setPlaceDisplay(place);
        setNameDisplay(name);
        setScoreDisplay(score);
    }

    public void setPlaceDisplay(int place)
    {
        placeText.text = $"{place}.";
    }

    public void setNameDisplay(string name)
    {
        nameText.text = name;
    }

    public void setScoreDisplay(int score)
    {
        scoreText.text = score.ToString();
    }
}
