using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PuzzleManager: MonoBehaviour
{
    //public Puzzle puzzlePrefab;

    public Timer timer;
    public InputField inputName;

    public int x_size, y_size;
    public int randomPasses = 12;
    int puzzlePieceSize = 650;
    public int QCount = 0;
    public int randomNum;

    public static int score;
    public static int indexStatis;

    public GameObject puzzlePiece;
    public GameObject Gameover;
    public GameObject Gameplay;
    GameObject sound;

    public Text ScoreTxt;
    public Text TotalScoreTxt;
    public Text QuestionCountTxt;

    Texture2D texture;
    public RawImage infoGame;

    private Puzzle[,] puzzle;
    private Puzzle puzzleSelection;

    public List<int> randomList = new List<int>();
    public Random a = new Random();
    public int currentQuestion;

    void Awake()
    {
        sound = GameObject.Find("backsound on");
        Destroy(sound);
        indexStatis = 0;

    }

    void Start()
    {
        Gameover.SetActive(false);
        infoGame.gameObject.SetActive(false);
        generatePuzzle();
    }

    void generatePuzzle()
    {
        QCount += 1;
        timer.time = 60;
        randomNum = PlayerPrefs.GetInt("jmlGambar");

        currentQuestion = Random.Range(1, randomNum);

        while (randomList.Contains<int>(currentQuestion))
        {
            currentQuestion = Random.Range(1, randomNum);
        }
        randomList.Add(currentQuestion);

        texture = Resources.Load("PuzzleImg/" + PlayerPrefs.GetString("lokasi") + "/" + currentQuestion) as Texture2D;
        infoGame.texture = texture;

        puzzlePiece.GetComponent<RawImage>().texture = texture;
        puzzlePiece.GetComponent<RectTransform>().sizeDelta = new Vector2(puzzlePieceSize, puzzlePieceSize);

        puzzle = new Puzzle[x_size, y_size];
        GameObject temp;

        for (int i = 0; i < x_size; i++)
        {
            for (int j = 0; j < y_size; j++)
            {
                temp = Instantiate(puzzlePiece, new Vector2(i * puzzlePieceSize / x_size, j * puzzlePieceSize / y_size), Quaternion.identity);
                temp.tag = "piece";
                temp.transform.SetParent(transform);
                puzzle[i, j] = temp.GetComponent<Puzzle>();
                puzzle[i, j].createPuzzlePiece(x_size, y_size);
            }
        }
        setupBoard();
        RandomizePlacement();
    }
    void setupBoard()
    {
        Vector2 offset;
        Vector2 scale = new Vector2(1f / x_size, 1f / y_size);
        for (int i = 0; i < x_size; i++)
        {
            for (int j = 0; j < y_size; j++)
            {
                offset = new Vector2(i * (1f / x_size), j * (1f / y_size));
                puzzle[i, j].assignImage(scale, offset);
            }
        }
    }

    void RandomizePlacement()
    {
        Vector2Int[] puzzleLocation = new Vector2Int[2];
        Vector2[] puzzleOffset = new Vector2[2];
        do
        {
            for(int i = 0; i < randomPasses; i++)
            {
                puzzleLocation[0].x = Random.Range(0, x_size);
                puzzleLocation[0].y = Random.Range(0, y_size);

                puzzleLocation[1].x = Random.Range(0, x_size);
                puzzleLocation[1].y = Random.Range(0, y_size);

                puzzleOffset[0] = puzzle[puzzleLocation[0].x, puzzleLocation[0].y].getImageOffset();
                puzzleOffset[1] = puzzle[puzzleLocation[1].x, puzzleLocation[1].y].getImageOffset();

                puzzle[puzzleLocation[0].x, puzzleLocation[0].y].assignImage(puzzleOffset[1]);
                puzzle[puzzleLocation[1].x, puzzleLocation[1].y].assignImage(puzzleOffset[0]);
            }
        } while (checkBoard()==true);
    }

    public bool checkBoard() {
        for (int i = 0; i < x_size; i++)
        {
            for (int j = 0; j < y_size; j++)
            {
                if (puzzle[i,j].checkGoodPlacement() == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void winGame()
    {
        infoGame.gameObject.SetActive(false);
        indexStatis++;
        if (indexStatis < 10)
        {
            GameObject[] deletePic = GameObject.FindGameObjectsWithTag("piece");
            for (int i = 0; i < deletePic.Length; i++)
            {
                Destroy(deletePic[i]);
            }
            generatePuzzle();
        }
        else
        {
            PlayerPrefs.SetInt("tmpScore", score);
            Gameplay.SetActive(false);
            Gameover.SetActive(true);
            TotalScoreTxt.text = "Score Anda: " + score;
        }
    }

    public void finish()
    {
        PlayerPrefs.SetString("tmpNama", inputName.text);
        SceneManager.LoadScene("ScoreBoard");
    }

    public Puzzle getSelection()
    {
        return puzzleSelection;
    }

    public void setSelection (Puzzle selection)
    {
        puzzleSelection = selection;
    }

    void Update()
    {
        ScoreTxt.text = score.ToString();
        QuestionCountTxt.text = (indexStatis + 1) + "/10";
    }
}
