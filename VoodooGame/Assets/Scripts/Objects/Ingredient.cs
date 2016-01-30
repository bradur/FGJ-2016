// Date   : 30.01.2016 00:07
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {

    [SerializeField]
    private AnimalType animalType;
    public AnimalType AnimalType { get { return animalType; } }

    [SerializeField]
    private AnimalState animalState;
    public AnimalState AnimalState { get { return animalState; } }

    [SerializeField]
    private GatheredType gatheredType;
    public GatheredType GatheredType { get { return gatheredType; } }

    [SerializeField]
    private GatheredState gatheredState;
    public GatheredState GatheredState { get { return gatheredState; } }

    void Start () {
    
    }

    void Update () {
    
    }
}

public enum AnimalType { None, Human, Cat, Dog, Chicken }
public enum AnimalState { None, Alive, Meat, Bone, Blood }
public enum GatheredType { None, Flower, Candle, Weed }
public enum GatheredState { None, Red, Blue, Green, Purple, White }