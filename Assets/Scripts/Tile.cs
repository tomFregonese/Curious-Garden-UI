using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomStruct {

public enum tileType {
    Adjacent, 
    Orthogonal, 
    Diagonal, 
    Joker, 
    Start, 
    Guard, 
    King, //?
    Ally
}

public struct Tile
{
    public tileType type; 
    public string imagePath; 
    public int availibleToPlaceOnTheField; 
    public int availibleInPlayersHand; 

    public Tile(tileType t, string iP, int ATPOTF, int AIPH){
        this.type = t; 
        this.imagePath = iP; 
        this.availibleToPlaceOnTheField = ATPOTF; 
        this.availibleInPlayersHand = AIPH;
    }

}
}