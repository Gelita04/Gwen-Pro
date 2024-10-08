using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Debug.Log("Entro a un Evaluate");
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
        //if the variable to be assigned is a propierty of a Card,it is needed to use System.Reflection to alter the value of the propierty
        Debug.Log("Entro a Assignment");
        var evaluatedValue = value.Evaluate(env);
        // Check if the name contains a dot, indicating a property assignment
        if (name.Contains("."))
        {
            // Split the name into object name and property name
            var parts = name.Split('.');
            var objectName = parts[0];
            var fieldName = parts[1];
            var card = env.GetVariable(objectName);
            var tryTargetCast = card as GameObject;
            if (tryTargetCast != null)
            {
                //access the script component that has the property
                object script;
                if (tryTargetCast.CompareTag("Unit-Cards"))
                {
                    script = tryTargetCast.GetComponent<Unit_Card>();
                }
                else if (tryTargetCast.CompareTag("Buff"))
                {
                    script = tryTargetCast.GetComponent<Buff_Card>();
                }
                else if (tryTargetCast.CompareTag("Field"))
                {
                    script = tryTargetCast.GetComponent<Field_Card>();
                }
                else if (tryTargetCast.CompareTag("Counterfield"))
                {
                    script = tryTargetCast.GetComponent<Counterfield_Card>();
                }
                else if (tryTargetCast.CompareTag("Wildcard"))
                {
                    script = tryTargetCast.GetComponent<Wildcard>();
                }
                else
                {
                    throw new Exception("target is not a GameObject with a script component");
                }

                string temporalFieldName = fieldName;
                if (temporalFieldName == "Power")
                {
                    //check if the script is a Unit_Card,other way gives a error message
                    if (script is Unit_Card)
                        temporalFieldName = "Attack";
                    else
                        throw new Exception("target is not a Unit_Card");
                }
                else if (temporalFieldName == "Owner")
                    temporalFieldName = "team";

                //get the field
                var field = script.GetType().GetField(temporalFieldName);
                {
                    // Cast evaluatedValue to the appropriate type
                    if (field.FieldType == typeof(string))
                    {
                        field.SetValue(script, (string)evaluatedValue);
                    }
                    else if (field.FieldType == typeof(long))
                    {
                        field.SetValue(script, (long)evaluatedValue);
                    }
                    else
                    {
                        throw new Exception("Unsupported field type in Assignment class .The code with problem is: " + name);
                    }
                    return evaluatedValue;
                }
            }
            else
            {
                throw new Exception("target is not a GameObject with a script component in Assignment class .The code with problem is: " + name);
            }
        }
        else
        {
            // Handle normal variable assignment
            env.SetVariable(name, evaluatedValue);
        }
        Debug.Log("set variable: " + name + " value: " + evaluatedValue);
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
        Debug.Log("Entro a CompoundAssignment");

        // Check if the name contains a dot, indicating a field assignment
        if (name.Contains("."))
        {
            var evaluatedValue = value.Evaluate(env);

            // Split the name into object name and field name
            var parts = name.Split('.');
            var objectName = parts[0];
            var fieldName = parts[1];

            // Get the object from the environment
            var card = env.GetVariable(objectName);
            var tryTargetCast = card as GameObject;
            if (tryTargetCast != null)
            {
                // Access the script component that has the field
                object script;
                if (tryTargetCast.CompareTag("Unit-Cards"))
                {
                    script = tryTargetCast.GetComponent<Unit_Card>();
                }
                else if (tryTargetCast.CompareTag("Buff"))
                {
                    script = tryTargetCast.GetComponent<Buff_Card>();
                }
                else if (tryTargetCast.CompareTag("Field"))
                {
                    script = tryTargetCast.GetComponent<Field_Card>();
                }
                else if (tryTargetCast.CompareTag("Counterfield"))
                {
                    script = tryTargetCast.GetComponent<Counterfield_Card>();
                }
                else if (tryTargetCast.CompareTag("Wildcard"))
                {
                    script = tryTargetCast.GetComponent<Wildcard>();
                }
                else
                {
                    throw new Exception("target is not a GameObject with a script component");
                }

                string temporalFieldName = fieldName;
                if (temporalFieldName == "Power")
                {
                    //check if the script is a Unit_Card,other way gives a error message
                    if (script is Unit_Card)
                        temporalFieldName = "Attack";
                    else
                        throw new Exception("target is not a Unit_Card");
                }
                else if (temporalFieldName == "Owner")
                    temporalFieldName = "team";
                // Get the field
                var field = script.GetType().GetField(temporalFieldName);
                if (field != null)
                {
                    // Get the current value of the field
                    var currentValue = field.GetValue(script);

                    // Perform the compound assignment operation
                    switch (operation)
                    {
                        case "+=":
                            if (field.FieldType == typeof(long))
                            {
                                field.SetValue(script, (long)currentValue + Convert.ToInt64(evaluatedValue));
                            }
                            else
                                throw new Exception("Unsupported field type in CompoundAssignment class .The code with problem is: " + name);
                            break;
                        case "-=":
                            if (field.FieldType == typeof(long))
                            {
                                field.SetValue(script, (long)currentValue - Convert.ToInt64(evaluatedValue));
                            }
                            else
                                throw new Exception("Unsupported field type in CompoundAssignment class .The code with problem is: " + name);
                            break;
                        case "*=":
                            if (field.FieldType == typeof(long))
                            {
                                field.SetValue(script, (long)currentValue * Convert.ToInt64(evaluatedValue));
                            }
                            else
                                throw new Exception("Unsupported field type in CompoundAssignment class .The code with problem is: " + name);
                            break;
                        case "/=":
                            if (field.FieldType == typeof(long))
                            {
                                if (Convert.ToInt64(evaluatedValue) == 0)
                                {
                                    throw new DivideByZeroException("Division by zero in CompoundAssignment class .The code with problem is: " + name);
                                }
                                field.SetValue(script, (long)currentValue / Convert.ToInt64(evaluatedValue));
                            }
                            else
                                throw new Exception("Unsupported field type in CompoundAssignment class .The code with problem is: " + name);
                            break;
                        default:
                            throw new Exception("Unsupported field type in CompoundAssignment class .The code with problem is: " + name);
                    }
                    return currentValue;
                }
                else
                {
                    throw new Exception($"Field '{fieldName}' not found on script '{script.GetType().Name}'.");
                }
            }
            else
            {
                throw new Exception($"Variable '{objectName}' is not a GameObject.");
            }
        }
        else
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


}

