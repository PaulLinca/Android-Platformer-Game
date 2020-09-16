using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    int levelNumber;
    Button button;
    Image buttonImage;
    Text buttonText;
    Transform star1, star2, star3;

    public Sprite lockedButtonSprite;
    public Sprite unlockedButtonSprite;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        levelNumber = int.Parse(transform.gameObject.name);

        button = transform.gameObject.GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        buttonText = button.gameObject.transform.GetChild(0).GetComponent<Text>();

        star1 = button.gameObject.transform.GetChild(1);
        star2 = button.gameObject.transform.GetChild(2);
        star3 = button.gameObject.transform.GetChild(3);

        SetButtonStatus();
    }

    void SetButtonStatus()
    {
        var unlocked = DataCtrl.instance.IsLevelUnlocked(levelNumber);
        var starsAwarded = DataCtrl.instance.GetLevelStarsAwarded(levelNumber);

        if(unlocked)
        {
            if(starsAwarded == 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
            }
            if(starsAwarded == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);
            }
            if(starsAwarded == 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);            
            }
            if(starsAwarded == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }

            button.onClick.AddListener(LoadScene);
        }
        else
        {
            buttonImage.overrideSprite = lockedButtonSprite;
            buttonText.text = string.Empty;
        
            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
