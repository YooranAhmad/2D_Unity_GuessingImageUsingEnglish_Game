using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // untuk restart scene

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class PictureData
    {
        public string englishWord;
        public Sprite image;
    }

    [Header("Game Data")]
    public List<PictureData> pictures = new List<PictureData>();

    [Header("UI References")]
    public Image pictureDisplay;
    public InputField answerInput;
    public Text scoreText;
    public MessageAnimator messageAnimator;
    public Image finishImage;
    public Button restartButton; // 🔁 tombol restart

    [Header("Icons")]
    public Sprite correctIcon;
    public Sprite wrongIcon;

    public Button mainMenuButton; // 👈 tambahkan ini


    private int score = 0;
    private int currentIndex = 0;

    void Start()
    {
        scoreText.text = "0";
        if (finishImage != null)
            finishImage.enabled = false;
        if (restartButton != null)
            restartButton.gameObject.SetActive(false); // tombol restart disembunyikan di awal

        ShowPicture();
    }

    public void CheckAnswer()
    {
        if (currentIndex >= pictures.Count) return;

        string playerAnswer = answerInput.text.Trim().ToLower();
        string correctAnswer = pictures[currentIndex].englishWord.ToLower();

        bool isCorrect = playerAnswer == correctAnswer;

        if (isCorrect)
        {
            score++;
            scoreText.text = "" + score;
        }

        messageAnimator.Show(isCorrect ? correctIcon : wrongIcon, 1f);
        answerInput.text = "";

        Invoke(nameof(NextPicture), 1f);
    }

    void ShowPicture()
    {
        pictureDisplay.sprite = pictures[currentIndex].image;
    }

    void NextPicture()
    {
        currentIndex++;
        if (currentIndex >= pictures.Count)
        {
            ShowFinishScreen();
        }
        else
        {
            ShowPicture();
        }
    }

    void ShowFinishScreen()
    {
        pictureDisplay.enabled = false;
        answerInput.gameObject.SetActive(false);

        if (finishImage != null)
        {
            finishImage.gameObject.SetActive(true);
            var cg = finishImage.GetComponent<CanvasGroup>();
            if (cg != null) cg.alpha = 1f;
            finishImage.enabled = true;
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
            var cg = restartButton.GetComponent<CanvasGroup>();
            if (cg != null) cg.alpha = 1f;
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(true);
            var cg = mainMenuButton.GetComponent<CanvasGroup>();
            if (cg != null) cg.alpha = 1f;
        }

        Debug.Log("🎉 Semua soal selesai! Skor akhir: " + score);
    }


    // 🔁 Fungsi Restart Game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

}
