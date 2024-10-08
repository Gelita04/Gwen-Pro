using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedEffect
{
    public string name;
    private Expression ast;
    private VariableEnvironment env;
    public CodedEffect(string name, Expression ast, VariableEnvironment env)
    {
        this.name = name;
        this.ast = ast;
        this.env = env;
    }

    // public void Execute(List<Tuple<string, object>> effectParams)
    // {
    //     foreach (var param in effectParams)
    //     {
    //         Debug.Log("Setting variable: " + param.Item1 + " = " + param.Item2);
    //         env.SetVariable(param.Item1, param.Item2);
    //     }
    //     ast.Evaluate(env);
    // }

    public void Execute(List<Tuple<string, object>> effectParams, Dictionary<string, Tuple<string, Expression>> predicate = null)
    {
        var targets = new List<GameObject>();
        var newTargets = new List<GameObject>();
        var isTargetsFoundWithPredicate = false;

        foreach (var param in effectParams)
        {
            if (param.Item1 == "targets" && predicate != null)
            {
                targets = (List<GameObject>)param.Item2;
                isTargetsFoundWithPredicate = true;
                continue;
            }
            Debug.Log("Setting variable: " + param.Item1 + " = " + param.Item2);
            env.SetVariable(param.Item1, param.Item2);
        }

        if (isTargetsFoundWithPredicate && predicate != null)
        {
            foreach (var target in targets)
            {
                env.SetVariable(predicate[name].Item1, target);
                if ((bool)predicate[name].Item2.Evaluate(env))
                {
                    newTargets.Add(target);
                }
            }
            Debug.Log("Setting variable: " + "targets" + " = " + targets);
            env.SetVariable("targets", newTargets);
        }
        ast.Evaluate(env);
    }
}
