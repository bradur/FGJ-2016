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

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    [SerializeField]
    private float cost;
    public float Cost { get { return cost; } }

    void Start () {
    
    }

    void Update () {
    
    }
}

public enum AnimalType { None, Human, Cat, Dog, Chicken, Any }
public enum AnimalState { None, Alive, Meat, Bone, Blood, Any }
public enum GatheredType { None, Flower, Candle, Weed, Jewel, Any, AnyPlant }
public enum GatheredState { None, Red, Blue, Green, Purple, White, Any }