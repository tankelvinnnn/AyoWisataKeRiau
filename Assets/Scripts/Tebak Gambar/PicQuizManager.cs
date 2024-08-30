using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PicQuizManager : MonoBehaviour
{
    public List<PicQuestionAndAnswer> PicQnA;

    public Timer timer;
    public GameObject[] options;
    public int currentQuestion;
    public int QCount = 0;

    int totalQuestions = 0;
    public int score;
    public int questionCount = 0;

    public GameObject Gameplay;
    public GameObject Gameover;
    GameObject sound;

    public Image QuestionPic;
    public Text ScoreTxt;
    public Text TotalScoreTxt;
    public Text QuestionCountTxt;

    public InputField inputName;

    private void Awake()
    {
        sound = GameObject.Find("backsound on");
        Destroy(sound);
    }
    void Start()
    {
        Gameover.SetActive(false);
        totalQuestions = PicQnA.Count;
        generateQuestion();
    }

    public void finish()
    {
        PlayerPrefs.SetString("tmpNama", inputName.text);
        SceneManager.LoadScene("ScoreBoard");
    }
    void GameOver()
    {
        PlayerPrefs.SetInt("tmpScore", score);
        StartCoroutine(GameOver(0f));
    }
    
    public void correct()
    {
        score += 10;
        PicQnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        PicQnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void Update()
    {
        QuestionCountTxt.text = questionCount + "/10";
        ScoreTxt.text = score.ToString();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<PicAnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = PicQnA[currentQuestion].Answers[i];

            if (PicQnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<PicAnswerScript>().isCorrect = true; 
            }
        }
    }

    void generateQuestion()
    {
        QCount += 1;

        if (QCount > 10)
        {
            GameOver();
        }
        if (PicQnA.Count > 0)
        {
            currentQuestion = Random.Range(0, PicQnA.Count);
            QuestionPic.sprite = PicQnA[currentQuestion].Question;
            SetAnswer();
            timer.time = 30;
            questionCount += 1;
        }
        else
        {
            GameOver();
        } 
    }
    IEnumerator GameOver(float time)
    {
        yield return new WaitForSeconds(time);
        Gameplay.SetActive(false);
        Gameover.SetActive(true);
        TotalScoreTxt.text = "Score Anda: " + score;
    }

    IEnumerator skip(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("A").GetComponent<Image>().color = Color.white;
        GameObject.Find("B").GetComponent<Image>().color = Color.white;
        GameObject.Find("C").GetComponent<Image>().color = Color.white;
        GameObject.Find("D").GetComponent<Image>().color = Color.white;
    }
}