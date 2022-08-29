using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Board : MonoBehaviour
{
    public GameObject prefab;
    public BoardData boardData;
    public List<List<GameObject>> board;

    public int rows = 10;
    public int columns = 10;
    public int bombs = 20;

    private ClickActions ca;

    private void Awake()
    {
        ca = new ClickActions();
    }

    private void OnEnable()
    {
        ca.Player.Reset.performed += ResetBoard;
        ca.Player.Enable();
    }

    private void OnDisable()
    {
        ca.Player.Reset.performed -= ResetBoard;
    }

    private void ResetBoard(InputAction.CallbackContext obj)
    {
        DeleteBoard();
        CreateBoard();
    }

    // Start is called before the first frame update
    void Start()
    {
        boardData = new BoardData();
        CreateBoard();
    }

    void SetCameraHeight()
    {
        var camY = boardData.columns * 2f;
        if (Screen.width > Screen.height)
        {
            if (columns > rows)
            {
                camY = boardData.columns * 2f;
            }
            else
            {
                camY = boardData.rows * 2f;
            }
        }
        else
        {
            if (rows > columns)
            {
                camY = boardData.rows * 2f;
            }
            else
            {
                camY = boardData.columns * 2f;
            }
        }

        Camera.main.transform.SetPositionAndRotation(new Vector3(0, camY, 0), Camera.main.transform.rotation);
    }

    void DeleteBoard()
    {
        for (var x = boardData.spaces.Count - 1; x >= 0; x--)
        {
            for (var y = boardData.spaces[x].Count - 1; y >= 0; y--)
            {
                Destroy(board[x][y], 0f);
            }
        }
    }

    void CreateBoard()
    {
        boardData.rows = rows;
        boardData.columns = columns;
        boardData.bombCount = bombs;
        boardData.GenerateBoard();
        SetCameraHeight();

        board = new List<List<GameObject>>(boardData.rows);

        var topCorner = new Vector2(((boardData.rows / 2f) * 2) - 1, ((boardData.columns / 2f) * 2) - 1);

        for (var x = 0; x < boardData.spaces.Count; x++)
        {
            board.Add(new List<GameObject>(boardData.columns));
            board[x] = new List<GameObject>(boardData.columns);
            for (var y = 0; y < boardData.spaces[x].Count; y++)
            {
                var go = Instantiate(prefab, new Vector3((x * 2.0f) - topCorner.x, 0, (y * 2.0f) - topCorner.y), Quaternion.identity, transform);
                var space = go.GetComponent<Space>();
                space.data = boardData.spaces[x][y];
                board[x].Add(go);
            }
        }
        SetSpaceSiblings();
    }

    void SetSpaceSiblings()
    {
        for (var x = 0; x < boardData.spaces.Count; x++)
        {
            for (var y = 0; y < boardData.spaces[x].Count; y++)
            {
                var s = board[x][y].GetComponent<Space>();
                s.siblings = new List<Space>();
                if (s.data.type == BoardData.EMPTY)
                {
                    SetSiblingsForSpace(s);
                }
            }
        }
    }

    void SetSiblingsForSpace(Space s)
    {
        if (boardData.UGet(s.data.location.x - 1, s.data.location.y - 1) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x - 1][s.data.location.y - 1].GetComponent<Space>());
        }
        // middle left
        if (boardData.UGet(s.data.location.x - 1, s.data.location.y) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x - 1][s.data.location.y].GetComponent<Space>());
        }
        // bottom left
        if (boardData.UGet(s.data.location.x - 1, s.data.location.y + 1) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x - 1][s.data.location.y + 1].GetComponent<Space>());
        }

        // top middle
        if (boardData.UGet(s.data.location.x, s.data.location.y - 1) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x][s.data.location.y - 1].GetComponent<Space>());
        }
        // bottom middle
        if (boardData.UGet(s.data.location.x, s.data.location.y + 1) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x][s.data.location.y + 1].GetComponent<Space>());
        }

        // top right
        if (boardData.UGet(s.data.location.x + 1, s.data.location.y - 1) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x + 1][s.data.location.y - 1].GetComponent<Space>());
        }
        // middle right
        if (boardData.UGet(s.data.location.x + 1, s.data.location.y) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x + 1][s.data.location.y].GetComponent<Space>());
        }
        // bottom right
        if (boardData.UGet(s.data.location.x + 1, s.data.location.y + 1) >= BoardData.EMPTY)
        {
            s.siblings.Add(board[s.data.location.x + 1][s.data.location.y + 1].GetComponent<Space>());
        }
    }

    public void CheckFlags(GameObject g) {
        var s = g.GetComponent<Space>();
        boardData.FlagLocation(s.data.location);
        var win = boardData.IsBoardFinished();
        if (win) {
            Debug.Log("You win");
        }
    }
}
