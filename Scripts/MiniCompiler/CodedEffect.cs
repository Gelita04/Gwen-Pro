using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedEffect 
{
    public string name;
    private Expression  ast;
    private VariableEnvironment env;
    public CodedEffect(string name, Expression ast, VariableEnvironment env)
    {
        this.name = name;
        this.ast = ast;
        this.env = env;
    }

    public void Execute(List<Tuple<string,object>> effectParams)
    {
        foreach (var param in effectParams)
        {Debug.Log("Setting variable: " + param.Item1 + " = " + param.Item2);
            env.SetVariable(param.Item1, param.Item2);
        }
        ast.Evaluate(env);
    }
}
