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
    public Image songImage;

    public void UpdateSongDetails(SongsSO song)
    {
        artistText.text = song.musicArtist;
        difficultyText.text = song.dificult;
        durationText.text = song.musicDuration;
        songImage.sprite = song.musicImage;
    }
}
