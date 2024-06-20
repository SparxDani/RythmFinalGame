using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CircularMenu : MonoBehaviour
{
    public SongsSO[] songsArray;
    public TextMeshProUGUI menuItemTemplate;
    private CircularDoublyLinkedList<SongsSO> songsList = new CircularDoublyLinkedList<SongsSO>();
    private CircularDoublyLinkedList<TextMeshProUGUI> menuItemsList = new CircularDoublyLinkedList<TextMeshProUGUI>();
    public int centerIndex = 3;
    public float spacing = 30f;
    public float horizontalPos = -170f;
    public Color blinkColor1 = Color.gray;
    public Color blinkColor2 = Color.white;
    public float blinkInterval = 0.5f;

    private Coroutine blinkCoroutine;

    public SongDetailsDisplay songDetailsDisplay;

    private SongsSO selectedSong;

    void Start()
    {
        for (int i = 0; i < songsArray.Length; i++)
        {
            songsList.Add(songsArray[i]);
        }

        for (int i = 0; i < songsArray.Length; i++)
        {
            TextMeshProUGUI menuItem = Instantiate(menuItemTemplate, menuItemTemplate.transform.parent);
            menuItem.gameObject.SetActive(true);
            menuItemsList.Add(menuItem);
        }

        UpdateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        SongsSO lastSong = songsList.Get(songsList.Count - 1);
        songsList.RemoveAt(songsList.Count - 1);
        songsList.Insert(0, lastSong);
        UpdateMenu();
    }

    void MoveDown()
    {
        SongsSO firstSong = songsList.Get(0);
        songsList.RemoveAt(0);
        songsList.Add(firstSong);
        UpdateMenu();
    }

    void UpdateMenu()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        for (int i = 0; i < menuItemsList.Count; i++)
        {
            if (i < songsList.Count)
            {
                menuItemsList.Get(i).text = songsList.Get(i).musicTitle;

                if (i == centerIndex)
                {
                    selectedSong = songsList.Get(i);
                    blinkCoroutine = StartCoroutine(Blink(menuItemsList.Get(i)));
                    songDetailsDisplay.UpdateSongDetails(songsList.Get(i));
                }
                else
                {
                    menuItemsList.Get(i).color = blinkColor2;
                }
            }

            RectTransform rectTransform = menuItemsList.Get(i).GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(horizontalPos, (centerIndex - i) * spacing);
        }
    }

    IEnumerator Blink(TextMeshProUGUI menuItem)
    {
        bool toggle = false;
        while (true)
        {
            menuItem.color = toggle ? blinkColor1 : blinkColor2;
            toggle = !toggle;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void PlaySelectedSong()
    {
        if (selectedSong != null)
        {
            GameData.AudioClip = selectedSong.audioClip;
            GameData.MidiFileName = selectedSong.midiNameFile;
            SceneManager.LoadScene("GameMusic");
        }
    }
}
