using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private SFX sfx;

    public void ShowItemSparkle(Vector3 position)
	{
        Instantiate(sfx.sfx_item_pickup, position, Quaternion.identity);
	}

    public void ShowGunSparkle(Vector3 position)
    {
        Instantiate(sfx.sfx_gun_pickup, position, Quaternion.identity);
    }
}
