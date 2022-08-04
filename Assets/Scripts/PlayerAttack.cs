using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;
    [SerializeField] GameObject Barrel;
    [SerializeField] GameObject[] waterProjectile;
    [SerializeField] AudioSource sfx;
    [SerializeField] float fireCooldown = 0.5f;

    float fireTargetTime = float.MinValue;
    Vector2 weaponVector;
    Vector2 snappedWeaponVector;

    void Update()
    {
        weaponVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        weaponVector.Normalize();

        snappedWeaponVector = SnapNormalizedVector2To8WayGrid(weaponVector);

        if(Input.GetMouseButtonDown(0) && fireTargetTime < Time.time)
        {
            FireWater();
            fireTargetTime = Time.time + fireCooldown;
            sfx.Play();
        }
    }

    void FireWater()
    {
        if (waterProjectile != null)
        {
            System.Random rand = new System.Random();
            GameObject WP = Instantiate(waterProjectile[rand.Next(0, waterProjectile.Length)], Barrel.transform.position, Quaternion.identity);
            WP.GetComponent<bulletScript>().SetFlightVector(snappedWeaponVector);
        }
    }
    Vector2 SnapNormalizedVector2To8WayGrid(Vector2 vector)
    {
        float smallestAngle = Mathf.Min(Vector2.Angle(vector, Vector2.left), Vector2.Angle(vector, Vector2.right),
            Vector2.Angle(vector, Vector2.up), Vector2.Angle(vector, Vector2.down),
            Vector2.Angle(vector, new Vector2(0.7f, 0.7f)), Vector2.Angle(vector, new Vector2(0.7f, -0.7f)),
            Vector2.Angle(vector, new Vector2(-0.7f, -0.7f)), Vector2.Angle(vector, new Vector2(-0.7f, 0.7f)));

            Barrel.transform.position = Functions.AddVector2sTogether(transform.position, snappedWeaponVector);

        if (smallestAngle == Vector2.Angle(vector, Vector2.left))
        {
            SetBools(Dir.Left);
            return Vector2.left;
        }
        else if (smallestAngle == Vector2.Angle(vector, Vector2.right))
        {
            SetBools(Dir.Right);
            return Vector2.right;
        }
        else if (smallestAngle == Vector2.Angle(vector, Vector2.up))
        {
            SetBools(Dir.Up);
            return Vector2.up;
        }
        else if (smallestAngle == Vector2.Angle(vector, Vector2.down))
        {
            SetBools(Dir.Down);
            return Vector2.down;
        }
        else if (smallestAngle == Vector2.Angle(vector, new Vector2(0.7f, 0.7f)))
        {
            SetBools(Dir.TopRight);
            return new Vector2(0.7f, 0.7f);
        }
        else if (smallestAngle == Vector2.Angle(vector, new Vector2(0.7f, -0.7f)))
        {
            SetBools(Dir.BottomRight);
            return new Vector2(0.7f, -0.7f);
        }
        else if (smallestAngle == Vector2.Angle(vector, new Vector2(-0.7f, -0.7f)))
        {
            SetBools(Dir.BottomLeft);
            return new Vector2(-0.7f, -0.7f);
        }
        else
        {
            SetBools(Dir.TopLeft);
            return new Vector2(-0.7f, 0.7f);
        }
    }

    void SetBools(Dir boolToLeaveActive)
    {
        for (int i = 0; i < 8; i++)
        {
            if (boolToLeaveActive != (Dir)i)
            {
                Dir annen = (Dir)i;
                string name = annen.ToString();

                animator.SetBool(name, false);
            }
            else
                animator.SetBool(boolToLeaveActive.ToString(), true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Functions.AddVector2sTogether(transform.position, weaponVector));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, Functions.AddVector2sTogether(transform.position, snappedWeaponVector));
    }
}
