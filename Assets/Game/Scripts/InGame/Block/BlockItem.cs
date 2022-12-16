using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetActive(bool active)
    {
        boxCollider.enabled = active;
    }
    public void DestroyBlock()
    {
        Destroy(gameObject);
    }
}
