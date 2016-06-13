using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public void Awake()
    {
        instance = this;
    }

}
