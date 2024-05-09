using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    PlayerController controller;
    PlayerCursor cursor;
    SpriteRenderer ourSprite;

    [Header("Weapon Attributes")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate;
    [SerializeField] int burstAmount;
    float currentFireRate, maxFireRate;

    private void Start()
    {
        controller = PlayerController.instance;
        cursor = PlayerCursor.instance;
        ourSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // we capture our weapon input in the update
        ProcessWeaponFiring();
    }

    private void FixedUpdate()
    {
        // process the rotation and position of our weapon
        ProcessPosition();
        ProcessRotation();
        // manage the firerate tracking of this gun
        ManageFirerate();
    }

    // process the rotation of our weapon
    void ProcessRotation()
    {
        transform.right = new Vector3(cursor.transform.position.x, cursor.transform.position.y, transform.position.z) - transform.position;
        // if our transform.right is less than 0, flip the sprite of our weapon
        if (transform.right.x < 0)
            ourSprite.flipY = true;
        else ourSprite.flipY = false;
    }

    // process the position of our weapon
    void ProcessPosition()
    {
        transform.position = controller.transform.position;
    }

    // process the firing of our weapon
    void ProcessWeaponFiring()
    {
        // if we are clicking left click with the mouse
        if (Input.GetMouseButton(0) && (currentFireRate <= 0))
        {
            // then shoot
            FireWeapon();
        }
    }

    // fire this weapon and its projectile
    void FireWeapon()
    {
        // instantiate a new projectile at the fire point
        var fired = Instantiate(projectile, firePoint.position, Quaternion.identity);
        fired.transform.right = transform.right; // make sure it is going in the right direction
        // then reset our firerate
        currentFireRate = fireRate;
    }

    void ManageFirerate()
    {
        // reduce our fire rate
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }
}
