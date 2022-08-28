using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehavior : MonoBehaviour
{
    public GameObject prefab;
    public Board boardData;
    public List<List<GameObject>> board;

    public int rows = 10;
    public int columns = 10;
    public int bombs = 20;

    // Start is called before the first frame update
    void Start()
    {
        boardData = new Board();
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateBoard()
    {
        boardData.rows = rows;
        boardData.columns = columns;
        boardData.bombs = bombs;
        boardData.GenerateBoard();
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
                space.p = new Vector2Int(x, y);
                space.spaceType = boardData.spaces[x][y];
                board[x].Add(go);
            }
        }
    }
}
