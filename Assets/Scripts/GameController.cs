using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private CubePos _nowCube = new CubePos(0, 1, 0);
    [SerializeField] private float _cubeChangePlaceSpeed = 0.5f;
    [SerializeField] private Transform _cubeToPlace;
    [SerializeField] private GameObject _cubeToCreate, _allCubes;
    private Rigidbody _allcubesRb;

    private List<Vector3> _allCubePositions = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(0, 1, 0),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(1, 0, -1),
        new Vector3(-1, 0, 1)


    };

    private void Start()
    {
        _allcubesRb = _allCubes.GetComponent<Rigidbody>();
        StartCoroutine(ShowCubePlace());
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newCube = Instantiate(
                _cubeToCreate,
                _cubeToPlace.position,
                Quaternion.identity
            ) as GameObject;
            newCube.transform.SetParent(_allCubes.transform);
            _nowCube.SetVector(_cubeToPlace.position);
            _allCubePositions.Add(_nowCube.GetVector());

            _allcubesRb.isKinematic = true;
            _allcubesRb.isKinematic = false;
            SpawnPositions();
            
        }
    }

    IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(_cubeChangePlaceSpeed);
        }
    }

    private void SpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        if (IsPositionEmpty(new Vector3(_nowCube.X + 1, _nowCube.Y, _nowCube.Z)) && _nowCube.X + 1 != _cubeToPlace.position.x)
        {
            positions.Add(new Vector3(_nowCube.X + 1, _nowCube.Y, _nowCube.Z));
        }
        if(IsPositionEmpty(new Vector3(_nowCube.X - 1, _nowCube.Y, _nowCube.Z))&& _nowCube.X - 1 != _cubeToPlace.position.x)
            positions.Add(new Vector3(_nowCube.X - 1, _nowCube.Y, _nowCube.Z));
        if(IsPositionEmpty(new Vector3(_nowCube.X, _nowCube.Y + 1, _nowCube.Z))&& _nowCube.Y + 1 != _cubeToPlace.position.y) 
            positions.Add(new Vector3(_nowCube.X, _nowCube.Y + 1, _nowCube.Z));
        if(IsPositionEmpty(new Vector3(_nowCube.X, _nowCube.Y - 1, _nowCube.Z))&& _nowCube.Y - 1 != _cubeToPlace.position.y) 
            positions.Add(new Vector3(_nowCube.X, _nowCube.Y - 1, _nowCube.Z));
        if(IsPositionEmpty(new Vector3(_nowCube.X, _nowCube.Y, _nowCube.Z + 1)) && _nowCube.Z + 1 != _cubeToPlace.position.z) 
            positions.Add(new Vector3(_nowCube.X, _nowCube.Y, _nowCube.Z+ 1)); 
        if(IsPositionEmpty(new Vector3(_nowCube.X, _nowCube.Y, _nowCube.Z - 1)) && _nowCube.Z - 1 != _cubeToPlace.position.z) 
            positions.Add(new Vector3(_nowCube.X, _nowCube.Y, _nowCube.Z - 1));

        _cubeToPlace.position = positions[Random.Range(0, positions.Count)];
    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
            return false;
        foreach (Vector3 pos in _allCubePositions)
        {
            if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
                return false;

        }

        return true;
    }
}

struct CubePos
{
    public int X, Y, Z;

    public CubePos(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public Vector3 GetVector()
    {
        return new Vector3(X, Y, Z);
    }

    public void SetVector(Vector3 pos)
    {
        X = Convert.ToInt32(pos.x);
        Y = Convert.ToInt32(pos.y);
        Z = Convert.ToInt32(pos.z);
    }
}
