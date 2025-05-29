using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public static int intValue;

    private void Start()
    {
        //This will get the saved "value" from the updateData()
        //If no "value" was saved, intValue will be set to 0
        intValue = PlayerPrefs.GetInt("value", 0);
    }

    public void updateData()
    {
        //This will update the PlayerPrefs Int "value" to the current intValue
        PlayerPrefs.SetInt("value", intValue);
    }
}
