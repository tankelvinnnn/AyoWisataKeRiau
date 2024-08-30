using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicAnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public PicQuizManager quizManager;

    public Color startColor;

    void Start()
    {
        startColor = GetComponent<Image>().color;
    }

    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            StartCoroutine(correct(1f));
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            if (quizManager.options[0].GetComponent<PicAnswerScript>().isCorrect == true)
            {
                GameObject.Find("A").GetComponent<Image>().color = Color.green;
            }
            else if (quizManager.options[1].GetComponent<PicAnswerScript>().isCorrect == true)
            {
                GameObject.Find("B").GetComponent<Image>().color = Color.green;
            }
            else if (quizManager.options[2].GetComponent<PicAnswerScript>().isCorrect == true)
            {
                GameObject.Find("C").GetComponent<Image>().color = Color.green;
            }
            else if (quizManager.options[3].GetComponent<PicAnswerScript>().isCorrect == true)
            {
                GameObject.Find("D").GetComponent<Image>().color = Color.green;
            }
            StartCoroutine(wrong(2f));
        }
    }

    IEnumerator correct(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Image>().color = startColor;
        quizManager.correct();
    }

    IEnumerator wrong(float time)
    {
        yield return new WaitForSeconds(time);
        //GetComponent<Image>().color = startColor;
        GameObject.Find("A").GetComponent<Image>().color = Color.white;
        GameObject.Find("B").GetComponent<Image>().color = Color.white;
        GameObject.Find("C").GetComponent<Image>().color = Color.white;
        GameObject.Find("D").GetComponent<Image>().color = Color.white;
        quizManager.wrong();
    }
}
