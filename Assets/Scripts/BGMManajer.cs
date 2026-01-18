using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioSource bgmSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SetVolume(float value)
    {
        bgmSource.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        bgmSource.volume = savedVolume;
    }
}
