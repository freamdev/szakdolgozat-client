using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//using System.IdentityModel.Tokens.Jwt;

public class LoginScreenController : MonoBehaviour
{
    [SerializeField]
    TMP_InputField loginText;
    [SerializeField]
    TMP_InputField passwordText;

    LocalDataStore localStore;

    void Start()
    {
        localStore = GameObject.FindObjectOfType<LocalDataStore>();
    }

    public void Login()
    {
        StartCoroutine(LoginProcess());
    }

    IEnumerator LoginProcess()
    {
        var url = ServerConstants.BaseUrl;
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{url}/api/login?username={loginText.text}&password={passwordText.text}"))
        {
            yield return webRequest.SendWebRequest();

            if(webRequest.result == UnityWebRequest.Result.Success)
            {
                var resultdata = webRequest.downloadHandler.text;
                localStore.JwtToken = resultdata;
                localStore.IsOfflineMode = false;
                localStore.CurrentUser = loginText.text;
                GoMainMenu();
            }
            else
            {
                print(webRequest.error);
            }
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterProcess());
    }

    IEnumerator RegisterProcess()
    {
        var url = ServerConstants.BaseUrl;
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{url}/api/register?username={loginText.text}&password={passwordText.text}"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                StartCoroutine(LoginProcess());
            }
            else
            {
                print(webRequest.error);
            }
        }
    }

    public void Playoffline()
    {
        localStore.IsOfflineMode = true;
        GoMainMenu();
    }

    protected void GoMainMenu()
    {
        SceneManager.LoadScene(SceneConstants.MainMenuScreen);
    }
}
