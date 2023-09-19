using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        public enum State
    {
        Ready,
        Play,
        End
    }




    public GameObject ObstacleObj;

    public brid Flappy;
    public float ObstacleSpeed;

    private List<GameObject> obstacleList = new List<GameObject>();
    private List<BoxCollider2D> obstacleColliderList = new List<BoxCollider2D>();
    private BoxCollider2D flappyCollider;

    private Vector3 startObstaclePos;
    private UIManager uiManager;

    private int curScore;
    private int bestScore;

    private State curState;
    private const int MaxObstacleCount = 6;
    private const float MinObstacleX = -1;
    private const float MaxObstacleY = 0.15f;
    private const float ObstacleInterval = 0.75f;


    public void Awake()
    {
        uiManager = GetComponent<UIManager>();
        flappyCollider = Flappy.GetComponent<BoxCollider2D>();
    }
    public void Start()
    {

        startObstaclePos = new Vector3(3, 0, 0);

        for (int i = 0; i < MaxObstacleCount; i++)
        {
            GameObject copy = GameObject.Instantiate(ObstacleObj);
            BoxCollider2D[] colliders = copy.GetComponentsInChildren<BoxCollider2D>();

            obstacleList.Add(copy);
            obstacleColliderList.AddRange(colliders);
        }
        InitGame();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(curState)
            {
                case State.Ready:
                    ChangeState(State.Play);
                    break;
                case State.Play:
                    Flappy.Jump();
                    break;
                case State.End:
                    InitGame();
                    break;

            }
           
        }


        if(curState == State.Play || curState == State.End)
        {
            Flappy.fly();
        }
        if (curState == State.Play)
        {
            MoveObstacle();
            CheckGameOver();
        }

    }

    public void InitGame()
    {
        Flappy.Init();
        SetScore(0);
        ChangeState(State.Ready);
        
        for (int i = 0; i < MaxObstacleCount; i++)
        {
            Transform tr = obstacleList[i].transform;
            float x = ObstacleInterval * i;
            float y = Random.Range(-MaxObstacleY, MaxObstacleY);
            Vector3 position = startObstaclePos + new Vector3(x, y, 0);

            tr.localPosition = position;
        }



    }

    public void MoveObstacle()
    {
        for(int i=0; i<MaxObstacleCount; i++)
        {

            Transform tr = obstacleList[i].transform;

            Vector3 prevPosition=tr.localPosition;
            Vector3 nextPosition = tr.localPosition + Vector3.left * ObstacleSpeed *Time.deltaTime;

            if (nextPosition.x <MinObstacleX)
            {
                nextPosition.x += ObstacleInterval * MaxObstacleCount;
                nextPosition.y = Random.Range(-MaxObstacleY, MaxObstacleY);
            }
            if(prevPosition.x >0&& nextPosition.x<=0)
            {
                SetScore(curScore + 1);
            }


            tr.localPosition = nextPosition;    
        }
    }
    public void CheckGameOver()
    {
        if(curState == State.Play)
        {
            for(int i = 0;i<obstacleColliderList.Count;i++)
            {
                BoxCollider2D boxCollider = obstacleColliderList[i];
                Bounds bounds = boxCollider.bounds;

                if(bounds.Intersects(flappyCollider.bounds))
                {
                    onGameover();
                    return;
                }
            }

            if (Flappy.IsBelowFloor())
            {
                onGameover();
                
            }
        }
    }

    public void onGameover()
    {
        ChangeState(State.End);
        uiManager.ShowResultMenu(curScore,bestScore);
    }




    public void ChangeState(State state)
    {
        curState = state;
        uiManager.ChangeState(state);
    }

    public void SetScore(int score)
    {
        curScore = score;

        if(score >bestScore)
        {
            bestScore = curScore;
             
        }
        uiManager.ChangeScore(score);
    }
    
}
