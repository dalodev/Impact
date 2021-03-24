﻿using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class GPGSAuthentication : MonoBehaviour
{
    public const string LEADERBOARD_ID = "CgkInYaFxIAdEAIQAg";
    public static PlayGamesPlatform platform;
    public bool isSaving = false;
    private string SAVE_NAME = "savegaames";
    public static GPGSAuthentication instance;

    public bool dataLoaded = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if(platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestServerAuthCode(false)
                .EnableSavedGames()
                .Build();
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
                    PlayerData data = SaveSystem.LoadPlayerData();
                    if(data != null)
                    {
                        if (!data.loadedFromCloud)
                        {
                            GPGSAuthentication.instance.OpenSaveToCloud(false);
                        }
                    }
                    else
                    {
                        GPGSAuthentication.instance.OpenSaveToCloud(false);
                    }
                    break;
                default:
                    Debug.Log("Play Games signin not sucess");
                    break;
            }
        });
    }

    public void SubmitScoreToLeaderboard(int score)
    {
        Social.ReportScore(score, LEADERBOARD_ID, (bool success) => {
            if (success)
            {
                Debug.Log("succesfully add score to leaderboard");
            }
            else
            {
                Debug.Log("Fail to add score to leaderboard");
            }
        });
    }

    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

    public void AchievementCompleted(string achievement = Achievements.PIMP_MY_BALL)
    {
        Social.ReportProgress(achievement, 100.0f, (bool success) => {
            if (success)
            {
                Debug.Log("Succesfully achievement complete " + achievement);
            }
            else
            {
                Debug.Log("Failed to complete achievement " + achievement);
            }
        });
    }

    public void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    //cloud saving
    public void OpenSaveToCloud(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution
                (SAVE_NAME, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, SavedGameOpen);
        }
    }

    private void SavedGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if(status== SavedGameRequestStatus.Success)
        {
            if (isSaving)//if is saving is truie we are saving our data to cloud
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GetDataToStore());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
            }
            else //if is saving is false we are opening our saved data from cloud
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, ReadDataFromCloud);
            }
        }
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            //use this to debug wether the game is uploaded to cloud
            Debug.Log("Successfully add to cloud");
        }
    }

    private void ReadDataFromCloud(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            string saveData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            LoadDataFromCloud(saveData);
        }
    }

    private void LoadDataFromCloud(string savedata)
    {
        string[] data = savedata.Split('|');

        Debug.Log("data from cloud: " + string.Join(", ", data));
        string skin = data[(int)CustomizeData.CustomizeIndex.CUSTOMIZE_SKIN] != "null" ? data[(int)CustomizeData.CustomizeIndex.CUSTOMIZE_SKIN] : null;
        string trail = data[(int)CustomizeData.CustomizeIndex.CUSTOMIZE_TRAIL] != "null" ? data[(int)CustomizeData.CustomizeIndex.CUSTOMIZE_TRAIL] : null;
        if(skin != null && trail != null)
        {
            SaveSystem.SaveCustomize(skin, trail);
        }

        int coins = int.Parse(data[(int)PlayerData.PlayerIndex.PLAYER_COINS]);
        int highscore = int.Parse(data[(int)PlayerData.PlayerIndex.PLAYER_HIGHSCORE]);
        SaveSystem.SavePlayerData(coins, highscore, true);

        string[] dataItems = data[(int)UpgradesData.UpgradesIndex.UPGRADES_ITEMS] != "null" ? data[(int)UpgradesData.UpgradesIndex.UPGRADES_ITEMS].Split(',') : null;
        if(dataItems != null)
        {
            int[] items = Array.ConvertAll(dataItems, s => int.Parse(s));
            int multiplier = int.Parse(data[(int)UpgradesData.UpgradesIndex.UPGRADES_ITEMS]);
            SaveSystem.SaveUpgrades(items, multiplier);
        }

        int level = int.Parse(data[(int)LevelData.LevelIndex.LEVEL_LEVEL]);
        SaveSystem.SaveLevelData(level);
        dataLoaded = true;
    }

    private string GetDataToStore() // we seting the value that we are going to store the data in cloud
    {
        CustomizeData customize = SaveSystem.LoadCustomize();
        PlayerData player = SaveSystem.LoadPlayerData();
        UpgradesData upgrades = SaveSystem.LoadUpgrades();
        LevelData level = SaveSystem.LoadLevelData();

        string data = "";

        data += customize != null ? customize.skin : "null";
        data += "|";
        data += customize != null ? customize.trail : "null";
        data += "|";
        data += player != null ? player.coins : 0;
        data += "|";
        data += player != null ? player.highScore : 0;
        data += "|";
        data += upgrades != null ? string.Join(",", upgrades.items) : "null";
        data += "|";
        data += upgrades != null ? upgrades.levelUpMultipler : 1;
        data += "|";
        data += level != null ? level.level : 0;

        return data;
    }
}
