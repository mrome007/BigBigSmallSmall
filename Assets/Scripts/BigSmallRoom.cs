using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigSmallRoom : MonoBehaviour 
{
    public bool Win = false;

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
    private List<BigSmallButton> button;
    private List<BigSmallEnemy> enemies;

    private void Awake()
    {
        if(PlayerTransform == null)
        {
            PlayerTransform = GameObject.Find("BigSmallPlayer").transform;
        }
        holes = new List<BigSmallHole>();
        button = new List<BigSmallButton>();
        enemies = new List<BigSmallEnemy>();
        Opened = false;
        colliderController.EnableDoors(Up != null, Down != null, Left != null, Right != null);
    }

    private void Start()
    {
        if(Win)
        {
            return;
        }
        CreateRoomObstacles();
        CreateRoomEnemies();
        if(button.Count > 0)
        {
            for(int index = 0; index < button.Count; index++)
            {
                button[index].Pressed += ButtonPressed;
            }
        }
        else
        {
            for(int index = 0; index < holes.Count; index++)
            {
                holes[index].Filled += FilledIn;
            }
        }
    }

    private void Update()
    {
        if(!Win)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        RearrangeEnemies();
        RepositionEnemies();
    }

    private void ButtonPressed(object sender, System.EventArgs e)
    {
        var pressed = true;
        for(int index = 0; index < button.Count; index++)
        {
            if(!button[index].Done)
            {
                pressed = false;
            }
        }

        if(pressed)
        {
            for(int index = 0; index < button.Count; index++)
            {
                button[index].Pressed -= ButtonPressed;
            }
            Opened = true;
            OpenDoors();
        }
    }

    private void FilledIn(object sender, System.EventArgs e)
    {
        RearrangeEnemies();
        var filled = true;

        for(int index = 0; index < holes.Count; index++)
        {
            if(!holes[index].Done)
            {
                filled = false;
            }
        }

        if(filled)
        {
            for(int index = 0; index < holes.Count; index++)
            {
                holes[index].Filled -= FilledIn;
            }
            Opened = true;
            OpenDoors();
        }
    }

    private void CreateRoomObstacles()
    {
        var whichObstacle = Random.Range(0, 100) >= 50;
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
            var numberOfCreatedHoles = Random.Range(1, numberOfHoles);
            for(int index = 0; index < numberOfCreatedHoles; index++)
            {
                var randomPosX = Random.Range(bottomLeftTransform.transform.position.x, topRightTransform.transform.position.x);
                var randomPosY = Random.Range(bottomLeftTransform.transform.position.y, topRightTransform.transform.position.y);
                var newPos = new Vector2(randomPosX, randomPosY);
                var btn = Instantiate(BigSmallRoomObjects.Instance.BigSmallButtonObject, newPos, Quaternion.identity);
                btn.transform.parent = transform;
                button.Add(btn);
            }
        }
    }

    private void CreateRoomEnemies()
    {
        var numberOfEnemiesCreated = holes.Count > 0 ? 3 * holes.Count + Random.Range(1,3): Random.Range(3, numberOfEnemies);
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

    private void RearrangeEnemies()
    {
        enemies = enemies.Where(enemy => enemy != null).ToList();
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
