using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(SnappedVector2Script))]
public class PlayerAttack : MonoBehaviour
{
    public event EventHandler<bool> ReloadEvent;

    public Animator animator;

    [SerializeField] Slider ReloadIndicatorBar;
    [SerializeField] Slider WaterBar;
    [SerializeField] int maxWater = 30;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] GameManagerScript GM;
    [SerializeField] GameObject Barrel;
    [SerializeField] GameObject[] waterProjectile;
    [SerializeField] AudioSource sfx;
    [SerializeField] float fireCooldown = 0.5f;

    float reloadTimeFragment;
    float reloadIndicatorTargetTime = float.MaxValue;

    bool gameIsPaused;
    bool isReloading;
    int water;
    float fireTargetTime = float.MinValue;
    float reloadTargetTime = float.MaxValue;
    float gridSize;
    Vector2 snappedWeaponVector;

    void Start()
    {
        if(GM == null && GetComponent<GameManagerScript>() != null)
        GM = GetComponent<GameManagerScript>();
        if (GM != null)
            GM.GamePauseEvent += GM_GamePauseEvent;

        water = maxWater;
        WaterBar.maxValue = maxWater;
        WaterBar.value = water;

        reloadTimeFragment = reloadTime / (ReloadIndicatorBar.maxValue + 1);

        ReloadIndicatorBar.value = 0;
    }
    void GM_GamePauseEvent(object sender, bool e)
    {
        gameIsPaused = e;
    }


    void Update()
    {
        if (!gameIsPaused)
        {
            snappedWeaponVector = GetComponent<SnappedVector2Script>().Get8WaySnappedVector2();

            SetBools(snappedWeaponVector);

            Barrel.transform.position = Functions.AddVector2sTogether(transform.position, snappedWeaponVector);

            Vector2 barrelCorrectorVector = Barrel.transform.position;
            barrelCorrectorVector.x -= barrelCorrectorVector.x % gridSize;
            barrelCorrectorVector.y -= barrelCorrectorVector.y % gridSize;
            Barrel.transform.position = barrelCorrectorVector;


            if (Input.GetMouseButtonDown(0) && fireTargetTime < Time.time && !isReloading)
                FireWater();

            if(reloadIndicatorTargetTime <= Time.time)//bar valuesi arttý
            {
                ReloadIndicatorBar.value ++;
                reloadIndicatorTargetTime += reloadTimeFragment;
            }

            if(reloadTargetTime <= Time.time)//reload bitti
            {
                CloseReloadBar();

                water = maxWater;
                WaterBar.value = water;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenReloadBar();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            CloseReloadBar();
        }
    }

    void OpenReloadBar()
    {
        isReloading = true;
        reloadTargetTime = Time.time + reloadTime;
        ReloadEvent.Invoke(this, true);

        reloadIndicatorTargetTime = Time.time + reloadTimeFragment;

        Image[] images = ReloadIndicatorBar.GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            i.enabled = true;
        }
    }
    void CloseReloadBar()
    {
        isReloading = false;
        reloadTargetTime = float.MaxValue;
        ReloadEvent.Invoke(this, false);

        reloadIndicatorTargetTime = float.MaxValue;
        ReloadIndicatorBar.value = 0;

        Image[] images = ReloadIndicatorBar.GetComponentsInChildren<Image>();
        foreach(Image i in images)
        {
            i.enabled = false;
        }
    }

    public void SetGridSize(float gs) => gridSize = gs;
    void FireWater()
    {
        if (waterProjectile != null)
        {
            System.Random rand = new System.Random();
            GameObject nextProjectile = waterProjectile[rand.Next(0, waterProjectile.Length)];

            if(nextProjectile.GetComponent<PlayerBulletScript>()?.waterCost <= water)
            {
                fireTargetTime = Time.time + fireCooldown;
                sfx.Play();

                ConsumeWater(nextProjectile.GetComponent<PlayerBulletScript>().waterCost);

                GameObject WP = Instantiate(nextProjectile, Barrel.transform.position, Quaternion.identity);
                WP.GetComponent<PlayerBulletScript>().SetFlightVector(snappedWeaponVector, gridSize);
                WP.GetComponent<PlayerBulletScript>().SetParentGO(gameObject);
            }
        }
    }

    void ConsumeWater(int waterAmountToConsume)
    {
        water -= waterAmountToConsume;
        WaterBar.value = water;
    }
    void SetBools(Vector2 vector)
    {
        Dir boolToLeaveActive = Functions.Vector2ToDir(vector);

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
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, Functions.AddVector2sTogether(transform.position, snappedWeaponVector));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(0.15625f, 0.15625f, 1));
    }
}
