using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public int characterValue1;
    public int characterValue2;
    public TextMeshProUGUI characterName1;
    public TextMeshProUGUI characterName2;

    public void PlayGame()
    {
        CharacterManager.characterValuePlayer1 = characterValue1;
        CharacterManager.characterValuePlayer2 = characterValue2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetPlayer1(int characterValue)
    {
        characterValue1 = characterValue;
        if (characterValue == 0)
        {
            characterName1.SetText("Player 1: Shoto");
        }
        else
        {
            characterName1.SetText("Player 1: Grappler");
        }
    }

    public void SetPlayer2(int characterValue)
    {
        characterValue2 = characterValue;
        if (characterValue == 0)
        {
            characterName2.SetText("Player 2: Shoto");
        }
        else
        {
            characterName2.SetText("Player 2: Grappler");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
