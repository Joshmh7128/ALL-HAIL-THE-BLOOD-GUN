using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    PlayerController controller;
    PlayerCursor cursor;
    Vector3 targetRotation;

    private void Start()
    {
        controller = PlayerController.instance;
        cursor = PlayerCursor.instance;
    }

    private void FixedUpdate()
    {
        ProcessPosition();
        ProcessRotation();
    }

    // process the rotation of our weapon
    void ProcessRotation()
    {
        transform.right = new Vector3(cursor.transform.position.x, cursor.transform.position.y, transform.position.z) - transform.position;
    }

    void ProcessPosition()
    {
        transform.position = controller.transform.position;
    }
}
