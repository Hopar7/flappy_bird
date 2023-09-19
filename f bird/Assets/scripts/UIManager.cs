using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum SpriteType
    {
        Number_0,Number_1, Number_2, Number_3, Number_4, Number_5, Number_6, Number_7, Number_8, Number_9,
        Gold_Medal,Silver_Medal,
    }



    public GameObject ReadyObj;
    public GameObject PlayObj;
    public GameObject EndObj;

    public List<Sprite> UISprites;


    public Image NumBerImage1;
    public Image NumBerImage2;

    public Image MedlaImage;
    public Image[] ResultNumbers;

    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void ChangeState(GameManager.State state)
    {
        switch (state)
        {
            case GameManager.State.Ready:
                ReadyObj.SetActive(true);
                PlayObj.SetActive(false);
                EndObj.SetActive(false);

                break;
            case GameManager.State.Play:
                ReadyObj.SetActive(false);
                PlayObj.SetActive(true);
                EndObj.SetActive(false);
                break;
            case GameManager.State.End:
                ReadyObj.SetActive(false);
                PlayObj.SetActive(false);
                EndObj.SetActive(true);
                break;
        }
    }


    public void ChangeScore(int  score)
    {
        NumBerImage1.sprite = UISprites[score / 10];
        NumBerImage2.sprite = UISprites[score % 10];
    }

    public void ShowResultMenu(int curScore, int bestScore)
    {
        if(curScore >10)
        {
            MedlaImage.sprite = UISprites[(int)SpriteType.Gold_Medal];
        }
        else
        {
            MedlaImage.sprite = UISprites[(int)SpriteType.Silver_Medal];
        }
       
        ResultNumbers[0].sprite = UISprites[curScore / 10];
        ResultNumbers[1].sprite = UISprites[curScore % 10];
        ResultNumbers[2].sprite = UISprites[bestScore /10];
        ResultNumbers[3].sprite = UISprites[bestScore % 10];

    }
}
