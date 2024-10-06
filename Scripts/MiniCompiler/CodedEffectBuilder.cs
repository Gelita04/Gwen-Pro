using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedEffectBuilder 
{ 
    public static CodedEffect BuildEffect(string[] tokens,string name)
    {
        //Debug.Log("Building effect: " + name);
        VariableEnvironment env = new VariableEnvironment();
        Expression ast = AST_Builder.Parse(tokens);
        //Debug.Log("AST: " + ast);
        CodedEffect codedEffect = new CodedEffect(name,ast,env);
        return codedEffect;
    }   
}
