using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallRoom : MonoBehaviour 
{
    public BigSmallRoom Up;
    public BigSmallRoom Down;
    public BigSmallRoom Left;
    public BigSmallRoom Right;

    public static Transform PlayerTransform;

    public bool Opened { get; set; }

    [SerializeField]
    private BigSmallRoomColliderController colliderController;

    [SerializeField]
    private Transform topRightTransform;

    [SerializeField]
    private Transform bottomLeftTransform;

    [SerializeField]
    private int numberOfHoles = 2;

    [SerializeField]
    private int numberOfEnemies = 3;

    private List<BigSmallHole> holes;
    private BigSmallButton button;
    private List<BigSmallEnemy> enemies;

    private void Awake()
    {
        if(PlayerTransform == null)
        {
            PlayerTransform = GameObject.Find("BigSmallPlayer").transform;
        }
        holes = new List<BigSmallHole>();
        enemies = new List<BigSmallEnemy>();
        Opened = false;
        colliderController.EnableDoors(Up != null, Down != null, Left != null, Right != null);
    }

    private void Start()
    {

        CreateRoomObstacles();
        CreateRoomEnemies();
        button.Pressed += ButtonPressed;
    }

    private void OnEnable()
    {
        RepositionEnemies();
    }

    private void ButtonPressed(object sender, System.EventArgs e)
    {
        button.Pressed -= ButtonPressed;
        Opened = true;
        OpenDoors();
    }

    private void CreateRoomObstacles()
    {
        var whichObstacle = false; // Random.Range(0, 100) >= 50;
        if(whichObstacle)
        {
            var numberOfCreatedHoles = Random.Range(1, numberOfHoles);
            for(int index = 0; index < numberOfCreatedHoles; index++)
            {
                var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
                var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
                var newPos = new Vector2(randomPosX, randomPosY);

                var hole = Instantiate(BigSmallRoomObjects.Instance.BigSmallHoleObject, newPos, Quaternion.identity);
                hole.transform.parent = transform;
                holes.Add(hole);
            }
        }
        else
        {
            var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
            var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
            var newPos = new Vector2(randomPosX, randomPosY);
            button = Instantiate(BigSmallRoomObjects.Instance.BigSmallButtonObject, newPos, Quaternion.identity);
            button.transform.parent = transform;
        }
    }

    private void CreateRoomEnemies()
    {
        var numberOfEnemiesCreated = holes.Count > 0 ? holes.Count + 1 : Random.Range(1, numberOfEnemies);
        for(int index = 0; index < numberOfEnemiesCreated; index++)
        {
            var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
            var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
            var newPos = new Vector2(randomPosX, randomPosY);

            var enemy = Instantiate(BigSmallRoomObjects.Instance.BigSmallEnemyObjects[Random.Range(0, BigSmallRoomObjects.Instance.BigSmallEnemyObjects.Count)],
                            newPos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemies.Add(enemy);
        }
    }

    private void RepositionEnemies()
    {
        for(int index = 0; index < enemies.Count; index++)
        {
            var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
            var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
            var newPos = new Vector2(randomPosX, randomPosY);
            enemies[index].transform.position = newPos;
        }
    }

    public void OpenDoors()
    {
        colliderController.EnableDoors(Up != null, Down != null, Left != null, Right != null);
    }

    public void CloseDoors()
    {
        colliderController.DisableDoors();
    }
}
