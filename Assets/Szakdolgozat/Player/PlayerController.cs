using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Transactions;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxHealth;
    float health;

    public Image healthBar;
    public TextMeshProUGUI healthText;

    PlayerWeaponController weaponController;
    LocalDataStore localDataStore;

    int killCounter;

    bool isDead;

    private void Awake()
    {
        killCounter = 0;
        weaponController = GetComponent<PlayerWeaponController>();
        localDataStore = GameObject.FindObjectOfType<LocalDataStore>();
    }

    private void Start()
    {
        maxHealth = 30;
        InitializeHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void LooseHealth(float value)
    {
        health -= value;
        UpdateHealthBar();
        print(localDataStore.IsOfflineMode);
        if (health <= 0 && !isDead)
        {
            isDead = true;
            if (localDataStore.IsOfflineMode)
            {
                SceneManager.LoadScene(SceneConstants.MainMenuScreen);
            }
            else
            {
                StartCoroutine(SaveProgress());
            }
        }
    }

    IEnumerator SaveProgress()
    {
        var url = ServerConstants.BaseUrl;
        var token = localDataStore.JwtToken;

        var postData = new PlayerDataModel
        {
            playerId = localDataStore.CurrentUser,
            killedEnemies = killCounter,
            passedFloor = 3,
            time = Time.timeSinceLevelLoad
        };

        var jsonData = JsonConvert.SerializeObject(postData);

        var request = new UnityWebRequest($"{url}/api/stores", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", $"Bearer {token}");
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Status Code: " + request.error);
        }
        SceneManager.LoadScene(SceneConstants.MainMenuScreen);
    }

    public void InitializeHealthBar()
    {
        health = maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = $"{health}/{maxHealth}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Loot")
        {
            weaponController.weapons.Add(Instantiate(other.GetComponent<WeaponPikcup>().pickup));
            Destroy(other.gameObject);
        }
        if (other.tag == "EnemyProjectile")
        {
            other.GetComponent<EnemyProjectile>();
        }
    }

    public void KilledOne()
    {
        killCounter++;
    }

}
