using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon currentWeapon;
    [Header("Movement")] 
    [Range(0f, 100f)] public float movementSpeed = 5f;
    [Range(0f, 100f)] public float jumpForce = 1f;
    [Range(0f, 100f)] public float maxJump = 10f;
    public bool isGrounded;
    private Rigidbody2D _rb;

    [Header("Aiming with mouse position Y")] 
    [Range(0f, 100f)] float mouseSens = 100f;

    public Transform Cam;

    [Range(0f, 100f)] [SerializeField] private float XRotate = 0f;
    public Vector3 direction;

    private void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody2D>();
        // Voorzorgt dat die niet omvalt bij een collider (alleen Rigidbody2D)
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (currentWeapon == null)
        {
            currentWeapon = GetComponentInChildren<Weapon>();
        }

        if (currentWeapon != null && currentWeapon.ammo == null)
        {
            currentWeapon.ammo = GetComponentInChildren<Ammo>();
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontal, 0);
        transform.Translate(move * Time.deltaTime * movementSpeed);

        if (Input.GetButton("left shift"))
        {
            movementSpeed = 10f;
        }
        else
        {
            movementSpeed = 5f;
        }

        // Check jump input and if the player is grounded
        if (Input.GetButton("Jump") && isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            if (_rb.velocity.y > maxJump)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, maxJump);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon?.Reload();
        }
    }

    public void PickupAmmo(int pistolAmmo, int rifleAmmo)
    {
        if (currentWeapon != null && currentWeapon.ammo != null)
        {
            currentWeapon.ammo.pistolAmmo += pistolAmmo;
            currentWeapon.ammo.rifleAmmo += rifleAmmo;
        
            currentWeapon.Reload(); // Reload the weapon after picking up ammo
        }
        else
        {
            Debug.LogError("No current weapon or ammo reference found.");
        }
    }

    public void PickupWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = newWeapon;
        currentWeapon.transform.SetParent(transform);
        currentWeapon.transform.localPosition = Vector3.zero;

        if (currentWeapon.ammo == null)
        {
            currentWeapon.ammo = currentWeapon.GetComponent<Ammo>();
        }

        Debug.Log("Picked up weapon: " + newWeapon.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AmmoPickup"))
        {
            Ammo ammoPickup = other.GetComponent<Ammo>();
            if (ammoPickup != null)
            {
                PickupAmmo(ammoPickup.pistolAmmo, ammoPickup.rifleAmmo);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}