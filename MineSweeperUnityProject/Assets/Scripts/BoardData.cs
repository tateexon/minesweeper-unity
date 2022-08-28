using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public static int EMPTY = 0;
    public static int BOMB = -1;

    public int rows;
    public int columns;
    public int bombs;
    public List<List<int>> spaces;

    public void InitializeBoard() {
        spaces = new List<List<int>>(rows);
        for(var x = 0; x < rows; x++) {
            spaces.Add(new List<int>(columns));
            for (var y = 0; y < columns; y++) {
                spaces[x].Add(EMPTY);
            }
        }
    }

    public Vector2Int GenerateBombLocation() {
        return new Vector2Int(UnityEngine.Random.Range(0, rows-1), UnityEngine.Random.Range(0, columns-1));
    }

    public List<Vector2Int> GenerateBombLocations(){
        var locations = new List<Vector2Int>(bombs);
        for(var i = 0; i < bombs; i++) {
            var l = GenerateBombLocation();
            while (locations.Contains(l)) {
                l = GenerateBombLocation();
            }
            locations.Add(l);
        }
        return locations;
    }

    public void SetNumbersAroundBomb(Vector2Int bomb) {
        // set all spaces around the bomb and catch array index out of bounds errors instead of adding extra logic
        // top left
        try {
            if (spaces[bomb.x - 1][bomb.y - 1] >= EMPTY) {
                spaces[bomb.x - 1][bomb.y - 1]++;
            }
        } catch (ArgumentOutOfRangeException) {}
        // middle left
        try {
            if (spaces[bomb.x - 1][bomb.y] >= EMPTY) {
                spaces[bomb.x - 1][bomb.y]++;
            }
        } catch (ArgumentOutOfRangeException) {}
        // bottom left
        try {
            if (spaces[bomb.x - 1][bomb.y + 1] >= EMPTY) {
                spaces[bomb.x - 1][bomb.y + 1]++;
            }
        } catch (ArgumentOutOfRangeException) {}

        // top middle
        try {
            if (spaces[bomb.x][bomb.y - 1] >= EMPTY) {
                spaces[bomb.x][bomb.y - 1]++;
            }
        } catch (ArgumentOutOfRangeException) {}
        // bottom middle
        try {
            if (spaces[bomb.x][bomb.y + 1] >= EMPTY) {
                spaces[bomb.x][bomb.y + 1]++;
            }
        } catch (ArgumentOutOfRangeException) {}

        // top right
        try {
            if (spaces[bomb.x + 1][bomb.y - 1] >= EMPTY) {
                spaces[bomb.x + 1][bomb.y - 1]++;
            }
        } catch (ArgumentOutOfRangeException) {}
        // middle right
        try {
            if (spaces[bomb.x + 1][bomb.y] >= EMPTY) {
                spaces[bomb.x + 1][bomb.y]++;
            }
        } catch (ArgumentOutOfRangeException) {}
        // bottom right
        try {
            if (spaces[bomb.x + 1][bomb.y + 1] >= EMPTY) {
                spaces[bomb.x + 1][bomb.y + 1]++;
            }
        } catch (ArgumentOutOfRangeException) {}
    }

    public void SetBombsAndNumbers(List<Vector2Int> bombLocations) {
        foreach(var bomb in bombLocations) {
            spaces[bomb.x][bomb.y] = BOMB;
            SetNumbersAroundBomb(bomb);
        }
    }

    public void GenerateBoard() {
        InitializeBoard();
        var bombLocations = GenerateBombLocations();
        SetBombsAndNumbers(bombLocations);
    }
}
