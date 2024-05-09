using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerWeapon : MonoBehaviour
{
    PlayerController controller;
    PlayerCursor cursor;
    SpriteRenderer ourSprite;

    [Header("Weapon Attributes")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate, currentFireRate;
    [SerializeField] int burstAmount;
    [SerializeField] float burstRate; // the amount of time between each bullet during a burst
    [SerializeField] bool isAutomatic; // is this weapon automatic?
    [SerializeField] float inaccuracy; // how inaccurate is this weapon?
    [SerializeField] AudioSource reloadAudioSource;

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
        // for automatic firing
        if (isAutomatic)
        {
            // if we are clicking left click with the mouse
            if (Input.GetMouseButton(0) && (currentFireRate <= 0))
            {
                // then shoot
                FireWeapon();
            }
        }

        // for semi-automatic firing
        if (!isAutomatic)
        {
            // if we are clicking left click with the mouse
            if (Input.GetMouseButtonDown(0) && (currentFireRate <= 0))
            {
                // then shoot
                FireWeapon();
            }
        }
    }

    // fire this weapon and its projectile
    void FireWeapon()
    {
        // reset our firerate
        currentFireRate = fireRate;

        // make sure our burst amount cannot be 0
        if (burstAmount <= 0) burstAmount = 1;

        // queue up our burst
        for (int i = 0; i < burstAmount; i++)
        {
            Debug.Log("requesting queue shot");
            StartCoroutine(QueueShot(burstRate * i));

            // on our last shot, queue the reload sound playback
            if (i == burstAmount-1)
                StartCoroutine(QueueReloadSound());
        }
    }

    void FireShot()
    {
        // instantiate a new projectile at the fire point
        var fired = Instantiate(projectile, firePoint.position, Quaternion.identity);
        fired.transform.right = transform.right; // make sure it is going in the right direction

        // randomize the direction based off of our inaccuracy
        fired.transform.localEulerAngles = new Vector3(
            fired.transform.localEulerAngles.x,
            fired.transform.localEulerAngles.y,
            fired.transform.localEulerAngles.z + Random.Range(-inaccuracy, inaccuracy)
            );
    }

    IEnumerator QueueShot(float time)
    {
        Debug.Log("queuing shot");
        yield return new WaitForSeconds(time);
        FireShot();
    }

    IEnumerator QueueReloadSound()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        reloadAudioSource.Play();
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
