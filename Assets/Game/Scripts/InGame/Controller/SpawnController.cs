using UnityEngine;
using Gemmob;
using System.Collections.Generic;

public class SpawnController : SingletonBind<SpawnController>
{
    [SerializeField] Block[] Block;
    [SerializeField] Transform[] Box;
    public int CountBlockDrop;
    public List<Block> ListBlock;
    void Start()
    {
        SpawnObject();
    }
    void Update()
    {
        if(ListBlock.Count == 0)
        {
            SpawnObject();
        }
    }
    [ContextMenu("spawn")]
    public void SpawnObject()
    {
        for (int i = 0; i < Box.Length; i++)
        {
            var clone = Block[Random.Range(0, Block.Length)].Spawn(Box[i], Box[i].position);
            if (!IsDuplicate(ListBlock, clone))
            {
                clone.transform.localScale = Vector3.one * .5f;

                ListBlock.Add(clone);
            }
            else
            {
                Destroy(clone.gameObject);
                i--;
            }
        }
        if (!(BoardController.Instance.IsEndGame(ListBlock) || ListBlock.Count == 0))
        {
            GamePlayUI.Instance.OpenGameOverPanel();
        }
        if (BoardController.Instance.HasSuges(ListBlock) != (null, null))
        {
            BoardController.Instance.hasSugges = true;
        }
    }
    private bool IsDuplicate(List<Block> listBlock, Block block)
    {
        foreach (var item in listBlock)
        {
            if(block.Type == item.Type)
            {
                return true;
            }
        }
        return false;
    }

}
