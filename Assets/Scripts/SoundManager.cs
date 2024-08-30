using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;

        if (GameObject.Find("backsound on") == null)
        {  
            DontDestroyOnLoad(gameObject);
            GetComponent<AudioSource>().Play();
            gameObject.name = "backsound on";
            PlayerPrefs.SetFloat("volume",0.6f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
    }
}