//Member Access (dot operator)
public class MemberAccess : Expression
{
    private Expression obj;
    private string memberName;
    private Expression parameter;

    public MemberAccess(Expression obj, string memberName, Expression parameter)
    {
        this.obj = obj;
        this.memberName = memberName;
        this.parameter = parameter;
    }

    public override object Evaluate(VariableEnvironment env) //problema con los tipos
    {
        Debug.Log("Entro a MEMBERACCESS");
        // Try to get the method
        Debug.Log("memberName: " + memberName);
        var method = typeof(GameContext).GetMethod(memberName);
        if (method != null)
        {

            var evaluatedObj = obj.Evaluate(env);

            //cast evaluatedObj to GameContext
            var tryContextCast = evaluatedObj as GameContext;

            // Cast evaluatedObj to List<GameObject>
            var tryListOfGameObjectsCast = evaluatedObj as List<GameObject>;

            if (tryContextCast != null)
            {
                //castedObj is context and must be returned context.property or context.property(TriggerPlayer) form
                return method.Invoke(
                    env.GetVariable("context"),
                    new object[] { (string)env.GetVariable("TriggerPlayer") }
                );
            }
            else if (tryListOfGameObjectsCast != null)
            {
                if (parameter == null)
                {
                    return method.Invoke(
                        env.GetVariable("context"),
                        new object[] { tryListOfGameObjectsCast }
                    );
                }
                else
                {
                    Debug.Log("Entro a parameter != null");
                    Debug.Log("memberName: " + memberName);

                    var target = parameter.Evaluate(env);
                    Debug.Log(target);
                    var castedTarget = target as GameObject;
                    Debug.Log(castedTarget);
                    if (castedTarget == null)
                    {
                        throw new InvalidCastException("target is not a GameObject");
                    }
                    return method.Invoke(
                        env.GetVariable("context"),
                        new object[] { tryListOfGameObjectsCast, castedTarget }
                    );
                }
            }
            else
            {
                throw new InvalidCastException(
                    "evaluatedObj is not a GameContext or List<GameObject>"
                );
            }
        }
        else if (method == null)
        {
            var card = obj.Evaluate(env);
            var tryTargetCast = card as GameObject;
            if (tryTargetCast != null)
            {
                Debug.Log("Entro a tryTargetCast != null");
                //access the script component that has the property
                object script;
                if (tryTargetCast.CompareTag("Unit-Cards"))
                {
                    script = tryTargetCast.GetComponent<Unit_Card>();
                }
                else if (tryTargetCast.CompareTag("Buff"))
                {
                    script = tryTargetCast.GetComponent<Buff_Card>();
                }
                else if (tryTargetCast.CompareTag("Field"))
                {
                    script = tryTargetCast.GetComponent<Field_Card>();
                }
                else if (tryTargetCast.CompareTag("Counterfield"))
                {
                    script = tryTargetCast.GetComponent<Counterfield_Card>();
                }
                else if (tryTargetCast.CompareTag("Wildcard"))
                {
                    script = tryTargetCast.GetComponent<Wildcard>();
                }
                else
                {
                    throw new Exception("target is not a GameObject with a script component");
                }

                string temporalMemberName = memberName;
                if (temporalMemberName == "Power")
                {
                    //check if the script is a Unit_Card,other way gives a error message
                    if (script is Unit_Card)
                        temporalMemberName = "Attack";
                    else
                        throw new Exception("target is not a Unit_Card");
                }
                else if (temporalMemberName == "Owner" || temporalMemberName == "Faction")
                    temporalMemberName = "team";

                //get the field
                var field = script.GetType().GetField(temporalMemberName);
                if (field != null)
                {
                    return field.GetValue(script);
                }
                else
                {
                    throw new Exception($"Field '{memberName}' not found on script '{script.GetType().Name}'.");
                }
            }
            else
            {
                throw new Exception("Card is not a GameObject with a script component");
            }
        }
        else
        {
            throw new Exception($"Member {memberName} not found on Context");
        }
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
        var evaluatedCollection = collection.Evaluate(env) as List<GameObject>;
        if (evaluatedCollection == null)
        {
            Debug.Log("collection en el ForIn es null");
            return null; // ForIn loop does not return a value
        }
        Debug.Log("La cantidad de cartas en la collection del ForIn es " + evaluatedCollection.Count);

        //for debug reasons
        foreach (var item in evaluatedCollection)
        {
            Debug.Log("Entro a ForIn");
            Debug.Log("variable: " + variable);
            Debug.Log("item: " + item);
            env.SetVariable(variable, item);
            body.Evaluate(env);
        }
        return null; // ForIn loop does not return a value
    }
}

//predicate example: targets.Find((card) => card.Power > 100) returns the list filtered
public class Find : Expression
{
    public string variable;
    private Expression collection;
    public Expression condition;

    public Find(string variable, Expression collection, Expression condition)
    {
        this.variable = variable;
        this.collection = collection;
        this.condition = condition;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        var evaluatedCollection = collection.Evaluate(env) as IEnumerable<object>;
        if (evaluatedCollection == null)
        {
            return null;
        }
        List<object> filtered = new List<object>();
        foreach (var item in evaluatedCollection)
        {
            env.SetVariable(variable, item);
            if ((bool)condition.Evaluate(env))
            {
                filtered.Add(item);
            }
        }
        return filtered;
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
    public List<Expression> statements;

    public Block(List<Expression> statements)
    {
        this.statements = statements;
    }

    public override object Evaluate(VariableEnvironment env)
    {
        Debug.Log("Entro a block evaluate");
        Debug.Log("Cantidad de statments del block: " + statements.Count);
        foreach (var statement in statements)
        {
            Debug.Log(statement);

            statement.Evaluate(env);
        }
        return null;
    }
}
