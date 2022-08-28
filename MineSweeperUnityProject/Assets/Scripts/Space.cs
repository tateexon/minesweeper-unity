using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : ItemTarget
{
    public int spaceType = 0;
    public Vector2Int p;

    public bool isClicked;
    public bool isFlagged;

    public override void LeftClicked()
    {
        if (isClicked || isFlagged) {
            return;
        }
        if (spaceType == BoardData.BOMB) {
            Debug.Log("bomb hit end game");
        } else {
            Debug.Log("hit number: " + spaceType + " at " + p);
        }
        isClicked = true;
    }

    public override void RightClicked()
    {
        if (isClicked) {
            return;
        }
        isFlagged = !isFlagged;
        Debug.Log("flagging space: " + p);
    }
}
