using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    // this is the class that handles all of our projectiles in the game
    [Header("Core Properties")]
    [SerializeField] float speed; // how quickly this bullet is currently moving
    [SerializeField] Vector2 size; // the size of this projectile
    [SerializeField] float drag; // how much the bullet slows down overtime
    [SerializeField] float lifetime; // how long will this bullet live in seconds?
    [SerializeField] float damageToPlayer, damageToEnemy; // damage dealt to the player and to the enemy
    [SerializeField] float angularDrag; // how much the bullet rotates overtime
    [SerializeField] bool doesShrink; // does this projectile shrink overtime?
    float maxLifetime;
    [SerializeField] AudioSource shotAudioSource;

    private void Start()
    {
        // determine our angular drag
        angularDrag = Random.Range(-angularDrag, angularDrag);
        maxLifetime = lifetime;
        // play our shot
        shotAudioSource = GetComponent<AudioSource>();
        shotAudioSource.pitch = 1 + Random.Range(-0.05f, 0.05f);
        shotAudioSource.Play();
    }

    private void FixedUpdate()
    {
        // process the collision of this object
        ProcessCollision();
        // process our bullet's movement
        ProcessMovement();
        // process the lifetime of this bullet
        ProcessLifetime();
    }

    void ProcessMovement()
    {
        // move to the right
        transform.position += transform.right * speed * Time.deltaTime;
        // then slow down our speed
        if (speed > 0)
            speed -= drag * Time.deltaTime;
        if (speed < 0)
            speed = 0;

        // process our angular drag, determined in the start function
        transform.localEulerAngles += new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angularDrag * Time.deltaTime);
        // increase our angular drag overtime
        if (Mathf.Abs(angularDrag) < 200)
            angularDrag += angularDrag * 0.25f;
        else angularDrag = 200 * Mathf.Sign(angularDrag);

        // do we shrink?
        if (doesShrink)
        {
            transform.localScale = Vector2.one * (lifetime / maxLifetime);
        }
    }

    void ProcessLifetime()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            DestroyThisProjectile();

        if (speed == 0)
            DestroyThisProjectile();

        if (transform.localScale.x <= 0.001f)
            DestroyThisProjectile();
    }

    void DestroyThisProjectile()
    {
        Destroy(gameObject);
    }

    // the results of the boxcast
    List<RaycastHit2D> hitResults = new List<RaycastHit2D>();
    [SerializeField] ContactFilter2D contactFilter;
    void ProcessCollision()
    {
        // clear our hit results
        if (hitResults.Count > 0)
            hitResults.Clear();

        // do a boxcast to our next position and see if we hit anything
        if (Physics2D.BoxCast(transform.position, size, transform.eulerAngles.z, transform.right, contactFilter, hitResults, speed * Time.deltaTime) > 0)
        {
            // check the tag of the objects we have hit
            foreach (var hit in hitResults)
            {
                CollisionActions(hit.collider.GetComponent<CustomTag>()?.tag, hit.collider?.gameObject);
            }
        }
    }

    void CollisionActions(CustomTag.Tags? hitTag, GameObject? hitObject)
    {
        // check if this is not null
        if (hitTag == null || hitObject == null) return;

        // correlate our actions to different resulting states
        if (hitTag == CustomTag.Tags.Enemy)
        {
            // damage to enemy here

            DestroyThisProjectile();
        }

        if (hitTag == CustomTag.Tags.Player)
        {
            // damage to player here

            DestroyThisProjectile();
        }

        if (hitTag == CustomTag.Tags.Wall)
        {
            DestroyThisProjectile();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

}
