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
    public List<List<SpaceData>> spaces;
    public List<Vector2Int> bombLocations;
    public List<Vector2Int> flaggedLocationsGood;
    public List<Vector2Int> flaggedLocationsBad;

    public void InitializeBoard()
    {
        spaces = new List<List<SpaceData>>(rows);
        for (var x = 0; x < rows; x++)
        {
            spaces.Add(new List<SpaceData>(columns));
            for (var y = 0; y < columns; y++)
            {
                var d = new SpaceData();
                d.location = new Vector2Int(x, y);
                d.type = EMPTY;
                spaces[x].Add(d);
            }
        }
        flaggedLocationsBad = new List<Vector2Int>();
        flaggedLocationsGood = new List<Vector2Int>();
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
            return spaces[x][y].type;
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
            spaces[bomb.x - 1][bomb.y - 1].type++;
        }
        // middle left
        if (UGet(bomb.x - 1, bomb.y) >= EMPTY)
        {
            spaces[bomb.x - 1][bomb.y].type++;
        }
        // bottom left
        if (UGet(bomb.x - 1, bomb.y + 1) >= EMPTY)
        {
            spaces[bomb.x - 1][bomb.y + 1].type++;
        }

        // top middle
        if (UGet(bomb.x, bomb.y - 1) >= EMPTY)
        {
            spaces[bomb.x][bomb.y - 1].type++;
        }
        // bottom middle
        if (UGet(bomb.x, bomb.y + 1) >= EMPTY)
        {
            spaces[bomb.x][bomb.y + 1].type++;
        }

        // top right
        if (UGet(bomb.x + 1, bomb.y - 1) >= EMPTY)
        {
            spaces[bomb.x + 1][bomb.y - 1].type++;
        }
        // middle right
        if (UGet(bomb.x + 1, bomb.y) >= EMPTY)
        {
            spaces[bomb.x + 1][bomb.y].type++;
        }
        // bottom right
        if (UGet(bomb.x + 1, bomb.y + 1) >= EMPTY)
        {
            spaces[bomb.x + 1][bomb.y + 1].type++;
        }
    }

    public void SetBombsAndNumbers()
    {
        foreach (var bomb in bombLocations)
        {
            spaces[bomb.x][bomb.y].type = BOMB;
            SetNumbersAroundBomb(bomb);
        }
    }

    public void GenerateBoard()
    {
        InitializeBoard();
        GenerateBombLocations();
        SetBombsAndNumbers();
    }

    public void FlagLocation(Vector2Int location) {
        var s = spaces[location.x][location.y];
        if (s.isFlagged && s.IsBomb()) {
            flaggedLocationsGood.Add(location);
        } else if(s.isFlagged) {
            flaggedLocationsBad.Add(location);
        } else if(!s.isFlagged && s.IsBomb()) {
            flaggedLocationsGood.Remove(location);
        } else if (!s.isFlagged) {
            flaggedLocationsBad.Remove(location);
        }
    }

    public bool IsBoardFinished() {
        if (flaggedLocationsBad.Count > 0) {
            return false;
        }
        if (flaggedLocationsGood.Count == bombCount){
            return true;
        }
        return false;
    }
}
