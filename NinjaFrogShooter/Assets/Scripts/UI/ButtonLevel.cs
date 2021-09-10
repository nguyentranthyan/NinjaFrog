using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
    int levelNumber; //Level Select
    Button btn;
    Image btnImg; //Image in button
    Text btnText; //Text in button
    Transform star1, star2, star3; //Star in button

    [SerializeField] private Sprite lockedBtn; //Show sprite locked
    [SerializeField] private Sprite unLockedBtn; //Show sprite unlocked
    [SerializeField] private string sceneName; 

	// Start is called before the first frame update
	void Start()
    {
        levelNumber = int.Parse(transform.gameObject.name);

        //Setup button
        btn = transform.gameObject.GetComponent<Button>();
        btnImg = btn.GetComponent<Image>();
        btnText = btn.gameObject.transform.GetChild(0).GetComponent<Text>();

        //Star
        star1 = btn.gameObject.transform.GetChild(1);
        star2 = btn.gameObject.transform.GetChild(2);
        star3 = btn.gameObject.transform.GetChild(3);

        BtnStatus();
    }

    void BtnStatus()
    {
        bool unlocked = DataManager.instance.IsUnlocked(levelNumber);
        int startAwared = DataManager.instance.GetStars(levelNumber);
		if (unlocked)
		{
            if (startAwared == 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
            }
            if (startAwared == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);
            }
            if (startAwared == 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
            if (startAwared == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
            btn.onClick.AddListener(LoadScene);
        }
		else
		{
            //show the locked btn image
            btnImg.overrideSprite = lockedBtn;

            //don't show text on button
            btnText.text = "";

            //hide 3 star
            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
    }

	private void LoadScene()
	{
        LoadingBar.instance.ShowLoading();
        Invoke("changeScene", 2f);
	}
    private void changeScene()
	{
        SceneManager.LoadScene(sceneName);
    }
}
