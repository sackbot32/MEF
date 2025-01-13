using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment
{
    // Create an instance of the GameEnvironment class called 'instance'.
    private static GameEnvironment instance;

    // Create a list of game objects called 'checkpoints'
    private List<GameObject> checkpoints = new List<GameObject>();
    private List<Transform> hidePoints = new List<Transform>();

    // Create public reference for retrieving checkpoints list.
    public List<GameObject> Checkpoints { get { return checkpoints; } }
    public List<Transform> HidePoints { get { return hidePoints; } }

    // Create singleton if it doesn't already exist and populate list with any objects found with tag set to "Checkpoint".
    public static GameEnvironment Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new GameEnvironment();
                instance.Checkpoints.AddRange(
                    GameObject.FindGameObjectsWithTag("Checkpoint"));
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("Hide"))
                {
                    instance.hidePoints.Add(item.transform);
                }
            }
            return instance;
        }
    }

}
