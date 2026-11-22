using System;
using UnityEngine;


public enum EnemyActionType
{
    Chase,
    Patrol,
    Idle
}
public abstract class DecisionTreeNode
{
    public abstract DecisionTreeNode MakeDecision();
}


public class ActionNode : DecisionTreeNode
{
    public Ghost.State ActionType { get; private set; }

    public ActionNode(Ghost.State actionType)
    {
        this.ActionType = actionType;
    }
    public override DecisionTreeNode MakeDecision()
    {
        Debug.Log(ActionType.ToString());
        return this;
    }

}

public class DecisionNode : DecisionTreeNode
{
    private Func<bool> testValue;
    private DecisionTreeNode TrueNode;
    private DecisionTreeNode FalseNode;
    
    public DecisionNode(Func<bool> testValue, DecisionTreeNode trueNode, DecisionTreeNode falseNode)
    {
        this.testValue = testValue;
        TrueNode = trueNode;
        FalseNode = falseNode;
    }

    public override DecisionTreeNode MakeDecision()
    {
        DecisionTreeNode bracnh;

        if(testValue())
        {
            bracnh = TrueNode;
        }
        else
        {
            bracnh = FalseNode;
        }        
        return bracnh.MakeDecision();
    }
}


