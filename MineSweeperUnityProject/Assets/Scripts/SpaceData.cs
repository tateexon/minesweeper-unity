using System;
using UnityEngine;

[Serializable()]
public class SpaceData
{
    public int type;
    public Vector2Int location;
    public bool isFlagged;
    public bool isClicked;

    // return trus if bomb hit
    public bool Click()
    {
        if (isClicked || isFlagged)
        {
            return false;
        }

        isClicked = true;

        return IsBomb();
    }

    public bool Flag()
    {
        if (isClicked)
        {
            return false;
        }
        isFlagged = !isFlagged;
        return isFlagged;
    }

    public bool IsBomb()
    {
        return type == BoardData.BOMB;
    }


}
