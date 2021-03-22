using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSAuthentication : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    void Start()
    {
        if(platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
            Debug.Log("Play Games initialized");
        }

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (success) =>
        {
            switch (success)
            {
                case SignInStatus.Success:
                    Debug.Log("Play Games signed in succesfully");
                    break;
                default:
                    Debug.Log("Play Games signin not sucess");
                    break;

            }

        });
    }

}
