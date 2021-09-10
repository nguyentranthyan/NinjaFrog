using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshData : MonoBehaviour
{
   public void Start()
    {
        DataManager.instance.RefreshData();
    }
}
