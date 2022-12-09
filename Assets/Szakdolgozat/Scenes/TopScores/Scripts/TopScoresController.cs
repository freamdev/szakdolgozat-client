using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TopScoresController : MonoBehaviour
{
    [SerializeField]
    GameObject scoresTextHolder;

    [SerializeField]
    GameObject textPrefab;

    LocalDataStore localStore;

    // Start is called before the first frame update
    void Start()
    {
        localStore = GameObject.FindObjectOfType<LocalDataStore>();
        StartCoroutine(LoadTop10PlayerData());
    }

    IEnumerator LoadTop10PlayerData()
    {
        var url = ServerConstants.BaseUrl;
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{url}/api/getAllPlayerData"))
        {
            webRequest.SetRequestHeader("Authorization", $"Bearer {localStore.JwtToken}");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                print(webRequest.downloadHandler.text);
                var data = JsonConvert.DeserializeObject<List<PlayerDataModel>>(webRequest.downloadHandler.text);
                foreach(var player in data)
                {
                    var newText = Instantiate(textPrefab, scoresTextHolder.transform);
                    newText.GetComponent<TextMeshProUGUI>().text = $"{player.playerId} {player.killedEnemies}";
                }
            }
            else
            {
                print(webRequest.error);
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneConstants.MainMenuScreen);
    }
}
