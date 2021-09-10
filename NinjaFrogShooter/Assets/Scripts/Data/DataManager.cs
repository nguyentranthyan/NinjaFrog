using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : Singleton<DataManager>
{
	public GameData data;
	string dataFilePath;
	BinaryFormatter bf;
	public bool devMode;

	protected override void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		bf = new BinaryFormatter();
		dataFilePath = Application.persistentDataPath + "/game.dat";
		Debug.Log(dataFilePath);
	}

	public void RefreshData()
	{
		if (File.Exists(dataFilePath))
		{
			FileStream fs = new FileStream(dataFilePath, FileMode.Open);
			data = (GameData)bf.Deserialize(fs);
			fs.Close();
			Debug.Log("Data Refresh");
		}
	}

	public void SaveData()
	{
		FileStream fs = new FileStream(dataFilePath, FileMode.Create);
		bf.Serialize(fs, data);
		fs.Close();
		Debug.Log("Data Saved");
	}
	public void SaveData(GameData data)
	{
		FileStream fs = new FileStream(dataFilePath, FileMode.Create);
		bf.Serialize(fs, data);
		fs.Close();
		Debug.Log("Data Saved");
	}

	public bool IsUnlocked(int levelNumber)
	{
		return data.levelDatas[levelNumber].isUnlocked;
	}

	public int GetStars(int levelNumber)
	{
		return data.levelDatas[levelNumber].starsAwarded;
	}

	void OnEnable()
	{
		CheckDB();
	}

	public void CheckDB()
	{
		if (!File.Exists(dataFilePath))
		{
			 #if UNITY_ANDROID
            CopyDB();
            #endif
		}
		else
		{
			if (SystemInfo.deviceType == DeviceType.Desktop)
			{
				string destFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
				File.Delete(destFile);
				File.Copy(dataFilePath, destFile);
			}

			if (devMode)
			{
				if (SystemInfo.deviceType == DeviceType.Handheld)
				{
					File.Delete(dataFilePath);
					CopyDB();
				}
			}
			RefreshData();
		}
	}

	public void CopyDB()
	{
		string srcFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
		WWW downloader = new WWW(srcFile);
		while (!downloader.isDone)
		{

		}
		File.WriteAllBytes(dataFilePath, downloader.bytes);
		RefreshData();
	}
}
