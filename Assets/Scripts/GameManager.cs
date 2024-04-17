using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomStruct; 

public class GameManager : MonoBehaviour {

    public Tile[] possibleTiles = new Tile[] { 
        new Tile(tileType.Adjacent, "Assets/Sprites/adjacent.png", 5, 0), 
        new Tile(tileType.Orthogonal, "Assets/Sprites/orthogonal.png", 7, 0), 
        new Tile(tileType.Diagonal, "Assets/Sprites/diagonal.png", 5, 0), 
        new Tile(tileType.Joker, "Assets/Sprites/joker.png", 2, 0), 
        new Tile(tileType.Start, "Assets/Sprites/start.png", 1, 0), 
        new Tile(tileType.Guard, "Assets/Sprites/guard.png", 0, 10), 
        new Tile(tileType.King, "Assets/Sprites/king.png", 0, 3), 
        new Tile(tileType.Ally, "Assets/Sprites/ally.png", 0, 8)
    }; 

    public List<Tile> possibleGardenTiles = new List<Tile>();
    public List<Tile> gardenTiles = new List<Tile>();
    public List<Tile> queenOfHeartsHand = new List<Tile>();
    public List<Tile> aliceHand = new List<Tile>();

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles; 
    public List<Sprite> gamePuzzles = new List<Sprite>();
    
    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess = false; 

    private int countGuesses; 
    private int countCorrectGuesses; 
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex; 
    private string firstGuessPuzzle, secondGuessPuzzle; 



    void Awake() {
        for (int i=0; i<4; i++) {
            gardenTiles.Add(possibleTiles[i]);
        }
        for (int i=5; i<7; i++) {
            queenOfHeartsHand.Add(possibleTiles[i]);
        }
        aliceHand.Add(possibleTiles[7]);
        /*puzzles = Resources.LoadAll<Sprite> ("Sprites/");
        Debug.Log(puzzles);*/
    }

    void Start(){
        GetButtons();  
        AddListeners();
        //AddGamePuzzles();
        GenerateGarden(); 
        Shuffle(gardenTiles);
    }

    void GetButtons(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GardenTiles");
        for(int i=0; i<objects.Length; i++){
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles() {
        int looper = btns.Count;
        int index = 0; 

        for (int i = 0; i < looper; i++) {
            if (index == looper/2) {
                index = 0; 
            }
            gamePuzzles.Add(puzzles[index]);
            index ++; 
        }
    }

    void GenerateGarden() {
        foreach (Tile tile in possibleGardenTiles) {
            for (int i=0; i<tile.availibleToPlaceOnTheField; i++){
                gardenTiles.Add(tile);
            }
        }
    }

    /*void DistributeTiles() {
        for (int i=0; i<5; i++) {
            queenOfHeartsHand.Add(gardenTiles[i]);
            gardenTiles.RemoveAt(i);
        }
        for (int i=0; i<5; i++) {
            aliceHand.Add(gardenTiles[i]);
            gardenTiles.RemoveAt(i);
        }
    }*/

    void AddListeners() {
        foreach (Button btn in btns) {
            btn.onClick.AddListener(() => PickATile());
        }
    }

    public void PickATile() {
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int buttonIndex = int.Parse(buttonName.Substring(buttonName.Length -1, 1)) -1 ; 

        if (!firstGuess) {
            
            firstGuess = true; 
            
            firstGuessIndex = buttonIndex ;
            
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

        } else if (!secondGuess) {

            secondGuess = true; 

            secondGuessIndex = buttonIndex;
            
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            countGuesses ++; 

            StartCoroutine(CheckIfThePuzzlesMatch());

            /*if (firstGuessPuzzle == secondGuessPuzzle) {
                Debug.Log("The puzzles match");
            } else {
                Debug.Log("The puzzles dont match");
            }*/

        }

    }

    IEnumerator CheckIfThePuzzlesMatch() {
        
        yield return new WaitForSeconds (1f); 

        if (firstGuessPuzzle == secondGuessPuzzle) {

            yield return new WaitForSeconds (.5f); 

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false; 

            btns[firstGuessIndex].image.color = new Color (0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color (0, 0, 0, 0); 

            CheckIfGameIsFinished(); 

        } else {

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

        }

        yield return new WaitForSeconds (.5f);

        firstGuess = secondGuess = false; 

    }

    void CheckIfGameIsFinished() {
        countCorrectGuesses ++; 
        if (countCorrectGuesses == gameGuesses) {
            Debug.Log("Game Finished"); 
            Debug.Log("It took you " + countGuesses + " many guess(es) to finish the game.");
        }
    }

    void Shuffle(List<Tile> list) {

        for (int i = 0; i < list.Count; i++) {
            
            Tile temp = list[i]; 
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex]; 
            list[randomIndex] = temp; 
        
        }

    }

}
