using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // our objects
    [SerializeField] Transform player, cursor;
    [SerializeField] float spread;

    // process our position
    void ProcessCameraPosition()
    {
        transform.position = Vector3.Lerp(player.position, cursor.position, spread);
    }

    // 60 times per second
    private void FixedUpdate()
    {
        ProcessCameraPosition();
    }
}
