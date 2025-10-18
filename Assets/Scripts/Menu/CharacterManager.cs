using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static int characterValuePlayer1;
    public static int characterValuePlayer2;
    public MovementSM smPlayer1;
    public MovementSM smPlayer2;

    public void Awake()
    {
        smPlayer1 = GameObject.Find("Player").GetComponent<MovementSM>();
        smPlayer2 = GameObject.Find("Player 2").GetComponent<MovementSM>();
        smPlayer1.character = characterValuePlayer1;
        smPlayer2.character = characterValuePlayer2;
    }
}
