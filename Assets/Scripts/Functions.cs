using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Functions
{
    public static Dir Vector2ToDir(Vector2 vector)
    {
        Dir direction;

        if (vector == Vector2.up)
            direction = Dir.Up;
        else if (vector == Vector2.right)
            direction = Dir.Right;
        else if (vector == Vector2.down)
            direction = Dir.Down;
        else if (vector == Vector2.left)
            direction = Dir.Left;
        else if (vector == new Vector2(0.7f, 0.7f))
            direction = Dir.TopRight;
        else if (vector == new Vector2(0.7f, -0.7f))
            direction = Dir.BottomRight;
        else if (vector == new Vector2(-0.7f, -0.7f))
            direction = Dir.BottomLeft;
        else
            direction = Dir.TopLeft;

        return direction;
    }
    public static Vector2 AddVector2sTogether(Vector2 vector1, Vector2 vector2)
    {
        Vector2 targetVector = new Vector2();
        targetVector.x = vector1.x + vector2.x;
        targetVector.y = vector1.y + vector2.y;

        return targetVector;
    }
    public static Vector2 AddVector2sTogether(Vector2 vector1, Vector2 vector2, Vector2 vector3)
    {
        Vector2 targetVector = new Vector2();
        targetVector.x = vector1.x + vector2.x + vector3.x;
        targetVector.y = vector1.y + vector2.y + vector3.y;

        return targetVector;
    }
    public static Vector2 AddVector2sTogether(Vector2 vector1, Vector2 vector2, Vector2 vector3, Vector2 vector4)
    {
        Vector2 targetVector = new Vector2();
        targetVector.x = vector1.x + vector2.x + vector3.x + vector4.x;
        targetVector.y = vector1.y + vector2.y + vector3.y + vector4.y;

        return targetVector;
    }
}
