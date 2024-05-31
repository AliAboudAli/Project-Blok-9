using UnityEngine;
public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform firePoint;

    // Methode om te schieten
    public virtual void Shoot(Vector3 direction)
    {
        // Instantieer een kogel prefab op de vuurpositie van het wapen
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Stel de richting van de kogel in
        bullet.SetDirection(direction);
    }

    // Methode om het wapen te herladen
    public virtual void Reload()
    {
        
    }
}

// Afgeleide klasse voor een pistool
public class Pistol : Weapon
{
    // Methode om te schieten, specifiek voor pistool
    public override void Shoot(Vector3 direction)
    {
        // Specifieke logica voor het schieten met een pistool
        Debug.Log("Schiet met pistool");
        base.Shoot(direction); // Roep de basisklasse methode aan
    }

    // Methode om het pistool te herladen
    public override void Reload()
    {
        // Specifieke logica voor het herladen van een pistool
        Debug.Log("Herlaad pistool");
        // Bijvoorbeeld, reset het aantal kogels in het pistoolmagazijn
    }
}

// Afgeleide klasse voor een geweer
public class Rifle : Weapon
{
    // Methode om te schieten, specifiek voor geweer
    public override void Shoot(Vector3 direction)
    {
        // Specifieke logica voor het schieten met een geweer
        Debug.Log("Schiet met geweer");
        base.Shoot(direction); // Roep de basisklasse methode aan
    }

    // Methode om het geweer te herladen
    public override void Reload()
    {
        // Specifieke logica voor het herladen van een geweer
        Debug.Log("Herlaad geweer");
        // Bijvoorbeeld, reset het aantal kogels in het geweermagazijn
    }
}
