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
            
        }

        return dir;
    }
}
