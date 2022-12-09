using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Weapons/Weapon")]
public class WeaponBase : ScriptableObject
{
    public GameObject weaponPrefab;

    public List<WeaponEffect> weaponEffects;

    GameObject weaponInstance;
    public int clipSize;
    public bool infiniteWeapon;

    public void Drop()
    {
        Destroy(weaponInstance);
    }

    public void SwapTo(Transform weaponHolder)
    {
        weaponInstance = GameObject.Instantiate(weaponPrefab, weaponHolder);
    }

    public Transform GetFirePositionTransform()
    {
        return weaponInstance.transform.Find("BulletPoint");
    }

    public void Fire(PlayerWeaponController pwc)
    {
        weaponEffects.ForEach(f => f.Activate(pwc));
    }
}
