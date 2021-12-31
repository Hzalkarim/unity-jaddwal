using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Team activeTeam;

    [Space]
    private RectTransform playPiece;
    public RectTransform playPiecePrefab;

    [Space]
    public int valueToSet = 8;
    public int redScore = 0;
    public int blueScore = 0;
    public int greenScore = 0;

    [Space]
    public Text redScoreText;
    public Text blueScoreText;
    public Text greenScoreText;

    public Text valueToSetText;

    [Space]
    public TurnButton[] buttons;
    public Board board;
    
    [Space]
    public List<Scheduler> schedulers;
    public Scheduler blueSchedulerPrefab;
    public Scheduler greenSchedulerPrefab;

    private void Start()
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => SetActiveTeam(button.myTeam));
        }

        schedulers = new List<Scheduler>();

        board.FillBoard(OnClickCell);
        int center = (board.size * board.size) / 2;

        var newPiece = Instantiate(playPiecePrefab);
        playPiece = newPiece;

        var cellCenter = board.cells[center];
        board.cells[center].AddPiece(newPiece);

        Debug.Log($"{newPiece}: {center}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeProgress();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            valueToSet = 2;
            valueToSetText.text = valueToSet.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            valueToSet = 4;
            valueToSetText.text = valueToSet.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            valueToSet = 8;
            valueToSetText.text = valueToSet.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            valueToSet = -1;
            valueToSetText.text = valueToSet.ToString();
        }
        //else if (Input.GetKeyDown(KeyCode.W))
        //{
        //    valueToSet = -2;
        //    valueToSetText.text = valueToSet.ToString();
        //}
    }

    public void SetActiveTeam(Team team)
    {
        activeTeam = team;
        Debug.Log($"Active: {team}");
    }

    public void OnClickCell(Cell cell)
    {
        if (cell.IsOccupied)
        {
            if (activeTeam == Team.Red)
            {
                RedGetScheduler(cell);
            }
            else if(valueToSet < 0)
            {
                cell.GetComponentInChildren<Scheduler>().DecreaseCurrentValue(valueToSet);
            }
        }
        else
        {
            AddPiece(cell);
        }
    }

    private void AddPiece(Cell cell)
    {
        Scheduler sch;
        switch (activeTeam)
        {
            case Team.Red:
                cell.AddPiece(playPiece);
                break;
            case Team.Green:
                sch = InstantiateScheduler(greenSchedulerPrefab);
                cell.AddPiece(sch.GetComponent<RectTransform>());
                break;
            case Team.Blue:
                sch = InstantiateScheduler(blueSchedulerPrefab);
                cell.AddPiece(sch.GetComponent<RectTransform>());
                break;
            default:
                break;
        }
    }

    private void RedGetScheduler(Cell cell)
    {
        var sch = cell.GetComponentInChildren<Scheduler>();
        int score = sch.GetFinalValue();

        if (score > sch.Value)
        {
            switch (sch.team)
            {
                case Team.Green:
                    greenScore += sch.Value;
                    break;
                case Team.Blue:
                    blueScore += sch.Value;
                    break;
                default:
                    break;
            }
        }

        redScore += score;
        Destroy(sch.gameObject);
        cell.AddPiece(playPiece);

        UpdateScoreText();
    }

    public Scheduler InstantiateScheduler(Scheduler schedulerPrefab)
    {
        var scheduler = Instantiate(schedulerPrefab);
        schedulers.Add(scheduler);
        scheduler.Value = valueToSet;
        return scheduler;
    }

    public void TimeProgress()
    {
        foreach (var sch in schedulers)
        {
            if (sch != null)
            {
                sch.TimeProgress();
                switch (sch.team)
                {
                    case Team.Green:
                        greenScore++;
                        break;
                    case Team.Blue:
                        blueScore++;
                        break;
                    default:
                        break;
                }
            }
        }
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        redScoreText.text = redScore.ToString();
        blueScoreText.text = blueScore.ToString();
        greenScoreText.text = greenScore.ToString();
    }
}

public enum Team
{
    Red, Green, Blue
}
