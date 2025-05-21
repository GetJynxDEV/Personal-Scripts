using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{    
    //Attach the GameObject Prefab that you want to Instantiate
    [SerializeField] private GameObject prefab;
    //Attach the Transform of where you want it to spawn
    [SerializeField] private Transform prefabPos;

    //Countdown Timer for when it will spawn
    [SerializeField] private float elapsedTimer;
    //Set Max Timer for Countdown Timer
    [SerializeField] private float maxTimer = 2f;

    //Float Value for starting X position of Prefab
    [SerializeField] private float startPos;

    
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
        //There will be 2 Examples of how you will spawn a prefab

        //Example One
        //Call the Object you want to Instantiate, then the Position you want, then last is the Rotation
         GameObject newObject Instantiate(prefab, prefabPos.position, Quaternion.identity);

        //Example Two
        //This will randomize the X and Y Position of the Platform
        Vector3 newPos = new Vector3(0, 0, 0); //You can modify what's inside the new Vector3
        //Call the Object you want to Instantiate, then get the newPos for its Position, then last is the Rotation
        GameObject newObject = Instantiate(prefab, newPos, Quaternion.identity);

        //You can also set the Instantiated Object to be a child of a GameObject
        //Set newObject inside a Parent
        newObject.transform.SetParent(parentObject);
    }

    private void Ienumator
}
