using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPikcup : MonoBehaviour
{
    public WeaponBase pickup;
    GameObject gfxInstance;

    public void Initialize(WeaponBase item)
    {
        gfxInstance = Instantiate(item.weaponPrefab, transform);
        pickup = item;
    }

    // Update is called once per frame
    void Update()
    {
        gfxInstance.transform.Rotate(Vector3.up, 60 * Time.deltaTime);
    }
}
