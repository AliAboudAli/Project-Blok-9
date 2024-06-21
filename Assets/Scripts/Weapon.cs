using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected enum AmmoType { Pistol, Rifle }
    [SerializeField] protected AmmoType ammoType;

    public Ammo ammo;
    public Bullet bulletPrefab;
    public Transform firePoint;
    
    [Header("Gun properties")] 
    public int currentBullets;
    public int magSize = 14;
    private float lastFireTime = 0f;
    private float fireRate = 0.05f;

    public void Start()
    {
        currentBullets = magSize;

        if (ammo == null)
        {
            ammo = GetComponent<Ammo>();
            if (ammo == null)
            {
                Debug.LogError("No Ammo component found on weapon.");
            }
        }
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastFireTime + fireRate)
        {
            if (currentBullets > 0)
            {
                Shoot();
                lastFireTime = Time.time;
            }
            else
            {
                Debug.Log("Out of Ammo!");
            }
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = worldMousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Shoot()
    {
        Vector3 fireDirection = firePoint.right; // Assuming firePoint faces right in 2D
        print(fireDirection);
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.SetDirection(fireDirection);
        currentBullets--;
    }

    public void Reload()
    {
        if (ammo == null)
        {
            Debug.LogError("No Ammo component found.");
            return;
        }

        switch (ammoType)
        {
            case AmmoType.Pistol:
                if (ammo.pistolAmmo > 0)
                {
                    int bulletsNeeded = magSize - currentBullets;
                    int bulletsToLoad = Mathf.Min(bulletsNeeded, ammo.pistolAmmo);
                    currentBullets += bulletsToLoad;
                    ammo.pistolAmmo -= bulletsToLoad;
                }
                break;
            case AmmoType.Rifle:
                if (ammo.rifleAmmo > 0)
                {
                    int bulletsNeeded = magSize - currentBullets;
                    int bulletsToLoad = Mathf.Min(bulletsNeeded, ammo.rifleAmmo);
                    currentBullets += bulletsToLoad;
                    ammo.rifleAmmo -= bulletsToLoad;
                }
                break;
        }

        Debug.Log("Reloaded weapon. Current bullets: " + currentBullets);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.PickupWeapon(this);
            }
        }
    }
}