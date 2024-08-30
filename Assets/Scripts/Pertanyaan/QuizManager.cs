using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;

    public Timer timer;
    public GameObject[] options;
    public int currentQuestion;
    public int QCount = 0;

    int totalQuestions = 0;
    public int score;
    public int questionCount = 0;

    public GameObject Gameplay;
    public GameObject Gameover;
    public GameObject Timeover;
    GameObject sound;

    public Text QuestionTxt;
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
        totalQuestions = QnA.Count;
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
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void Update()
    {
        QuestionCountTxt.text = QCount + "/10";
        ScoreTxt.text = score.ToString();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; 
            }
        }
    }

    void generateQuestion()
    {
        QCount += 1;

        if (QnA.Count > 0)
        {
            if (QCount > 10)
            {
                GameOver();
            }
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
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
        Timeover.SetActive(false);
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