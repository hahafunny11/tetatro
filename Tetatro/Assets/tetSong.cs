using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class tetSong : MonoBehaviour
{
    public AudioSource tet1;
    public AudioSource tet2;
    public AudioSource tetboss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        tet1 = audioSource[0];
        tet2 = audioSource[1];
        tetboss = audioSource[2];
        switch (GlobalVariables.currentLevel % 3)
        {
            case 0:
                tet1.Play();
                break;

            case 1:
                tet2.Play();
                break;

            case 2:
                tetboss.Play();
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
