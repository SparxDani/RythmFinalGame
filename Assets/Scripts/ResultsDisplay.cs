using PriorityQueue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsDisplay : MonoBehaviour
{
    public TextMeshProUGUI songNameText;
    //public TextMeshProUGUI usernameText;
    //public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI[] missedNotesText;
    //public TextMeshProUGUI hitedNotesText;
    public TextMeshProUGUI[] okNotesText;
    public TextMeshProUGUI[] goodNotesText;
    public TextMeshProUGUI[] greatNotesText;
    public TextMeshProUGUI[] excellentNotesText;
    public TextMeshProUGUI[] maxComboText;
    public TextMeshProUGUI finalScoreText;

    private CustomPriorityQueue priorityQueue;

    void Start()
    {
        GameData.finalScore = GameData.CalculateFinalScore();
        priorityQueue = new CustomPriorityQueue();

        songNameText.text = GameData.songName;
        //usernameText.text = "Username: " + GameData.Username + " Results:";
        //totalScoreText.text = "Score: " + GameData.TotalScore.ToString();

        for (int i = 0; i < missedNotesText.Length; i++)
        {
            missedNotesText[i].text = GameData.MissedNotes.ToString();
        }

        //hitedNotesText.text = "Successful notes: " + GameData.HitedNotes.ToString();

        for (int i = 0; i < okNotesText.Length; i++)
        {
            okNotesText[i].text = GameData.OkNotes.ToString();
        }

        for (int i = 0; i < goodNotesText.Length; i++)
        {
            goodNotesText[i].text = GameData.GoodNotes.ToString();
        }

        for (int i = 0; i < greatNotesText.Length; i++)
        {
            greatNotesText[i].text = GameData.GreatNotes.ToString();
        }

        for (int i = 0; i < excellentNotesText.Length; i++)
        {
            excellentNotesText[i].text = GameData.ExcellentNotes.ToString();
        }

        for (int i = 0; i < maxComboText.Length; i++)
        {
            maxComboText[i].text = $"MAX COMBO  {GameData.ComboScore.ToString()}";
        }

        finalScoreText.text = GameData.finalScore.ToString();
    }

    public void SendScoreAndReturnToMenu()
    {
        priorityQueue.PriorityEnqueue(GameData.finalScore);
        GameData.topScores = priorityQueue.GetTop5Scores();
        if (GameData.currentSongData != null)
        {
            GameData.currentSongData.top5Scores = GameData.topScores;
        }
        SceneManager.LoadScene("MainScene");
    }
}
