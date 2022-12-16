using UnityEngine;
using Gemmob;
public class InputController : SingletonBind<InputController>
{
    [SerializeField] LayerMask layerMask;
    public RaycastHit2D[] ArrVBox;
    public Vector3 move;
    public Block Block;
    private Vector3 startPos;
    private VBox[,] Box;
    private RaycastHit2D hit;
    private void Start()
    {
        Box = BoardController.Instance.Board;
        
    }
    private void Update()
    {
        ClickedBlock();
        MoveBlock();
        DropBox();
    }
    private void DropBox()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Block != null)
            {
                if (!IsExist(Block))
                {
                    ChangePos();
                }
                else
                    ComeBack();
            }
            Block = null;
        }

    }
    private void ChangePos()
    {
        RaycastHit2D bodyHit = Physics2D.Raycast((Block.rayCastItem.transform.position), Vector2.zero, 0f, layerMask);
        Vector2 root = BoardController.Instance.GetIndexBoard(bodyHit);
        int x = (int)root.x;
        int y = (int)root.y;
        Block.rayCastItem.transform.position = Box[x,y].transform.position;
        Box[x, y].SetOwn(true);
        Box[x, y].SetBlockItem(Block.rayCastItem.transform.GetComponent<BlockItem>());
        Transform childBlock;
        Block.GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < Block.transform.childCount; i++)
        {
            Block.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = -1;

        }
        for (int i = 0; i <Block.transform.childCount - 1; i++)
        {
            x = (int)(root.x + Block.points[i].y);
            y = (int)(root.y + Block.points[i].x);
            childBlock = Block.rayCastItem.transform.parent.GetChild(i+1);
            childBlock.position = Box[x,y].transform.position;
            Box[x,y].SetBlockItem(childBlock.GetComponent<BlockItem>());
            Box[x,y].SetOwn(true);
        }
        BoardController.Instance.CaculatorScore(Block.Type);
        SpawnController.Instance.ListBlock.Remove(Block);

        if (BoardController.Instance.HasSuges(SpawnController.Instance.ListBlock) != (null,null))
        {
            BoardController.Instance.hasSugges = true;
        }
        else
            BoardController.Instance.hasSugges = false;


        if (!(BoardController.Instance.IsEndGame(SpawnController.Instance.ListBlock) || SpawnController.Instance.ListBlock.Count == 0))
        {
            GamePlayUI.Instance.OpenGameOverPanel();
        }
        BoardController.Instance.temp = null;
    }

    private void MoveBlock()
    {
        if (Block != null)
        {
            move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            move.z = 0;
            Block.transform.position = move + 2 * Vector3.up;
        }
    }
    public bool IsExist(Block Block)
    {
        if (Block == null) return false;

        RaycastHit2D bodyHit = Physics2D.Raycast(Block.rayCastItem.transform.position, Vector2.zero, 0f, layerMask);
        if (!bodyHit || bodyHit.collider.GetComponent<VBox>() != null && bodyHit.collider.GetComponent<VBox>().IsOwn())
            return true;
        else if (bodyHit.collider.CompareTag(GameTag.VirtualBox))
        {
            return BoardController.Instance.CheckConditionVBox(bodyHit.transform.GetComponent<VBox>(), Block.points);
        }
        return false;
    }

    private void ComeBack()
    {
        if (Block != null)
        {
            move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            move.z = 0;
            Block.transform.position = startPos;
            Block.transform.localScale = Vector3.one * .5f;
        }
    }

    private bool IsBlock(RaycastHit2D hit)
    {
        return hit && hit.collider.transform.CompareTag(GameTag.Block);
    }

    private void ClickedBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.ClickPlaySounds();
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit && IsBlock(hit))
            {
                Block = hit.collider.gameObject.GetComponent<Block>();
                startPos = Block.transform.position;
                Block.transform.localScale *= 2f;

            }
        }
    }
}
