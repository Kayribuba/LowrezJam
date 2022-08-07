using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnappedVector2Script))]
public class CameraZoomMovementScript : MonoBehaviour
{
    [SerializeField] GameManagerScript GM;
    [SerializeField] Camera Kamera;
    [SerializeField] Transform Player;

    [SerializeField] int cameraZoomDistance = 5;

    bool gameIsPaused;
    bool holding;
    float gridSize = 0.15625f;
    SnappedVector2Script SV2S;

    void Start()
    {
        SV2S = GetComponent<SnappedVector2Script>();
        SV2S.SnappedVectorChangedEvent += SV2S_SnappedVectorChangedEvent;

        gridSize = FindObjectOfType<GridSizer>().GetGridSize();

        if (GM == null && GetComponent<GameManagerScript>() != null)
            GM = GetComponent<GameManagerScript>();
        GM.GamePauseEvent += GM_GamePauseEvent;
    }

    void SV2S_SnappedVectorChangedEvent(object sender, string e)
    {
        //if (holding)
        //    ChangeCamPos();
    }

    void GM_GamePauseEvent(object sender, bool e)
    {
        gameIsPaused = e;
        ResetCameraPosition();
    }

    void ResetCameraPosition()
    {
        Vector3 targetPos = Player.position;
        targetPos.z = -10;
        Kamera.transform.position = targetPos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            holding = true;
        if (Input.GetMouseButtonUp(1))
            holding = false;

        if (!gameIsPaused)
        {
            if(holding)
            {
                ChangeCamPos();
            }
            if (!holding)
            {
                ResetCameraPosition();
            }
        }
    }

    void ChangeCamPos()
    {
        Vector3 targetPos = Functions.AddVector2sTogether(Player.position, GetComponent<SnappedVector2Script>().Get8WaySnappedVector2() * cameraZoomDistance * gridSize);
        targetPos.z = -10;
        Kamera.transform.position = targetPos;
    }
}
