using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    Vector2 m_goodOffset;
    Vector2 m_offset;
    Vector2 m_scale;

    PuzzleManager puzzleManager;
    public static int click = 0;

    void Start()
    {
        click = 0;
        puzzleManager = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<PuzzleManager>();
    }

    public void createPuzzlePiece(int x, int y)
    {
        transform.localScale = new Vector3(1.0f * transform.localScale.x / x, 1.0f * transform.localScale.z / y, 1);
    }

    public void assignImage(Vector2 scale, Vector2 offset)
    {
        m_goodOffset = offset;
        m_scale = scale;
        assignImage(offset);
    }

    public void assignImage(Vector2 offset)
    {
        m_offset = offset;
        GetComponent<RawImage>().uvRect = new Rect(offset.x, offset.y, m_scale.x, m_scale.y); 
    }

    public Vector2 getImageOffset()
    {
        return m_offset;
    }

    public bool checkGoodPlacement()
    {
        return (m_goodOffset == m_offset);
    }

    public void onClick()
    {
        if (click==0)
        {
            Puzzle previousSelection = puzzleManager.getSelection();
            if (previousSelection != null)
            {
                previousSelection.GetComponent<RawImage>().color = Color.white;
                Vector2 tempOffset = previousSelection.getImageOffset();
                previousSelection.assignImage(m_offset);
                assignImage(tempOffset);
                puzzleManager.setSelection(null);

                if (puzzleManager.checkBoard())
                {
                    PuzzleManager.score += 10;
                    //PlayerPrefs.SetInt("tempScore", PuzzleManager.score);
                    StartCoroutine(wait(2f));
                }
            }
            else
            {
                GetComponent<RawImage>().color = Color.gray;
                puzzleManager.setSelection(this);
            }
        }
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        puzzleManager.winGame();
    }
}
