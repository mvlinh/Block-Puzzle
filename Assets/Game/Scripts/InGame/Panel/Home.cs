using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Home : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] GameObject homePanel;
    [SerializeField] Button btnPlay;
    void Start()
    {
        btnPlay.onClick.AddListener(Play);
    }

    public void Play()
    {
        rect.DOAnchorPos(new Vector2(0, 2500), 0.25f);
        DOVirtual.DelayedCall(1f,() => homePanel.SetActive(false));
    }
}
