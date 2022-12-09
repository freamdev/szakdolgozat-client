using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    Button progressButton;
    [SerializeField]
    TextMeshProUGUI currentScoreText;



    LocalDataStore localStore;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        localStore = GameObject.FindObjectOfType<LocalDataStore>();
        progressButton.interactable = !localStore.IsOfflineMode;

        if (!localStore.IsOfflineMode)
        {
            StartCoroutine(LoadPlayerTopScore());
        }

    }

    IEnumerator LoadPlayerTopScore()
    {
        var url = ServerConstants.BaseUrl;
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{url}/api/getPlayerData?playerId={localStore.CurrentUser}"))
        {
            webRequest.SetRequestHeader("Authorization", $"Bearer {localStore.JwtToken}");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                print(webRequest.downloadHandler.text);
                var data = JsonConvert.DeserializeObject<PlayerDataModel>(webRequest.downloadHandler.text);
                currentScoreText.text = $"Your score: {data.killedEnemies}";
            }
            else
            {
                print(webRequest.error);
                currentScoreText.text = $"Your score: -";
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneConstants.BattleSceen);
    }

    public void TopScores()
    {
        SceneManager.LoadScene(SceneConstants.TopScoresSceen);
    }
}
