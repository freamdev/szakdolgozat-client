using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    public List<WeaponBase> weapons;

    public Transform firePosition;
    public WeaponBase currentWeapon;

    public TextMeshProUGUI weaponText;

    float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = -999;
        currentWeapon = weapons[0];

        currentWeapon.weaponEffects.ForEach(f => f.weaponFireStrategy.Generate());

        currentWeapon.SwapTo(GameObject.Find("WeaponHolder").transform);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeaponUI();
    }

    void UpdateWeaponUI()
    {
        if (currentWeapon.infiniteWeapon)
        {
            weaponText.text = "N/A";
        }
        else
        {
            weaponText.text = currentWeapon.clipSize.ToString();
        }
    }

    public void ChangeWeapon(int v)
    {
        if (weapons[v - 1] != null)
        {
            currentWeapon.Drop();
            currentWeapon = weapons[v - 1];
            currentWeapon.SwapTo(GameObject.Find("WeaponHolder").transform);
        }
    }

    public Transform GetFirePositionTransform()
    {
        return currentWeapon.GetFirePositionTransform();
    }

    public void Fire()
    {
        currentWeapon.Fire(this);
    }

    public void FireHappened()
    {
        lastAttackTime = Time.time;
        currentWeapon.clipSize -= 1;
        if (currentWeapon.clipSize == 0 && !currentWeapon.infiniteWeapon)
        {
            weapons.RemoveAt(weapons.IndexOf(currentWeapon));
            currentWeapon.Drop();
            currentWeapon = weapons.FirstOrDefault(f => f != null);
            currentWeapon.SwapTo(GameObject.Find("WeaponHolder").transform);
        }
    }

    public bool Canfire(float cooldown)
    {
        return lastAttackTime + cooldown < Time.time && (currentWeapon.clipSize > 0 || currentWeapon.infiniteWeapon);
    }
}
