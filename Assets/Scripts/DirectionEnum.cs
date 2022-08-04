using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Dir { Left, Right, Up, Down, TopLeft, TopRight, BottomLeft, BottomRight }

public class DirectionEnum : MonoBehaviour
{
    public Vector2 GetVector2DirFromEnum(Dir directionEnumName)
    {
        Vector2 dir = Vector2.zero;

        switch(directionEnumName)
        {
            case Dir.Left:
                dir = Vector2.left;
                break;
            case Dir.Right:
                dir = Vector2.right;
                break;
            case Dir.Up:
                dir = Vector2.up;
                break;
            case Dir.Down:
                dir = Vector2.down;
                break;
            case Dir.TopRight:
                dir = new Vector2(0.7f, 0.7f);
                break;
            case Dir.BottomRight:
                dir = new Vector2(0.7f, -0.7f);
                break;
            case Dir.BottomLeft:
                dir = new Vector2(-0.7f, -0.7f);
                break;
            case Dir.TopLeft:
                dir = new Vector2(-0.7f, -0.7f);
                break;
        }

        return dir;
    }
}
