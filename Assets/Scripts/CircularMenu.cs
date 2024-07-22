using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CircularMenu : MonoBehaviour
{
    public List<SongsSO> songsList = new List<SongsSO>();
    public TextMeshProUGUI menuItemTemplate;
    private List<TextMeshProUGUI> menuItemsList = new List<TextMeshProUGUI>();
    public int centerIndex = 3;
    public float spacing = 30f;
    public float horizontalPos = -170f;
    public Color blinkColor1 = Color.gray;
    public Color blinkColor2 = Color.white;
    public float blinkInterval = 0.5f;

    private Coroutine blinkCoroutine;

    public SongDetailsDisplay songDetailsDisplay;
    public SongDetailsDisplay scoreDisplay;

    private SongsSO selectedSong;

    private List<SongsSO> originalOrder = new List<SongsSO>();

    public AudioClip moveSoundEffect;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = moveSoundEffect;

        originalOrder.AddRange(songsList);

        for (int i = 0; i < songsList.Count; i++)
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
        SongsSO lastSong = songsList[songsList.Count - 1];
        songsList.RemoveAt(songsList.Count - 1);
        songsList.Insert(0, lastSong);
        UpdateMenu();
        PlayMoveSound();
    }

    void MoveDown()
    {
        SongsSO firstSong = songsList[0];
        songsList.RemoveAt(0);
        songsList.Add(firstSong);
        UpdateMenu();
        PlayMoveSound();
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
                SongsSO currentSong = songsList[i];
                TextMeshProUGUI menuItem = menuItemsList[i];
                bool isCenterIndex = (i == centerIndex);

                menuItem.text = currentSong.musicTitle;
                menuItem.color = isCenterIndex ? blinkColor1 : blinkColor2;

                RectTransform rectTransform = menuItem.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(horizontalPos, (centerIndex - i) * spacing);

                if (isCenterIndex)
                {
                    selectedSong = currentSong;
                    blinkCoroutine = StartCoroutine(Blink(menuItem));
                    songDetailsDisplay.UpdateSongDetails(currentSong);
                    scoreDisplay.UpdateScores(currentSong.top5Scores);
                }
            }
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
            GameData.songName = selectedSong.musicTitle;
            GameData.topScores = selectedSong.top5Scores;
            GameData.currentSongData = selectedSong;
            SceneTransitionController.Instance.FadeToScene("GameMusic");
        }
    }

    public void SortByDuration()
    {
        for (int i = 1; i < songsList.Count; i++)
        {
            SongsSO key = songsList[i];
            int j = i - 1;

            while (j >= 0 && songsList[j].musicDurationInSeconds > key.musicDurationInSeconds)
            {
                songsList[j + 1] = songsList[j];
                j--;
            }
            songsList[j + 1] = key;
        }

        UpdateMenu();
    }

    public void RestoreOriginalOrder()
    {
        songsList.Clear();
        songsList.AddRange(originalOrder);

        UpdateMenu();
    }

    private void PlayMoveSound()
    {
        if (audioSource != null && moveSoundEffect != null)
        {
            audioSource.PlayOneShot(moveSoundEffect);
        }
    }
}
