using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManeger : MonoBehaviour
{
    // serializable make an object into a list or array so we can send it to another API
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        // the constructor for class
        public Count(int minimum, int maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

    }

    // we will generate the map base on a grid system
    // so we will have row and column
    public int row;
    public int column;

    // the list of vector is sorted all cell of ours grid system
    private List<Vector3> cells = new List<Vector3>();



    //then in here we will define the random number of object we want to generate
    public Count objectCount = new Count(3, 6);
    public Count potionCount = new Count(2, 6);

    public GameObject exitDoor;

    // because we have multiple tiles so we have to define an array of GameObject
    public GameObject[] floorTiles;
    public GameObject[] wallTopTiles;
    public GameObject[] wallBottomTiles;
    public GameObject[] wallLeftTiles;
    public GameObject[] wallRightTiles;
    public GameObject[] enemys;
    public GameObject[] innerWallTiles;
    public GameObject[] potionsTiles;

    public GameObject[] objects;

    // this will store all the game objects that spawn in scenes
    private Transform worldHolder;

    public GameObject rightCorrner;
    public GameObject leftCorrner;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // this will create the grid system with the index of each cell like a Matrix in Liner Algebra
    void initializeGridSystem()
    {
        // to create a matrix we use a nested for loop just like in Ki Thuat Lap Trinh
        for (int x = 1; x < column - 1; x++)
        {
            for (int y = 1; y < row - 1; y++)
            {
                // we just make a square matrix have level of n-1, so we won fill the outer path with trap or wall and make an impossible level to pass through
                cells.Add(new Vector3(x, y, 0f));
            }

        }

    }

    // this function will make the floor and outer wall for the map
    void BoardSetup()
    {
        // init the worldHolder ang add boar tag to it
        worldHolder = new GameObject("map").transform;

        // this nested loop is use for making the outer wall and floor of the game world
        // this will start at the outer position of the matrix where the outer wall exist
        for (int x = -1; x < column + 1; x++)
        {
            for (int y = -1; y < row + 1; y++)
            {
                // make a floor for all cells by get a random tile from floor
                GameObject floorPrefabs = floorTiles[Random.Range(0, floorTiles.Length)];

                // if the position is the outer wall we create outer wall
                if (x == -1)
                {
                    floorPrefabs = wallLeftTiles[Random.Range(0, wallLeftTiles.Length)];
                }
                if (x == column)
                {
                    floorPrefabs = wallRightTiles[Random.Range(0, wallRightTiles.Length)];
                }
                if (y == -1)
                {
                    floorPrefabs = wallBottomTiles[Random.Range(0, wallBottomTiles.Length)];
                }
                if (y == row)
                {
                    floorPrefabs = wallTopTiles[Random.Range(0, wallTopTiles.Length)];
                    // because the top wall is wide so we make it two cells

                }

                // two top conner use different sprite
                if (x == -1 && y == row)
                {
                    floorPrefabs = leftCorrner;
                }

                if (x == column && y == row)
                {
                    floorPrefabs = rightCorrner;
                }

                // MAKE THE MAP BY CALLING INSTANTIATE TO CREATE A CLONE OBJECT OF OUR FLOOR OR WALL TILES
                GameObject makeMapCells = Instantiate(floorPrefabs, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                // assign the newly made game object to worldHolder so we dont have to see a long long list of clone object in hierarchy window

                makeMapCells.transform.SetParent(worldHolder);

            }
        }

    }

    // get a random position in the grid system to throw enemy food or weapon in there
    Vector3 randomPostion()
    {
        // get a random number from 0 to number of cell in our grid system
        int randomIndex = Random.Range(0, cells.Count);

        // next we will get the postion of that random number
        Vector3 postionRandom = cells[randomIndex];

        // then we remove that cordinate so we cant choose it again and overwrite the game objects
        cells.RemoveAt(randomIndex);

        return postionRandom;
    }

    // this function is for spawn gameobject at randomPostion

    void spawnObjectAtRandom(GameObject[] spawnObject, int min, int max)
    {
        // object count is the number of objects we want to spawn in the game map
        int objectCount = Random.Range(min, max + 1);

        // after we have the number of objects we want to spawn in the game map
        // use a for loop to spawn random objects in the spawnObject array

        for (int i = 0; i < objectCount; i++)
        {
            //call random position to choose a random postion to spawn objects
            Vector3 randomPos = randomPostion();

            // choose game object base on random 
            GameObject randomChooseGameObject = spawnObject[Random.Range(0, spawnObject.Length)];

            // spawn object with the random position and random choose from array
            Instantiate(randomChooseGameObject, randomPos, Quaternion.identity);

        }

    }


    // this function will setup the MAP
    public void mapGen(int level)
    {
        // call board setup
        BoardSetup();

        // create the grid system
        initializeGridSystem();

        //create inner wall tiles
        spawnObjectAtRandom(objects, objectCount.minimum, objectCount.maximum);

        //spawn potion items
        spawnObjectAtRandom(potionsTiles, potionCount.minimum, potionCount.maximum);


        // calculate difficulty curve base on the log n where n = level
        int enemysCount = (int)Mathf.Log(level, 2f);

        spawnObjectAtRandom(enemys, enemysCount, enemysCount);

        // spawn the exit point
        Instantiate(exitDoor, new Vector3(column - 1, row - 1, 0f), Quaternion.identity);
    }
}
