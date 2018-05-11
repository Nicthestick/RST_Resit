using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

	
public class RoomLayout : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum; 			//Minimum value for our Count class.
		public int maximum; 			//Maximum value for our Count class.
			
			
		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
    

    public bool simpleRooms;
    public bool tb;
    public bool lr;
    public Vector3 floorOffset = new Vector3(-3.5f, -3.5f, 0.5f);
    public int columns = 8; 										//Number of columns in our game board.
	public int rows = 8;											//Number of rows in our game board.
	public Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
	public Count foodCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
	public GameObject exit;											//Prefab to spawn for exit.
	public GameObject[] floorTiles;									//Array of floor prefabs.
	public GameObject[] wallTiles;									//Array of wall prefabs.
	public GameObject[] foodTiles;									//Array of food prefabs.
	public GameObject[] enemyTiles;									//Array of enemy prefabs.
	public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
    public int levelNumber;
    public GameObject StartSquare;
    public RoomLayout roomlayout_;
   

    private Vector3 tempGO;	
	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
    private bool done = false;


private void Awake()
{
    SetupScene(1);
}
//Clears our list gridPositions and prepares it to generate a new board.
void InitialiseList () 
	{
		gridPositions.Clear ();
			
		for(int x = 1; x < columns-1; x++)
		{
			for(int y = 1; y < rows-1; y++)
			{
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}
		
		
	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
           
            if (done == false)
        {
            boardHolder = new GameObject("Board").transform;
            tempGO = transform.position;

            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    GameObject instance =
                            Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
            if (tb == true)
            {
                floorOffset = new Vector3(-0.5f, -3.5f, 0.5f);

            }
            else if (lr == true)
            {
                floorOffset = new Vector3(-3.5f, -0.5f, 0.5f);
            } else
            {
                floorOffset = new Vector3(-3.5f, -3.5f, 0.5f);
            }


            boardHolder.transform.localPosition = tempGO +floorOffset;
            done = true;
        }
			

	}
		
		
	//RandomPosition returns a random position from our list gridPositions.
	Vector3 RandomPosition ()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);

		Vector3 randomPosition = gridPositions[randomIndex];

		gridPositions.RemoveAt (randomIndex);

		return randomPosition;
	}
		
		
	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum+1);
			
		for(int i = 0; i < objectCount; i++)
		{
				
			Vector3 randomPosition = (RandomPosition() + tempGO + new Vector3(-3.5f, -3.5f, 0.5f));

        GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
                
		}
	}
		
		
	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level_)
	{
            

		//Creates the outer walls and floor.
		BoardSetup ();
			
		//Reset our list of gridpositions.
		InitialiseList ();
			
		//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
			
		//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
			
        if (simpleRooms == true)
        {
        StartSquare = GameObject.FindGameObjectWithTag("StartRoom");
        level_ = StartSquare.GetComponent<RoomLayout>().levelNumber;
        }

		//Determine number of enemies based on current level number, based on a logarithmic progression
		int enemyCount = (int)Mathf.Log(level_, 2f);
        levelNumber = level_;
			
		//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
			
	}
}
