using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public const int EMPTY = 0;
    public const int BOMB = -1;

    public int rows;
    public int columns;
    public int bombCount;
    public List<List<int>> spaces;
    public List<Vector2Int> bombLocations;

    public void InitializeBoard()
    {
        spaces = new List<List<int>>(rows);
        for (var x = 0; x < rows; x++)
        {
            spaces.Add(new List<int>(columns));
            for (var y = 0; y < columns; y++)
            {
                spaces[x].Add(EMPTY);
            }
        }
    }

    public Vector2Int GenerateBombLocation()
    {
        return new Vector2Int(UnityEngine.Random.Range(0, rows - 1), UnityEngine.Random.Range(0, columns - 1));
    }

    public void GenerateBombLocations()
    {
        bombLocations = new List<Vector2Int>(bombCount);
        for (var i = 0; i < bombCount; i++)
        {
            var l = GenerateBombLocation();
            while (bombLocations.Contains(l))
            {
                l = GenerateBombLocation();
            }
            bombLocations.Add(l);
        }
    }

    // Unsafe get only used for SetNumbersAroundBomb to make logic easier to read
    public int UGet(int x, int y)
    {
        try
        {
            return spaces[x][y];
        }
        catch (ArgumentOutOfRangeException) { }
        return -1;
    }

    public void SetNumbersAroundBomb(Vector2Int bomb)
    {
        // set all spaces around the bomb and catch array index out of bounds errors instead of adding extra logic
        // top left
        if (UGet(bomb.x - 1, bomb.y - 1) >= EMPTY)
        {
            spaces[bomb.x - 1][bomb.y - 1]++;
        }
        // middle left
        if (UGet(bomb.x - 1, bomb.y) >= EMPTY)
        {
            spaces[bomb.x - 1][bomb.y]++;
        }
        // bottom left
        if (UGet(bomb.x - 1, bomb.y + 1) >= EMPTY)
        {
            spaces[bomb.x - 1][bomb.y + 1]++;
        }

        // top middle
        if (UGet(bomb.x, bomb.y - 1) >= EMPTY)
        {
            spaces[bomb.x][bomb.y - 1]++;
        }
        // bottom middle
        if (UGet(bomb.x, bomb.y + 1) >= EMPTY)
        {
            spaces[bomb.x][bomb.y + 1]++;
        }

        // top right
        if (UGet(bomb.x + 1, bomb.y - 1) >= EMPTY)
        {
            spaces[bomb.x + 1][bomb.y - 1]++;
        }
        // middle right
        if (UGet(bomb.x + 1, bomb.y) >= EMPTY)
        {
            spaces[bomb.x + 1][bomb.y]++;
        }
        // bottom right
        if (UGet(bomb.x + 1, bomb.y + 1) >= EMPTY)
        {
            spaces[bomb.x + 1][bomb.y + 1]++;
        }
    }

    public void SetBombsAndNumbers()
    {
        foreach (var bomb in bombLocations)
        {
            spaces[bomb.x][bomb.y] = BOMB;
            SetNumbersAroundBomb(bomb);
        }
    }

    public void GenerateBoard()
    {
        InitializeBoard();
        GenerateBombLocations();
        SetBombsAndNumbers();
    }
}
