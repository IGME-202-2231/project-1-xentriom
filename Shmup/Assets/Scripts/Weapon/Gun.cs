using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// The GunType enum represents the different types of guns that can be used
/// </summary>
enum GunType { Pistol, Sniper, SMG, Shotgun }

/// <summary>
/// The AmmoType enum represents the different types of ammo that can be used
/// </summary>
enum AmmoType { Small, Medium, Large }

/// <summary>
/// The Gun class is used to represent the different guns that can be used
/// </summary>
public class Gun : MonoBehaviour
{
    // List of sprites for guns and ammos
    [SerializeField] public List<Sprite> gunSprite = new List<Sprite>();
    [SerializeField] public List<Sprite> AmmoSprite = new List<Sprite>();

    // Gun and ammo type enums
    private GunType gun;
    private AmmoType ammo;

    // Dictionaries for gun and ammo sprites
    private Dictionary<GunType, Sprite> gunDict = new Dictionary<GunType, Sprite>();
    private Dictionary<AmmoType, Sprite> ammoDict = new Dictionary<AmmoType, Sprite>();

    // Current gun and ammo
    private Sprite currentGun;
    private Sprite currentAmmo;

    /// <summary>
    /// Returns the current gun sprites
    /// </summary>
    public Sprite CurrentGun { get { return currentGun; } }

    /// <summary>
    /// Returns the current ammo sprites
    /// </summary>
    public Sprite CurrentAmmo { get { return currentAmmo; } }

    // Start is called before the first frame update
    void Start()
    {
        gunDict.Add(GunType.Pistol, gunSprite[0]);
        gunDict.Add(GunType.Sniper, gunSprite[1]);
        gunDict.Add(GunType.SMG, gunSprite[2]);
        gunDict.Add(GunType.Shotgun, gunSprite[3]);

        ammoDict.Add(AmmoType.Small, AmmoSprite[0]);
        ammoDict.Add(AmmoType.Medium, AmmoSprite[1]);
        ammoDict.Add(AmmoType.Large, AmmoSprite[2]);

        UseGun(GunType.Pistol);
    }

    public void UsePistol() { UseGun(GunType.Pistol); }
    public void UseSniper() { UseGun(GunType.Sniper); }
    public void UseSMG() { UseGun(GunType.SMG); }
    public void UseShotgun() { UseGun(GunType.Shotgun); }

    /// <summary>
    /// Sets the current gun and ammo sprites based on the given GunType parameter
    /// </summary>
    /// <param name="gun">The GunType value that represents the selected gun</param>
    private void UseGun(GunType gun)
    {
        currentGun = gunSprite[(int)gun];

        switch (gun)
        {
            case GunType.Pistol:
                ammo = AmmoType.Small;
                break;
            case GunType.Sniper:
                ammo = AmmoType.Large;
                break;
            case GunType.SMG:
                ammo = AmmoType.Small;
                break;
            case GunType.Shotgun:
                ammo = AmmoType.Medium;
                break;
        }

        currentAmmo = ammoDict[ammo];
    }
}