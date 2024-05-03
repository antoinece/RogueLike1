using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ennemyGen : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector2 centre = new Vector2(0, 0);
    private Vector2 LastRoom = new Vector2(0, 0);
    private Vector2 SpawnPosition = new Vector2(0, 0);
    public List<Vector2Int> VisitedRooms = new List<Vector2Int>();
    [SerializeField]private RoomGenerator _roomGenerator;
    private Vector3 centreCamera = new Vector3(0.25f, 0.25f, -10);
    private bool _changeRoom = false;
    private EnemiesUtil _enemiesUtil;
    [SerializeField] private GameObject chaser;
    [SerializeField] private GameObject dasher;
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject spawner;
    private int enemyValue;
    public int nbEnemies;
    void Start()
    {
        transform.position = new Vector3(centre.x/2,centre.y/2,0);
        _enemiesUtil = FindFirstObjectByType<EnemiesUtil>();
        VisitedRooms.Clear();
        VisitedRooms.Add(new Vector2Int(0,0));
    }

    void Update()
    {
        Debug.Log(nbEnemies);
        if (player.transform.position.x > centreCamera.x+7f)
        {
            LastRoom = centre;
            centre = new Vector2(centre.x+27,centre.y);
            centreCamera = new Vector3(centreCamera.x+13.22f,centreCamera.y,centreCamera.z);
            transform.position = centreCamera;
            _changeRoom = true;
        }
        if (player.transform.position.x < centreCamera.x-7f)
        {
            LastRoom = centre;
            centre = new Vector2(centre.x-27,centre.y);
            centreCamera = new Vector3(centreCamera.x-13.22f,centreCamera.y,centreCamera.z);
            transform.position = centreCamera;
            _changeRoom = true;
        }
        if (player.transform.position.y > centreCamera.y+5f)
        {
            LastRoom = centre;
            centre = new Vector2(centre.x,centre.y+15);
            centreCamera = new Vector3(centreCamera.x,centreCamera.y+7.35f,centreCamera.z);
            transform.position = centreCamera;
            _changeRoom = true;
        }
        if (player.transform.position.y < centreCamera.y-4)
        {
            LastRoom = centre;
            centre = new Vector2(centre.x,centre.y-15);
            centreCamera = new Vector3(centreCamera.x,centreCamera.y-7.35f,centreCamera.z);
            transform.position = centreCamera;
            _changeRoom = true;
        }

        if (_changeRoom)
        {
            foreach (Vector2Int room in VisitedRooms)
            {
                if (room==centre)
                {
                    _changeRoom = false;
                    return;
                }
            }

            if (_changeRoom)
            {
                VisitedRooms.Add(new Vector2Int(Mathf.CeilToInt(centre.x),Mathf.CeilToInt(centre.y)));
                foreach (Vector3Int room in _roomGenerator.RoomsValue)
                {
                    if (new Vector2(room.x, room.y) == centre)
                    {
                        enemyValue = room.z;
                        GenerateEnemies();
                    }
                }
            }
        }
        _changeRoom = false;

        if (nbEnemies==0)
        {
            _roomGenerator.DoorOpen();
            if (VisitedRooms.Count == _roomGenerator.Rooms.Count)
            {
                SceneManager.LoadScene("EndScene");
            }
        }

        
    }

    private void GenerateEnemies()
    {
        int i = 0;
        SpawnPosition = (centre + (centre - LastRoom) /3f)/2;
        while (enemyValue>1||i==20)
        {
            i++;
            _roomGenerator.DoorClose();
            switch (enemyValue)
            {
                case > 4:
                    HighDraw();
                    break;
                case 3 or 4:
                    MediumDraw();
                    break;
                case 2:
                    LowDraw();
                    break;
                case 1:
                    _enemiesUtil.Spawn(chaser,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                    enemyValue -= 1;
                    break;
                default:
                    Debug.Log("spawnPick");
                    enemyValue -= 1;
                    break;
            }    
        }
    }

    private void HighDraw()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                _enemiesUtil.Spawn(shooter,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 2;
                break;
            case 2:
                _enemiesUtil.Spawn(dasher,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 3;
                break;
            case 3:
                _enemiesUtil.Spawn(spawner,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 5;
                break;
            default:
                Debug.Log("spawn");
                break;
        }
    }
    
    private void MediumDraw()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                _enemiesUtil.Spawn(chaser,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 1;
                break;
            case 2:
                _enemiesUtil.Spawn(shooter,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 2;
                break;
            case 3:
                _enemiesUtil.Spawn(dasher,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 3;
                break;
            default:
                Debug.Log("spawn");
                break;
        }
    }
    private void LowDraw()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                _enemiesUtil.Spawn(chaser,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 1;
                break;
            case 2:
                _enemiesUtil.Spawn(shooter,new Vector3(SpawnPosition.x,SpawnPosition.y,-2));
                enemyValue -= 2;
                break;
            default:
                Debug.Log("spawn");
                break;
        }
    }

}
