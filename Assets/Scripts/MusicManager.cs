using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance = null;
    
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!source.isPlaying)
            source.Play();
    }
}
