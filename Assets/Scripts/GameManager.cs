using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerCombat.CharacterDead += GameOver;
    }

    private void OnDisable()
    {
        PlayerCombat.CharacterDead -= GameOver;
    }

    public void GameOver()
    {
        StartCoroutine( GameOverRoutine() );
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime( 2 );
        LoadGameOverScene();
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene( "MainMenu" );
    }
}
