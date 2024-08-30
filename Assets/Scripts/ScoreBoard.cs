using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    //public InputField inputName;
    public QuizManager qm;
    public GameObject scoreBoard;
    public Text txtScore1, txtScore2, txtScore3, txtScore4, txtScore5;
    public Text txtNama1, txtNama2, txtNama3, txtNama4, txtNama5;

    int tmpScore;
    string lokasi, tmpNama;

    int[] score = new int[5];
    string[] nama = new string[5];

    void Start()
    {
        int s = PlayerPrefs.GetInt("tmpScore");
        string t = PlayerPrefs.GetString("tmpNama");

        lokasi = PlayerPrefs.GetString("lokasi");
        tmpScore = PlayerPrefs.GetInt("tmpScore");
        tmpNama = PlayerPrefs.GetString("tmpNama");

        saveScore();
    }

    public void saveScore()
    {
        bool flag = false;

        for (int i = 0; i < score.Length; i++)
        {
            if (PlayerPrefs.GetInt("score_" + lokasi + "_" + i) == 0)
            {
                PlayerPrefs.SetInt("score_" + lokasi + "_" + i, tmpScore);
                PlayerPrefs.SetString("nama_" + lokasi + "_" + i, tmpNama);
                flag = true;
            }
            score[i] = PlayerPrefs.GetInt("score_" + lokasi + "_" + i);
            nama[i] = PlayerPrefs.GetString("nama_" + lokasi + "_" + i);
            sortScore();
            break;
        }
        for (int i = 0; i < score.Length; i++)
        {
            
        }

        if (!flag)
        {
            for (int i = 0; i < score.Length; i++)
            {
                score[i]= PlayerPrefs.GetInt("score_" + lokasi + "_" + i);
                nama[i] = PlayerPrefs.GetString("nama_" + lokasi + "_" + i);
            }

            if (tmpScore > score[4])
            {
                score[4] = tmpScore;
                nama[4] = tmpNama;
            }

            sortScore();

            for (int i = 0; i < score.Length; i++)
            {
                PlayerPrefs.SetInt("score_" + lokasi + "_" + i, score[i]);
                PlayerPrefs.SetString("nama_" + lokasi + "_" + i, nama[i]);
            }
        }

        /*PlayerPrefs.Save();*/
    }

    void sortScore()
    {
        for (int i = 0; i < score.Length; i++)
        {
            for (int j = i + 1; j < score.Length; j++)
            {
                if (score[j] > score[i])
                {
                    int tmp = score[i];
                    string tmpNama = nama[i];
                    score[i] = score[j];
                    score[j] = tmp;
                    nama[i] = nama[j];
                    nama[j] = tmpNama;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        txtScore1.text = PlayerPrefs.GetInt("score_" + lokasi + "_0").ToString();
        txtScore2.text = PlayerPrefs.GetInt("score_" + lokasi + "_1").ToString();
        txtScore3.text = PlayerPrefs.GetInt("score_" + lokasi + "_2").ToString();
        txtScore4.text = PlayerPrefs.GetInt("score_" + lokasi + "_3").ToString();
        txtScore5.text = PlayerPrefs.GetInt("score_" + lokasi + "_4").ToString();

        txtNama1.text = PlayerPrefs.GetString("nama_" + lokasi + "_0");
        txtNama2.text = PlayerPrefs.GetString("nama_" + lokasi + "_1");
        txtNama3.text = PlayerPrefs.GetString("nama_" + lokasi + "_2");
        txtNama4.text = PlayerPrefs.GetString("nama_" + lokasi + "_3");
        txtNama5.text = PlayerPrefs.GetString("nama_" + lokasi + "_4");
    }
}
