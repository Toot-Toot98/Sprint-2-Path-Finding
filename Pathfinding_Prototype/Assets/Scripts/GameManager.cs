using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int enemyLeft = 0;

    public void EnemyHasDied()
    {
        enemyLeft--;

        if (enemyLeft == 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void EnemyHasAppeared()
    {
        enemyLeft++;
    }

}
