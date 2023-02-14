using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //Static Class for save the current player data;
    
    //Singleton pattern

    public static MenuManager Instance { get; private set; } // add private setter

    public string PlayerName;
    public int Score;

    //Fields for display the player info

    public TMP_Text BestPlayerName;

    //Static variables for holding the best player data
    private static int BestScore;
    private static string BestPlayer;

    private void Awake()
    {
        // We dont actually need this if statement because when we are in the Main Scene we cant return to the Start Menu Scene where the Player Data Handle Object already exists.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayersData();
    }

    public void SavePlayersData()
    {
        SavePlayerData data = new SavePlayerData();
        data.TheBestPlayer = PlayerName;
        data.HighiestScore = Score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void SetBestPlayer()
    {
        if (BestPlayer == null && BestScore == 0)
        {
            BestPlayerName.text = "";
        }
        else
        {
            BestPlayerName.text = $"Лучший - {BestPlayer}: {BestScore}";
        }

    }

    public void LoadPlayersData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);

            BestPlayer = data.TheBestPlayer;
            BestScore = data.HighiestScore;

            SetBestPlayer();
        }
    }

    [System.Serializable]
    class SavePlayerData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }
}
