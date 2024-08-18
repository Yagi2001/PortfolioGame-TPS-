using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int deadEnemyCounter;

    private void Awake()
    {
        deadEnemyCounter = 0;
    }
    private void OnEnable()
    {
        PlayerCombat.CharacterDead += GameOver;
    }

    private void OnDisable()
    {
        PlayerCombat.CharacterDead -= GameOver;
    }

    private void Update()
    {
        if (deadEnemyCounter >= 9)
        {
            StartCoroutine( LoadPlayerWon() );
        }
    }

    private IEnumerator LoadPlayerWon()
    {
        yield return new WaitForSeconds( 2f );
        EnableCursor();
        LoadGameOverScene();
        SceneManager.LoadScene( "PlayerWonScene" );
    }

    private void GameOver()
    {
        StartCoroutine( GameOverRoutine() );
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime( 3.5f );
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
