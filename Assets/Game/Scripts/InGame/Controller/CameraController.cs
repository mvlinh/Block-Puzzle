using Gemmob;
using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    float s9_22 = 0.41f;
    float s9_21 = 0.43f;
    float s9_20 = 0.451f;
    float s9_19 = 0.48f;
    float s9_18 = 0.51f;
    float s9_16 = 0.57f;
    void Start()
    {
        //Output the current screen window width in the console
        SetCameraOrthorgraphicSize((float)Screen.width / (float)Screen.height); 
        EventDispatcher.Instance.AddListener(EventKey.ScoreChanged, OnScore);
    }

    private void OnScore()
    {
        Debug.Log("Camare");
    }
    private void SetCameraOrthorgraphicSize(float size)
    {
        if (size < s9_22)
        {
            Camera.main.orthographicSize = 11.5f;
        }
        else
        if (size < s9_21)
        {

            Camera.main.orthographicSize = 11;
        }
        else
        if (size < s9_20)
        {
            Camera.main.orthographicSize = 10.5f;

        }
        else
        if (size < s9_19)
        {
            Camera.main.orthographicSize = 10f;
        }
        else
        if (size < s9_18)
        {
            Camera.main.orthographicSize = 9.5f;
        }
        else
        {
            Camera.main.orthographicSize = 8.5f;
        }
    }
}