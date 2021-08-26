using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Text CoinText;

    private int coinCollect;

	// Update is called once per frame
	void Update()
    {
        AllCoinCollected();
		coinCollect = transform.childCount;
		CoinText.text = coinCollect.ToString();
    }

    public void AllCoinCollected()
	{
		if (transform.childCount == 0)
		{
            Invoke("ChangeScene", 1);
		}
	}

    void ChangeScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
