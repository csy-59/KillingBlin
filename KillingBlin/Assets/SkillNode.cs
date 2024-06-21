using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode : MonoBehaviour
{
    public string Name { get; set; }
    public List<SkillNode> Children { get; set; }
    public SkillNode Parent { get; set; }

    public SkillNode(string name)
    {
        Name = name;
        Children = new List<SkillNode>();
    }

    public void AddChild(SkillNode child)
    {
        Children.Add(child);
        child.Parent = this;
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
