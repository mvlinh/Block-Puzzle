using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : SingletonBind<BoardController>
{
    [SerializeField] Transform body;
    [SerializeField] Text score;
    [SerializeField] LayerMask layerMask;
    public int count;
    public VBox[,] Board = new VBox[8,8];
    private Stack row = new Stack();
    private Stack column = new Stack();
    public VBox temp;
    public bool hasSugges;
    
    //[SerializeField] GameObject[] cells;
    //[SerializeField] int x = 8;

    //[ContextMenu("Convert")]
    //private void Convert()
    //{
    //    for (int i = 0; i < cells.Length; i++)
    //    {
    //        cells[i].name = $"B_{i/x}{i%x}";
    //    }
    //}
    private void Start()
    {
        int index = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Board[x, y] = body.GetChild(index).GetComponent<VBox>();
                Board[x, y].x = x;
                Board[x, y].y = y;
                index++;
            }
        }
    }
  
    public Vector2 GetIndexBoard(RaycastHit2D hit)
    {
        return new Vector2(hit.transform.GetComponent<VBox>().x, hit.transform.GetComponent<VBox>().y);
    }

    public bool CheckConditionVBox(VBox hit, List<Vector2>  points)
    {
        int x = hit.x;
        int y = hit.y;
        if (Board[x, y].IsOwn()) return true;
        foreach (var item in points)
        {
            if (!IsOnBoard( x,  y, item))
                return true;
            else if ((Board[(x + (int)item.y), (y + (int)item.x)].IsOwn()))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsOnBoard(int x, int y,Vector2 item)
    {
        return !((x + (int)item.y) > 7 || (x + (int)item.y) < 0 || (y + (int)item.x) > 7 || (y + (int)item.x) < 0);
    }
    public void CaculatorScore(int blockIndex)
    {
        for (int i = 0; i < 8; i++)
        {
            if (RowScore(i))
            {
                row.Push(i);
            }
            if (ColumnScore(i))
            {
                column.Push(i);
            }
        }

        if ((row.Count + column.Count) != 0)
            ScoreController.Instance.UpdateScore(blockIndex, (row.Count + column.Count));

        while (row != null && row.Count != 0)
        {
            int temp = (int)row.Pop();
            for (int i = 0; i < 8; i++)
            {
                Board[temp, i].GetBlockItem().DestroyBlock();
                Board[temp, i].SetOwn(false);
            }
        }
        while (column != null && column.Count != 0)
        {
            int temp = (int)column.Pop();
            for (int i = 0; i < 8; i++)
            {
                Board[i,temp].GetBlockItem().DestroyBlock();
                Board[ i, temp].SetOwn(false);
            }
        }
       
    }
    public bool RowScore(int x)
    {
        for (int i = 0; i < 8; i++)
        {
            if (!Board[x, i].IsOwn())
            {
                return false;
            }
        }
        return true;
    }

    public bool ColumnScore(int x)
    {
        for (int i = 0; i < 8; i++)
        {
                if (!Board[i,x].IsOwn())
                {
                    return false;
                }
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //if (Board[i, j].IsOwn())
                //Gizmos.color = Color.yellow;
                //else
                //Gizmos.color = Color.blue;

                if (Board[i, j] == null) return;

                    if (Board[i, j].GetTurnOn()) 
                    Gizmos.DrawIcon(Board[i, j].transform.position, "icon1", true);
                else
                    Gizmos.DrawIcon(Board[i, j].transform.position, "icon2", true);
            }
        }
       
    }
    public bool IsEndGame(List<Block> T)
    {
        for (int i = 0; i < T.Count; i++)
        {
            if (CheckGameObjectCondition(T[i]))
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckGameObjectCondition(Block T)
    {
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {
                if (!Board[i, j].IsOwn())
                {
                    if(!CheckConditionVBox(Board[i, j], T.points))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void ReloadBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (Board[i, j].IsOwn())
                {
                    Board[i, j].GetBlockItem().DestroyBlock();
                    Board[i, j].SetOwn(false);
                }
            }
        }
    }

    List<int> a = new List<int>(), v = new List<int>();
    public (List<int>, List<int>) getRowColumnLight(Block b)
   {
        RaycastHit2D hit = Physics2D.Raycast(b.rayCastItem.transform.position, Vector2.zero, 0f, layerMask);
            if ( hit && temp == null)
            {
                temp = hit.transform.GetComponent<VBox>();
            }
            if (b != null && hit.transform.GetComponent<VBox>())
            {

                if (temp == hit.transform.GetComponent<VBox>())
                {
                    Board[temp.x, temp.y].SetTurnOn(true);
                    foreach (var item in b.points)
                    {
                        if (BoardController.Instance.IsOnBoard(temp.x, temp.y, item))
                            Board[temp.x + (int)item.y, temp.y + (int)item.x].SetTurnOn(true);
                    }
                }
                else
                {
                    NoHitRayCastSetOffLight(b);
                    temp = hit.transform.GetComponent<VBox>();
                }
            }
        a.Clear();
        v.Clear();
        for (int i = 0; i < 8; i++)
        {
            if (LightRowScore(i))
            {
                a.Add(i);
            }
            if (LightColumnScore(i))
            {
                v.Add(i);
            }
        }

        return (a, v);
   }
    public void NoHitRayCastSetOffLight(Block b)
    {
        if(temp != null && b != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(b.rayCastItem.transform.position, Vector2.zero, 0f, layerMask);
            if (hit)
            {
            Board[temp.x, temp.y].SetTurnOn(false);
            foreach (var item in b.points)
            {
                if (BoardController.Instance.IsOnBoard(temp.x, temp.y, item))
                    Board[temp.x + (int)item.y, temp.y + (int)item.x].SetTurnOn(false);
            }
            temp = hit.transform.GetComponent<VBox>();
            temp = null;
            }
        }
    }
    public bool LightRowScore(int x)
    {
        for (int i = 0; i < 8; i++)
        {
            if (!Board[x, i].GetTurnOn())
            {
                return false;
            }
        }
        return true;
    }

    public bool LightColumnScore(int x)
    {
        for (int i = 0; i < 8; i++)
        {
            if (!Board[i, x].GetTurnOn())
            {
                return false;
            }
        }
        return true;
    }

    VBox V_sugges;
    List<Vector2> L_sugges;
    public int Suggestion(Block T)
    {
        int temp = 0;
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {
                if (!Board[i, j].IsOwn())
                {
                    if (!CheckConditionVBox(Board[i, j], T.points))
                    {
                        temp++;
                        V_sugges = Board[i, j];
                        L_sugges = T.points;
                    }
                }
            }
        }
        return temp;
    }
    int Count;
    public (VBox, List<Vector2>) HasSuges(List<Block> T)
    {
        count = 0;
        for (int i = 0; i < T.Count; i++)
        {
            count += Suggestion(T[i]);
        }
        if (count == 1)
        {
            return (V_sugges, L_sugges);
        }
        else
            return (null,null);
    }
}
