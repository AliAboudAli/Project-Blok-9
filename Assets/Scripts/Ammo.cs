using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int pistolAmmo = 12;
    public int rifleAmmo = 45;

    public void AddAmmo(int amount)
    {
        pistolAmmo += amount;
        rifleAmmo += amount;
    }
}