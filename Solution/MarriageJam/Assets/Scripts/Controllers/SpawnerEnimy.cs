using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnerEnimy : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] enemyHard;

    public Transform[] spawnerPoints;
    [Range(0.5f, 5f)]
    public float spawnDelay = 1;
    [Range(1, 10)]
    public int numberOfEnimy;

    private int _currentEnimy;
    private FollowCamera _camera;

    void Start()
    {
        _camera = (FollowCamera)FindObjectOfType(typeof(FollowCamera));

        if (GameManager.difficulty == Difficulty.Hard && enemyHard.Any())
        {
            enemy = enemyHard;
        }
    }

    void Update()
    {
        CheckedCurrentEnemies();
    }

    private void CheckedCurrentEnemies()
    {
        if (_currentEnimy >= numberOfEnimy)
        {
            int enemies = FindObjectsOfType<Enemy>().Length;
            if (enemies <= 0)
            {
                _camera.camFollow = true;
                //FindObjectOfType<LimitPlayerCam>().LimitCamActive(false);
                FindObjectOfType<GoOnIndication>().TurnOnSignaling();
                Destroy(this.gameObject);
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy[Random.Range(0, enemy.Length)], spawnerPoints[Random.Range(0, spawnerPoints.Length)].position, Quaternion.identity);
        _currentEnimy++;

        if (_currentEnimy < numberOfEnimy)
        {
            Invoke("SpawnEnemy", spawnDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            //FindObjectOfType<LimitPlayerCam>().LimitCamActive(true);
            _camera.camFollow = false;
            SpawnEnemy();
        }
    }
}
