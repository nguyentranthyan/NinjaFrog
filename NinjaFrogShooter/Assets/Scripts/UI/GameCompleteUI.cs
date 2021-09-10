using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameCompleteUI : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private Button btnNext;
	[SerializeField] private Sprite goldStar;
	[SerializeField] private Image star1;
	[SerializeField] private Image star2;
	[SerializeField] private Image star3;
	[SerializeField] private Text txtScore;
	[SerializeField] private int levelNumber;

	
	[Header("Score")]
	[HideInInspector] private int score;
	[SerializeField] private int scoreForThreeStars;
	[SerializeField] private int scoreForTwoStars;
	[SerializeField] private int scoreForOneStars;
	[SerializeField] private int scoreForNextLevel;
	
	[SerializeField] private float animStarDelay;
	[SerializeField] private float animDelay;
	[SerializeField] private bool showTwoStars, showThreeStars;

	public void Start()
	{
		score = GameManager.Instance.GetScore();
		txtScore.text = "" + score;
		if (score >= scoreForThreeStars)
		{
			showThreeStars = true;
			GameManager.Instance.SetStarsAwarded(levelNumber, 3);
			Invoke("ShowGoldenStars", animStarDelay);
		}

		if(score >= scoreForTwoStars && score < scoreForThreeStars)
		{
			showTwoStars = true;
			GameManager.Instance.SetStarsAwarded(levelNumber, 2);
			Invoke("ShowGoldenStars", animStarDelay);
		}
		if(score<=scoreForOneStars && score != 0)
		{
			GameManager.Instance.SetStarsAwarded(levelNumber, 1);
			Invoke("ShowGoldenStars", animStarDelay);
		}
	}

	void ShowGoldenStars()
	{
		StartCoroutine("HandelFirstStarAnim", star1);
	}

	IEnumerator HandelFirstStarAnim(Image starImage)
	{
		DoAnim(starImage);

		// cause a delay before showing the next star
		yield return new WaitForSeconds(animDelay);

		// called if more than one star is awarded
		if (showTwoStars || showThreeStars)
			StartCoroutine("HandleSecondStarAnim", star2);
		else
			Invoke("CheckLevelStatus", 1.2f);
	}

	IEnumerator HandleSecondStarAnim(Image starImg)
	{
		DoAnim(starImg);

		// cause a delay before showing the next star
		yield return new WaitForSeconds(animDelay);
		showTwoStars = false;

		if (showThreeStars)
			StartCoroutine("HandleThirdStarAnim", star3);
		else
			Invoke("CheckLevelStatus", 1.2f);
	}

	IEnumerator HandleThirdStarAnim(Image starImg)
	{
		DoAnim(starImg);

		// cause a delay before showing the next star
		yield return new WaitForSeconds(animDelay);
		showThreeStars = false;
		Invoke("CheckLevelStatus", 1.2f);
	}

	void DoAnim(Image starImage)
	{
		//Increase the star size
		starImage.rectTransform.sizeDelta = new Vector2(150f, 150f);
		//Show the gold star
		starImage.sprite = goldStar;

		//reduce the star size normal using dotween animation
		RectTransform t = starImage.rectTransform;
		t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);

		//play an audio effect
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.KeyFound);

		//show a sparkle effect
		SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);
	}

	void CheckLevelStatus()
	{
		if (score >= scoreForNextLevel)
		{
			btnNext.interactable = true;

			//play an audio effect
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.KeyFound);

			//show a sparkle effect
			SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);

			//unlock the next level
			GameManager.Instance.UnlockLevel(levelNumber + 1);
		}
		else
		{
			btnNext.interactable = false;	
		}
	}

	public void OnButtonNextClicked(string nameScene)
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		SceneManager.LoadScene(nameScene);
		DataManager.instance.SaveData(GameManager.instance.data);
	}
	public void OnButtonRestartClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void OnButtonSelectLevelClicked(string nameScene)
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		SceneManager.LoadScene(nameScene);
	}
}
