using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform firePoint;
    public int currentAmmo;
    [Header("Pistol properties")]
    [Range(0, 100)] public int MaxAmmoPistol = 30;
    [Range(0, 100)] public int MagazineSizePistol = 190;
    [Header("Rifle properties")]
    [Range(0, 100)] public int MaxAmmoRifle = 30;
    [Range(0, 100)] public int MagazineSizeRifle = 190;

    private float lastFireTime = 0f;
    private float fireRate = 0.05f;

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastFireTime + fireRate)
        {
            if (currentAmmo > 0)
            {
                Debug.Log("shoots");
                Shoot();
                lastFireTime = Time.time;
            }
            else
            {
                Debug.Log("Out of Ammo!");
            }
        }
        // kijkt naar de muis pozietzi en onthoud waar die is
            Vector3 mousePosition = Input.mousePosition;

        // zorgt er voor dat de muis z posietzie en die van de camera het zelfde zijn
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;

        // zet de muis positzie van het scherm om naar cordienaten van de wereld
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the object to the mouse position
        Vector2 direction = worldMousePosition - transform.position;

        // bekijkt de rotatie van de gun
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // hier gebeurt de rotatie door middle van de Euler
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    public void Shoot()
    {
        // Calculate the direction based on the rotation of the firePoint
        Vector3 fireDirection = firePoint.right; // Assuming firePoint faces right in 2D
        print(fireDirection);
    
        // Instantiate a bullet prefab at the firePoint position and rotation
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Set the direction of the bullet
        bullet.SetDirection(fireDirection);
    
        // Decrease the current ammo
        currentAmmo--;
    }

    // Method to reload the weapon
    public virtual void Reload()
    {
        int ammo = MaxAmmoPistol - currentAmmo;
        if (MagazineSizePistol >= ammo)
        {
            currentAmmo += ammo;
            MagazineSizePistol -= ammo;
        }
        else
        {
            currentAmmo += MagazineSizePistol;
            MagazineSizePistol = 0;
        }
    }
}