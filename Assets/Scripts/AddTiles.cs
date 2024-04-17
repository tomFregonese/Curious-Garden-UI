using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTiles : MonoBehaviour {

    [SerializeField]
    private Transform gardenField;

    [SerializeField]
    private GameObject t; 

    void Awake(){
        for(int i=1; i<=20; i++){
            GameObject tile = Instantiate(t);
            tile.name = "Tile " + i; 
            tile.transform.SetParent(gardenField, false);
        }

    }
}
