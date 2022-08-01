using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Functions
{
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
