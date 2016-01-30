// Date   : 30.01.2016 15:54
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestRequirement : MonoBehaviour {

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

    [SerializeField]
    private string description= "Collect this item for me";
    public string Description { get { return description; } }

    public bool Check(Ingredient ingredient)
    {
        bool match = true;
        if (animalType == AnimalType.Any)
        {
            if(ingredient.AnimalType == AnimalType.None)
            {
                match = false;
            }
        }
        else if (animalType != ingredient.AnimalType)
        {
            match = false;
        }
        if (animalState == AnimalState.Any)
        {
            if (ingredient.AnimalState == AnimalState.None)
            {
                match = false;
            }
        }
        else if (animalState != ingredient.AnimalState)
        {
            match = false;
        }

        if (gatheredType == GatheredType.Any)
        {
            if (ingredient.GatheredType == GatheredType.None)
            {
                match = false;
            }
        }
        else if (gatheredType != ingredient.GatheredType)
        {
            match = false;
        }
        if (gatheredState == GatheredState.Any)
        {
            if (ingredient.GatheredState == GatheredState.None)
            {
                match = false;
            }
        }
        else if (gatheredState != ingredient.GatheredState)
        {
            match = false;
        }
        return match;
    }
}
