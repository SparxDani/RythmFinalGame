using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongDetailsDisplay : MonoBehaviour
{
    public TextMeshProUGUI artistText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI gameText;
    public TextMeshProUGUI[] scoreTexts;

    public Image songImage;

    public void UpdateScores(int[] scores)
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < scores.Length)
            {
                scoreTexts[i].text = scores[i].ToString();
            }
            else
            {
                scoreTexts[i].text = "";
            }
        }
    }
    public void UpdateSongDetails(SongsSO song)
    {
        artistText.text = song.musicArtist;
        difficultyText.text = song.dificult;
        gameText.text = song.musicGame;
        durationText.text = song.musicDuration;
        songImage.sprite = song.musicImage;
    }
}
