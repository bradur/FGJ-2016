// Date   : 30.01.2016 21:54
// Project: VoodooGame

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour {

    [SerializeField]
    private string title;
    public string Title { get { return title; } }

    [SerializeField]
    [TextAreaAttribute(20, 4)]
    private string description;
    public string Description { get { return description; } }

    [SerializeField]
    [TextAreaAttribute(10, 4)]
    private string endDescription;
    public string EndDescription { get { return endDescription; } }

    [SerializeField]
    private List<QuestRequirement> requirements;
    public List<QuestRequirement> Requirements { get { return requirements; } }

}
