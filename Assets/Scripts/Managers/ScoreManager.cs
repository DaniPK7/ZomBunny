using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int HellephantCount;
    public static int ZombearsCount;
    public static int ZombunniesCount;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score +
        "\nHellephant: "+ HellephantCount +
        "\nZombears: " + ZombearsCount +
        "\nZombunnies: " + ZombunniesCount;
    }
}
