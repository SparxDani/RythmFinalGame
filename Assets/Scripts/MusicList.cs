using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicList : MonoBehaviour
{
    private CircularDoublyLinkedList<SongsSO> songs = new CircularDoublyLinkedList<SongsSO>();
    private DoubleNode<SongsSO> currentSongNode;
    public TextMeshProUGUI songTitleText;
    public SongsSO[] songDataArray;

    void Start()
    {
        for (int i = 0; i < songDataArray.Length; i++)
        {
            songs.Add(songDataArray[i]);
        }
        currentSongNode = songs.Head;
        DisplayCurrentSong();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentSongNode = currentSongNode.Previous;
            DisplayCurrentSong();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentSongNode = currentSongNode.Next;
            DisplayCurrentSong();
        }
    }

    void DisplayCurrentSong()
    {
        songTitleText.text = "Current Song: " + currentSongNode.Data.title;
        Debug.Log("Current Song: " + currentSongNode.Data.title);
    }
}
