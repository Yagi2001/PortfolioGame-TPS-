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

    private void GameOver()
    {
        StartCoroutine( GameOverRoutine() );
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime( 2 );
        EnableCursor();
        LoadGameOverScene();
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene( "MainMenu" );
    }

    private void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


}
