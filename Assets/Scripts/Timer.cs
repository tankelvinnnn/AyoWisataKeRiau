using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    public Text Timertxt;
    public GameObject Timeover;
    public float time;
    float waitingTime = 2f;
    float timer;
    public QuizManager quizManager;
    public PicQuizManager picQuizManager;
    public PuzzleManager puzzleManager;

    [SerializeField]
    private AudioClip _countdown = null;
    private AudioSource _source = null;

    public bool active;

    float s;
    public bool timeOver;
    public string jenisGame;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (_source==null)
        {
            Debug.Log("Source null");
        }
        else
        {
            _source.clip = _countdown;
        }
        active = true;
        timeOver = false;
        Timeover.SetActive(false);
    }
    void setText()
    {
        int Menit = Mathf.FloorToInt(time / 60);
        int Detik = Mathf.FloorToInt(time % 60);
        Timertxt.text = Menit.ToString("00") + ":" + Detik.ToString("00");
    }
    
    public void playClip(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && timeOver == false)
        {
            Timeover.SetActive(false);
            setText();
            s += Time.deltaTime;
            if (s >= 1)
            {
                time--;
                s = 0;
            }
        }

        if (time == 11)
        {
            _source.PlayDelayed(0.1f);
        }

        if (time < 0)
        {
            timeOver = true;

            switch (jenisGame)
            {
                case "pertanyaan":
                    if (quizManager.options[0].GetComponent<AnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("A").GetComponent<Image>().color = Color.green;
                    }
                    else if (quizManager.options[1].GetComponent<AnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("B").GetComponent<Image>().color = Color.green;
                    }
                    else if (quizManager.options[2].GetComponent<AnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("C").GetComponent<Image>().color = Color.green;
                    }
                    else if (quizManager.options[3].GetComponent<AnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("D").GetComponent<Image>().color = Color.green;
                    }
                    break;
                case "tebakGambar":
                    if (picQuizManager.options[0].GetComponent<PicAnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("A").GetComponent<Image>().color = Color.green;
                    }
                    else if (picQuizManager.options[1].GetComponent<PicAnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("B").GetComponent<Image>().color = Color.green;
                    }
                    else if (picQuizManager.options[2].GetComponent<PicAnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("C").GetComponent<Image>().color = Color.green;
                    }
                    else if (picQuizManager.options[3].GetComponent<PicAnswerScript>().isCorrect == true && timeOver == true)
                    {
                        GameObject.Find("D").GetComponent<Image>().color = Color.green;
                    }
                    break;
                case "puzzle":
                    puzzleManager.infoGame.gameObject.SetActive(true);
                    break;
            }

            Timeover.SetActive(true);

            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0f;
                timeOver = false;
                Timeover.SetActive(false);
                

                switch (jenisGame)
                {
                    case "pertanyaan":
                        GameObject.Find("A").GetComponent<Image>().color = Color.white;
                        GameObject.Find("B").GetComponent<Image>().color = Color.white;
                        GameObject.Find("C").GetComponent<Image>().color = Color.white;
                        GameObject.Find("D").GetComponent<Image>().color = Color.white;
                        quizManager.wrong();
                        break;
                    case "tebakGambar":
                        GameObject.Find("A").GetComponent<Image>().color = Color.white;
                        GameObject.Find("B").GetComponent<Image>().color = Color.white;
                        GameObject.Find("C").GetComponent<Image>().color = Color.white;
                        GameObject.Find("D").GetComponent<Image>().color = Color.white;
                        picQuizManager.wrong();
                        break;
                    case "puzzle":
                        Puzzle.click = 1;
                        puzzleManager.winGame();
                        break;
                }
            }
        }   
    }
}
