using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBox : MonoBehaviour
{
    private SpriteRenderer icon;
    private bool turnOn;
    private bool isLight;
    private bool isOwned;
    private BlockItem block;
    public int x, y;

    private void Awake()
    {
        icon = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isLight)
        {
            icon.color = new Color(255,255,255,.5f);
        }
        else icon.color = new Color(255, 255, 255, 0);
    }
    public bool IsLight()
    {
        return isLight;
    }
    public void SetLight(bool light)
    {
        isLight = light;
    }
    public BlockItem GetBlockItem()
    {
        return this.block;
    }
    public void SetBlockItem(BlockItem b)
    {
        block = b;
    }
    public bool IsOwn()
    {
        return isOwned;
    }
    public void SetOwn(bool own)
    {
        isOwned = own;
        turnOn = own;
    }
    public void SetTurnOn(bool turnOn)
    {
        this.turnOn = turnOn;
    }
    public bool GetTurnOn()
    {
        return turnOn;
    }
}
