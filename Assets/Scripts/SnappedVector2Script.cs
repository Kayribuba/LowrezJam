using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappedVector2Script : MonoBehaviour
{
    public event EventHandler<string> SnappedVectorChangedEvent;

    Vector2 weaponVector;
    Vector2 snappedWeaponVector;
    Vector2 oldSnappedWeaponVector;

    void Update()
    {
        weaponVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        weaponVector.Normalize();

        if (snappedWeaponVector != null)
            oldSnappedWeaponVector = snappedWeaponVector;

        snappedWeaponVector = SnapNormalizedVector2To8WayGrid(weaponVector);

        if(oldSnappedWeaponVector != null)
        {
            if (oldSnappedWeaponVector != snappedWeaponVector)
                SnappedVectorChangedEvent?.Invoke(this, "Snapped Vector has changed");
        }
    }

    public Vector2 Get8WaySnappedVector2()
    {
        return snappedWeaponVector;
    }

    public Vector2 SnapNormalizedVector2To8WayGrid(Vector2 vector)
    {
        float smallestAngle = Mathf.Min(Vector2.Angle(vector, Vector2.left), Vector2.Angle(vector, Vector2.right),
            Vector2.Angle(vector, Vector2.up), Vector2.Angle(vector, Vector2.down),
            Vector2.Angle(vector, new Vector2(0.7f, 0.7f)), Vector2.Angle(vector, new Vector2(0.7f, -0.7f)),
            Vector2.Angle(vector, new Vector2(-0.7f, -0.7f)), Vector2.Angle(vector, new Vector2(-0.7f, 0.7f)));


        if (smallestAngle == Vector2.Angle(vector, Vector2.left))
        {
            return Vector2.left;
        }
        else if (smallestAngle == Vector2.Angle(vector, Vector2.right))
        {
            return Vector2.right;
        }
        else if (smallestAngle == Vector2.Angle(vector, Vector2.up))
        {
            return Vector2.up;
        }
        else if (smallestAngle == Vector2.Angle(vector, Vector2.down))
        {
            return Vector2.down;
        }
        else if (smallestAngle == Vector2.Angle(vector, new Vector2(0.7f, 0.7f)))
        {
            return new Vector2(0.7f, 0.7f);
        }
        else if (smallestAngle == Vector2.Angle(vector, new Vector2(0.7f, -0.7f)))
        {
            return new Vector2(0.7f, -0.7f);
        }
        else if (smallestAngle == Vector2.Angle(vector, new Vector2(-0.7f, -0.7f)))
        {
            return new Vector2(-0.7f, -0.7f);
        }
        else
        {
            return new Vector2(-0.7f, 0.7f);
        }
    }
}
