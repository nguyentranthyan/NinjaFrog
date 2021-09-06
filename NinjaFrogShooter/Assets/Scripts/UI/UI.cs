using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI 
{
    [Header("Data")]
    public Text txtCoinCount;
	public Text txtScore;
	public Text txtTime;

    [Header("Fuel's jetpack")]
    public Image fuelImage;
    public Text fuelText;

    [Header("Heart")]
    public GameObject[] playerLifes;

    [Header("Ammo")]
    public Text ammoText;

    [Header("Key")]
    public Image imageKey0;
    public Image imageKey1;
    public Image imageKey2;

    public Sprite keyFull0;
    public Sprite keyFull1;
    public Sprite keyFull2;

}
