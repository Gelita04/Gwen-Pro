using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all expressions
public abstract class Expression
{
    public abstract object Evaluate(VariableEnvironment env);
}

// Environment to store variable values
public class VariableEnvironment
{
    private Dictionary<string, object> variables = new Dictionary<string, object>();

    public object GetVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            return variables[name];
        }
        throw new Exception($"Variable '{name}' not found");
    }

    public void SetVariable(string name, object value)
    {
        variables[name] = value;
    }

    public bool IsVariable(string name)
    {
        return variables.ContainsKey(name);
    }
}

// Arithmetic operations
public class Add : Expression
{
    private Expression left,
        right;

    public Add(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) + (double)right.Evaluate(env);
    }
}

public class Subtract : Expression
{
    private Expression left,
        right;

    public Subtract(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) - (double)right.Evaluate(env);
    }
}

public class Multiply : Expression
{
    private Expression left,
        right;

    public Multiply(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) * (double)right.Evaluate(env);
    }
}

public class Divide : Expression
{
    private Expression left,
        right;

    public Divide(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) / (double)right.Evaluate(env);
    }
}

public class Power : Expression
{
    private Expression left,
        right;

    public Power(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return Math.Pow((double)left.Evaluate(env), (double)right.Evaluate(env));
    }
}

// Logical operations
public class And : Expression
{
    private Expression left,
        right;

    public And(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (bool)left.Evaluate(env) && (bool)right.Evaluate(env);
    }
}

public class Or : Expression
{
    private Expression left,
        right;

    public Or(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (bool)left.Evaluate(env) || (bool)right.Evaluate(env);
    }
}

// Comparison operations
public class LessThan : Expression
{
    private Expression left,
        right;

    public LessThan(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) < (double)right.Evaluate(env);
    }
}

public class GreaterThan : Expression
{
    private Expression left,
        right;

    public GreaterThan(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) > (double)right.Evaluate(env);
    }
}

public class LessThanOrEqual : Expression
{
    private Expression left,
        right;

    public LessThanOrEqual(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) <= (double)right.Evaluate(env);
    }
}

public class GreaterThanOrEqual : Expression
{
    private Expression left,
        right;

    public GreaterThanOrEqual(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (double)left.Evaluate(env) >= (double)right.Evaluate(env);
    }
}

public class Equal : Expression
{
    private Expression left,
        right;

    public Equal(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return left.Evaluate(env).Equals(right.Evaluate(env));
    }
}

// String concatenation operations
public class Concatenate : Expression
{
    private Expression left,
        right;

    public Concatenate(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (string)left.Evaluate(env) + (string)right.Evaluate(env);
    }
}

public class ConcatenateWithSpace : Expression
{
    private Expression left,
        right;

    public ConcatenateWithSpace(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return (string)left.Evaluate(env) + " " + (string)right.Evaluate(env);
    }
}

// Literal values
public class Number : Expression
{
    private double value;

    public Number(double value)
    {
        this.value = value;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return value;
    }
}

public class Boolean : Expression
{
    private bool value;

    public Boolean(bool value)
    {
        this.value = value;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return value;
    }
}

public class StringLiteral : Expression
{
    private string value;

    public StringLiteral(string value)
    {
        this.value = value;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return value;
    }
}

// Variables
public class Variable : Expression
{
    private string name;

    public Variable(string name)
    {
        this.name = name;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        return env.GetVariable(name);
    }
}

// Assignment
public class Assignment : Expression
{
    private string name;
    private Expression value;

    public Assignment(string name, Expression value)
    {
        this.name = name;
        this.value = value;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        var evaluatedValue = value.Evaluate(env);
        env.SetVariable(name, evaluatedValue);
        return evaluatedValue;
    }
}

public class CompoundAssignment : Expression
{
    private string name;
    private string operation;
    private Expression value;

    public CompoundAssignment(string name, string operation, Expression value)
    {
        this.name = name;
        this.operation = operation;
        this.value = value;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        double evaluatedValue = (double)env.GetVariable(name);
        double newValue = (double)value.Evaluate(env);
        switch (operation)
        {
            case "+=":
                evaluatedValue += newValue;
                break;
            case "-=":
                evaluatedValue -= newValue;
                break;
            case "*=":
                evaluatedValue *= newValue;
                break;
            case "/=":
                evaluatedValue /= newValue;
                break;
        }
        env.SetVariable(name, evaluatedValue);
        return evaluatedValue;
    }
}

// Increment and Decrement
public class IncrementDecrement : Expression
{
    private string name;
    private string operation;

    public IncrementDecrement(string name, string operation)
    {
        this.name = name;
        this.operation = operation;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        double evaluatedValue = (double)env.GetVariable(name);
        switch (operation)
        {
            case "++":
                evaluatedValue++;
                break;
            case "--":
                evaluatedValue--;
                break;
        }
        env.SetVariable(name, evaluatedValue);
        return evaluatedValue;
    }
}
// While loop
public class While : Expression
{
    private Expression condition,
        body;

    public While(Expression condition, Expression body)
    {
        this.condition = condition;
        this.body = body;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        while ((bool)condition.Evaluate(env))
        {
            body.Evaluate(env);
        }
        return null; // While loop does not return a value
    }
}

// ForIn loop
public class ForIn : Expression
{
    private string variable;
    private Expression collection,
        body;

    public ForIn(string variable, Expression collection, Expression body)
    {
        this.variable = variable;
        this.collection = collection;
        this.body = body;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        var evaluatedCollection = collection.Evaluate(env) as IEnumerable<object>;
        if (evaluatedCollection == null)
        {
            return null; // ForIn loop does not return a value
        }

        foreach (var item in evaluatedCollection)
        {
            env.SetVariable(variable, item);
            body.Evaluate(env);
        }
        return null; // ForIn loop does not return a value
    }
}

// Indexing
public class Index : Expression
{
    private Expression collection,
        index;

    public Index(Expression collection, Expression index)
    {
        this.collection = collection;
        this.index = index;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        var evaluatedCollection = collection.Evaluate(env) as IList<object>;
        if (evaluatedCollection == null)
        {
            throw new Exception("Collection is not a list");
        }

        var evaluatedIndex = (int)index.Evaluate(env);
        return evaluatedCollection[evaluatedIndex];
    }
}

// Block expression to hold multiple statements
public class Block : Expression
{
    private List<Expression> statements;

    public Block(List<Expression> statements)
    {
        this.statements = statements;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        foreach (var statement in statements)
        {
            statement.Evaluate(env);
        }
        return null;
    }
}
