using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTree : MonoBehaviour
{
    public SkillNode StartNode { get; private set; }
    public SkillNode EndNode { get; private set; }

    public SkillTree(string startName, string endName)
    {
        StartNode = new SkillNode(startName);
        EndNode = new SkillNode(endName);
        StartNode.AddChild(EndNode);
    }

    public SkillNode AddSkill(string parentName, string skillName)
    {
        var parentNode = FindNode(StartNode, parentName);
        if (parentNode != null)
        {
            var newSkillNode = new SkillNode(skillName);
            parentNode.AddChild(newSkillNode);
            return  newSkillNode;
        }
        return null;
    }

    private SkillNode FindNode(SkillNode currentNode, string nodeName)
    {
        if (currentNode.Name == nodeName)
            return currentNode;
        
        foreach (var child in currentNode.Children)
        {
            var foundNode = FindNode(child, nodeName);
            if (foundNode != null) 
                return foundNode;
        }

        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
