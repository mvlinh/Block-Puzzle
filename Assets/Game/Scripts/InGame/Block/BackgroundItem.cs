using Gemmob;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundItem : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject Light;
    private VBox[,] Box;
    private GameObject[,] ArrObjectLight = new GameObject[8,8];

    private void Start()
    {
        Box = BoardController.Instance.Board;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ArrObjectLight[i, j] = Light.Spawn(Box[i, j].transform, Box[i, j].transform.position);
                ArrObjectLight[i, j].SetActive(false);
            }
        }
    }
    private void Update()
    {
        SetDark(Box);
        SetRayCastPoint(InputController.Instance.Block);
        TurnOffLight();
        if (!InputController.Instance.IsExist(InputController.Instance.Block) && InputController.Instance.Block != null)
        {
            TurnOnLight(BoardController.Instance.getRowColumnLight(InputController.Instance.Block));
        }
        else
        {
            BoardController.Instance.NoHitRayCastSetOffLight(InputController.Instance.Block);
        }
        if (BoardController.Instance.hasSugges)
        {
            OnSuggesLight(BoardController.Instance.HasSuges(SpawnController.Instance.ListBlock));
        }
    }
    public void SetRayCastPoint(Block Block)
    {
        if (IsActive())
        {
            var firePoint = Block.rayCastItem;
            List<Vector2> points = Block.points;
            RaycastHit2D bodyHit = Physics2D.Raycast(firePoint.transform.position, Vector2.zero, .1f, layerMask);
            int x = bodyHit.transform.GetComponent<VBox>().x;
            int y = bodyHit.transform.GetComponent<VBox>().y;
            Box[x, y].SetLight(true);
            foreach (var item in points)
            {
                if (!BoardController.Instance.IsOnBoard(x , y , item)) return;
                    Box[(x + (int)item.y), (y + (int)item.x)].SetLight(true);
            }
        }
    }
    public void SetDark(VBox[,] x)
    {
        foreach (var item in x)
        {
            item.SetLight(false);
        }
    }
  
    public bool IsActive()
    {
        return InputController.Initialized && (InputController.Instance.Block != null ? !InputController.Instance.IsExist(InputController.Instance.Block) : false);
    }
    public void TurnOnLight((List<int> row, List<int> column) a)
    {
        for (int i = 0; i < a.row.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ArrObjectLight[a.row[i], j].SetActive(true);
            }
        }
        for (int i = 0; i < a.column.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ArrObjectLight[j, a.column[i]].SetActive(true);
            }
        }
    }
    public void TurnOffLight()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ArrObjectLight[i, j].SetActive(false);
            }
        }
       
    }
    public void OnSuggesLight((VBox vBox, List<Vector2> listPoint) a)
    {
        
        if(a.vBox != null)
        {
            int x = a.vBox.x;
            int y = a.vBox.y;
            ArrObjectLight[x,y].SetActive(true);
            foreach (var item in a.listPoint)
            {
                ArrObjectLight[x + (int)item.y, y + (int)item.x].SetActive(true);
            }
        }
    }
}
