using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text judulTxt;
    string lokasi;
    public GameObject exit_popup;

    public void setScene()
    {
        //if(judulTxt)
        switch (judulTxt.text)
        {
            case "Pekanbaru":
                lokasi = "Pekanbaru";
                PlayerPrefs.SetString("lokasi", lokasi);
                SceneManager.LoadScene("Pekanbaru"); break;
            case "Kampar":
                lokasi = "Kampar";
                PlayerPrefs.SetString("lokasi", lokasi); 
                SceneManager.LoadScene("Kampar"); break;
            case "Pelalawan":
                lokasi = "Pelalawan";
                PlayerPrefs.SetString("lokasi", lokasi); 
                SceneManager.LoadScene("Pelalawan"); break;
            case "Dumai":
                lokasi = "Dumai";
                PlayerPrefs.SetString("lokasi", lokasi);
                SceneManager.LoadScene("Dumai"); break;
            case "Rokan Hilir":
                lokasi = "Rohil";
                PlayerPrefs.SetString("lokasi", lokasi);
                SceneManager.LoadScene("Rohil"); break;
            case "Rokan Hulu":
                lokasi = "Rohul";
                PlayerPrefs.SetString("lokasi", lokasi);
                SceneManager.LoadScene("Rohul"); break;
            case "Siak":
                lokasi = "Siak";
                PlayerPrefs.SetString("lokasi", lokasi);
                SceneManager.LoadScene("Siak"); break;
            case "Kuansing":
                lokasi = "Kuansing";
                PlayerPrefs.SetString("lokasi", lokasi);
                SceneManager.LoadScene("Kuansing"); break;
            case "Bengkalis":
                lokasi = "Bengkalis";
                PlayerPrefs.SetString("lokasi", lokasi);
                PlayerPrefs.SetInt("jmlGambar", 16);
                SceneManager.LoadScene("Bengkalis"); break;
            case "Indragiri Hilir":
                lokasi = "Inhil";
                PlayerPrefs.SetString("lokasi", lokasi);
                PlayerPrefs.SetInt("jmlGambar", 12);
                SceneManager.LoadScene("Inhil"); break;
            case "Indragiri Hulu":
                lokasi = "Inhu";
                PlayerPrefs.SetString("lokasi", lokasi);
                PlayerPrefs.SetInt("jmlGambar", 12);
                SceneManager.LoadScene("Inhu"); break;
            case "Meranti":
                lokasi = "Meranti";
                PlayerPrefs.SetString("lokasi", lokasi);
                PlayerPrefs.SetInt("jmlGambar", 12);
                SceneManager.LoadScene("Meranti"); break;
        }
    }

    public void scene(string scene_name){
        SceneManager.LoadScene(scene_name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex==0)
            {
                exit_popup.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void exit(){
        Application.Quit();
    }

    public void sound_volume(float volume){
        PlayerPrefs.SetFloat("volume", volume); 
    }

    public void pressed(string name)
    {
        name = this.name;
    }
}
