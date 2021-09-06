using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSound : MonoBehaviour
{
	[SerializeField]
	private Slider audioVolume;
	[SerializeField]
	private Image soundOnIcon;
	[SerializeField]
	private Image soundOffIcon;
	private bool muted = false;

	// Start is called before the first frame update
	void Start()
	{
		if (!PlayerPrefs.HasKey("MusicVolume"))
		{
			PlayerPrefs.SetFloat("MusicVolume", audioVolume.value);
			Load();
		}
		else
		{
			Load();
		}

		if (!PlayerPrefs.HasKey("Muted"))
		{
			PlayerPrefs.SetFloat("Muted", 0);
			Load();
		}
		else
		{
			Load();
		}
		UpdateIcon();
		AudioListener.pause = muted;
	}
	public void ChangeVolume()
	{
		AudioListener.volume = audioVolume.value;
		Save();
	}
	private void Load()
	{
		audioVolume.value = PlayerPrefs.GetFloat("MusicVolume");
	}
	private void Save()
	{
		PlayerPrefs.SetFloat("MusicVolume", audioVolume.value);
	}

	public void OnButtonSoundPress()
	{
		if (muted == false)
		{
			muted = true;
			AudioListener.pause = true;
		}
		else
		{
			muted = false;
			AudioListener.pause = false;
		}
		UpdateIcon();
		SaveVolume();
	}
	private void LoadVolume()
	{
		muted = PlayerPrefs.GetInt("Muted") == 1;
	}
	private void SaveVolume()
	{
		PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
	}

	private void UpdateIcon()
	{
		if (muted == false)
		{
			soundOffIcon.enabled = false;
		}
		else
		{
			soundOffIcon.enabled = true;
		}
	}
}
