using DG.Tweening;
using DG.Tweening.Core;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] RectTransform rect;
    [SerializeField] Image img;
    [SerializeField] Button resume;
    [SerializeField] Button reload;
    [SerializeField] Button muted;

    private void OnEnable()
    {
        OpenPausePanel();
    }
    private void OnDisable()
    {
        reload.transform.localEulerAngles = Vector3.zero;
    }
    private void Start()
    {
        resume.onClick.AddListener(Close);
        reload.onClick.AddListener(Reload);
        muted.onClick.AddListener(Muted);
    }
    public void OpenPausePanel()
    {
        img.DOFade(.5f, .5f);
        DOVirtual.Vector3(rect.sizeDelta, new Vector2(rect.sizeDelta.x, 500f), .25f, OnChange).SetUpdate(true);
    }
    public void OnChange(Vector3 value)
    {
        rect.sizeDelta = value;

    }
    private void Close()
    {
        StartCoroutine(ClosePausePanel());
    }
    public void Reload()
    {
        StartCoroutine(ReloadGamePlay());
    }
    private void Muted()
    {
        SoundManager.Instance.OnButtonPress();
    }
    IEnumerator ClosePausePanel()
    {
        DOVirtual.Vector3(rect.sizeDelta, new Vector2(rect.sizeDelta.x, 200f), .25f, OnChange).SetUpdate(true);
        img.DOFade(0f, .5f);
        yield return new WaitForSeconds(.1f);
        panel.SetActive(false);
    }
    IEnumerator ReloadGamePlay()
    {
        reload.transform.DORotate(new Vector3(0, 0, 135), .25f, RotateMode.Fast);
        yield return new WaitForSeconds(.2f);
        ScoreController.Instance.DestroyScore();
        BoardController.Instance.ReloadBoard();
        for (int i = 0; i < SpawnController.Instance.ListBlock.Count; i++)
        {
            Destroy(SpawnController.Instance.ListBlock[i].gameObject);
        }
        SpawnController.Instance.ListBlock.Clear();
        Close();
        img.DOFade(0f, .5f);

    }
}
