using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LocalDataStore : MonoBehaviour
{
    [SerializeField]
    WeaponGenerator weaponGenerator;

    [SerializeField]
    string jwtToken;
    public string JwtToken { get { return jwtToken; } set { jwtToken = value; } }

    bool isOfflineMode;
    public bool IsOfflineMode { get { return isOfflineMode; } set { isOfflineMode = value; } }
    
    string currentUser;
    public string CurrentUser { get { return currentUser; } set { currentUser = value; } }


    private void Awake()
    {
        //var otherStores = GameObject.FindObjectsOfType<LocalDataStore>();
        //otherStores.Where(w => w != this).ToList().ForEach(f => Destroy(f.gameObject));

        DontDestroyOnLoad(this);
        if(weaponGenerator == null)
        {
            weaponGenerator = Instantiate(Resources.Load<WeaponGenerator>("WeaponData"));
            weaponGenerator.Load();
        }
    }

    public WeaponGenerator GetWeaponGenerator()
    {
        return weaponGenerator;
    }
}
