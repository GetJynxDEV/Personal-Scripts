using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{    
    //Attach the GameObject Prefab that you want to Instantiate
    public GameObject prefab;
    //Attach the Transform of where you want it to spawn
    public Transform prefabPos;

    //Countdown Timer for when it will spawn
    public float elapsedTimer;
    //Set Max Timer for Countdown Timer
    public float maxTimer = 2f;

    private void Update()
    { 
        //Elapsed Timer will now start counting
        elapsedTimer = Time.deltaTime;

        //If Elapsed Timer is greater than Max Timer Function will be called
        if (elapsedTimer > maxTimer)
        {
            //Elapsed Timer will be set to 0 again
            elapsedTimer = 0;

            //Function to Instantiate Game Object
            spawnPrefab();
        }
    }

    private void spawnPrefab()
    {
        //This is how you will Instantiate a Game Object
        //Call the Object you want to Instantiate, then the Position you want, then last is the Rotation
        Instantiate(prefab, prefabPos.position, Quaternion.identity);
    }
}
