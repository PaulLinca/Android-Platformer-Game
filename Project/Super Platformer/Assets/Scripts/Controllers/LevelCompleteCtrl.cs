using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelCompleteCtrl : MonoBehaviour
{

    public Button btnNextLevel;
    public Sprite goldenStar;
    public Image star1;
    public Image star2;
    public Image star3;
    public Text txtScore;

    public int levelNumber;
    [HideInInspector]
    public int score;
    public int scoreForThreeStars;
    public int scoreForTwoStars;
    public int scoreForOneStar;
    public int scoreForNextLevel;

    public float animStartDelay;
    public float animDelay;

    public bool showTwoStars, showThreeStars;

    // Start is called before the first frame update
    void Start()
    {
        score = GameCtrl.instance.GetScore();

        txtScore.text = score.ToString();

        if(score >= scoreForThreeStars)
        {
            showThreeStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 3);
            Invoke("ShowGoldenStars", animStartDelay);
        }
        if(score >= scoreForTwoStars && score < scoreForThreeStars)
        {
            showTwoStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 2);
            Invoke("ShowGoldenStars", animStartDelay);
        }
        if(score >= scoreForOneStar && score != 0)
        {
            GameCtrl.instance.SetStarsAwarded(levelNumber, 1);
            Invoke("ShowGoldenStars", animStartDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", star1);
    }

    IEnumerator HandleFirstStarAnim(Image starImg)
    {
        DoAnim(starImg);

        yield return new WaitForSeconds(animDelay);

        if(showTwoStars || showThreeStars)
        {
            StartCoroutine("HandleSecondStarAnim", star2);
        }
        else
        {
            Invoke("CheckLevelStatus", 1.2f);
        }
    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);

        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;
        if(showThreeStars)
        {
            StartCoroutine("HandleThirdStarAnim", star3);
        }
        else
        {
            Invoke("CheckLevelStatus", 1.2f);
        }
    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);

        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;

        Invoke("CheckLevelStatus", 1.2f);
    }

    void DoAnim(Image starImg)
    {
        // increase star size
        starImg.rectTransform.sizeDelta = new Vector2(250f, 250f);

        // show golden star
        starImg.sprite = goldenStar;

        // reduce star to normal
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(200f, 200f), 0.5f, false);

        AudioCtrl.instance.KeyFound(starImg.gameObject.transform.position);

        SFXCtrl.instance.ShowBulletSparkle(starImg.gameObject.transform.position);
    }

    void CheckLevelStatus()
    {
        if(score >= scoreForNextLevel)
        {
            btnNextLevel.interactable = true;

            SFXCtrl.instance.ShowBulletSparkle(btnNextLevel.gameObject.transform.position);

            AudioCtrl.instance.KeyFound(btnNextLevel.gameObject.transform.position);

            GameCtrl.instance.UnlockLevel(levelNumber);
        }
        else
        {
            btnNextLevel.interactable = false;
        }
    }
}
