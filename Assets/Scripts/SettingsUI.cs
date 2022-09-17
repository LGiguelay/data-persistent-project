using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public TMPro.TMP_Text paddleSpeedValueDisplay;
    public Scrollbar paddleSpeedScrollBar;

    private float minSpeed = 1.0f;
    private float maxSpeed = 10.0f;
    private float speed = 2.0f;

    private void Start()
    {
        paddleSpeedScrollBar.onValueChanged.AddListener((float val) => OnPaddleSpeedChange(val));

        InitPaddleSpeedScrollbar();
    }

    public void InitPaddleSpeedScrollbar()
    {
        if (!DataManager.HasInstance)
            return;

        DataManager.instance.LoadSettingsData();
        float speed = DataManager.instance.paddleSpeed;

        float scrollbarPos = Mathf.InverseLerp(minSpeed, maxSpeed, speed);

        paddleSpeedScrollBar.value = scrollbarPos;
        OnPaddleSpeedChange(scrollbarPos);
    }

    public void OnPaddleSpeedChange(float scrollbarValue)
    {
        speed = Mathf.Lerp(minSpeed, maxSpeed, scrollbarValue);
        speed = Mathf.RoundToInt(speed);
        paddleSpeedValueDisplay.text = speed.ToString();

        if (!DataManager.HasInstance)
            return;

        DataManager.instance.paddleSpeed = speed;
        DataManager.instance.SaveSettingsData();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
