using System;
using System.Collections.Generic;

using UnityEngine;

public class AST_Builder : MonoBehaviour
{
    public static Expression ParseExpression(string[] tokens, ref int index, bool calledFromWhile = false, bool calledFromFind = false)
    {
        var expressions = new Stack<Expression>();
        var operators = new Stack<string>();

        while (index < tokens.Length)
        {
            string token = tokens[index];
            index++;
            if (token == ";")
            {
                break;
            }
            else if (char.IsDigit(token[0]))
            {
                double number = double.Parse(token);
                expressions.Push(new Number(number));
            }
            else if (token == "(")
            {
                if (calledFromWhile)
                {
                    continue;
                }
                operators.Push(token);
            }
            else if (token == ")")
            {
                if (index < tokens.Length && ((tokens[index] == ";" && calledFromFind) || (tokens[index] == "{" && calledFromWhile)))//(supuestamente arreglado)posible error ya que si no es el ultimo parentisis,se parte fula pq no se termina de analizar la expression
                {
                    break;
                }
                while (operators.Count > 0 && operators.Peek() != "(")
                    ApplyOperator(expressions, operators.Pop());
                operators.Pop(); // quita '('
            }
            else if (token == "true" || token == "false")
            {
                expressions.Push(new Boolean(token == "true"));
            }
            else if (token[0] == '\"')
            {
                //remove the quotes
                string str = token.Substring(1, token.Length - 2);
                expressions.Push(new StringLiteral(str));
            }
            else if (token == ".")
            {
                //first check if this line is a assignment or compound assignment,if not,proceed
                if (index + 1 < tokens.Length && tokens[index + 1] == "=")
                {
                    int tempIndex = index;
                    index += 2; // Skip variable and "="
                    Expression value = ParseExpression(tokens, ref index);
                    string completeMemberAccessName = tokens[tempIndex - 2] + "." + tokens[tempIndex];
                    return new Assignment(completeMemberAccessName, value);
                }
                else if (
                    index + 1 < tokens.Length
                    && (
                        tokens[index + 1] == "+="
                        || tokens[index + 1] == "-="
                        || tokens[index + 1] == "*="
                        || tokens[index + 1] == "/="
                    )
                )
                {
                    int tempIndex = index;
                    index += 2; // Skip variable and operator
                    Expression value = ParseExpression(tokens, ref index);
                    string completeMemberAccessName = tokens[tempIndex - 2] + "." + tokens[tempIndex];
                    return new CompoundAssignment(completeMemberAccessName, tokens[tempIndex + 1], value);
                }
                else if (
                    index + 1 < tokens.Length
                    && (tokens[index + 1] == "++" || tokens[index + 1] == "--")
                )
                {
                    int tempIndex = index;
                    index += 2; // Skip variable and operator
                    string completeMemberAccessName = tokens[tempIndex - 2] + "." + tokens[tempIndex];
                    return new IncrementDecrement(completeMemberAccessName, tokens[tempIndex + 1]);
                }

                // Handle dot operator for member access
                string memberName = tokens[index];
                index++; // Move to the next token after the member name

                if (memberName == "Find")
                {
                    //predicate example: targets.Find((card) => card.Power > 100) returns the list filtered
                    index++; // Skip second "("
                    string variable = tokens[index];
                    index++; // Skip variable
                    index++; // skip ")"
                    index++; //skip =>
                    Expression condition = ParseExpression(tokens, ref index);
                    Expression collection = expressions.Pop();
                    expressions.Push(new Find(variable, collection, condition));
                }
                // Check if the next token is '(' indicating a method call
                else if (tokens[index] == "(")
                {
                    index++; // Move past '('
                    Expression paramExpression = null;
                    if (tokens[index] != ")")
                    {
                        paramExpression = ParseParam(tokens, ref index); //does not work if the expression does not end in ;
                        index--;//move to ')
                    }
                    index++; // Move past ')'
                    Expression obj = expressions.Pop();
                    expressions.Push(new MemberAccess(obj, memberName, paramExpression));
                }
                else
                {
                    Expression obj = expressions.Pop();
                    expressions.Push(new MemberAccess(obj, memberName, null));
                }
            }
            else if (
                token == "+"
                || token == "-"
                || token == "*"
                || token == "/"
                || token == "^"
                || token == "<"
                || token == ">"
                || token == ">="
                || token == "<="
                || token == "=="
                || token == "&&"
                || token == "||"
                || token == "@"
                || token == "@@"
            )
            {
                while (
                    operators.Count > 0
                    && BinOpPrecedence(operators.Peek()) >= BinOpPrecedence(token)
                )
                    ApplyOperator(expressions, operators.Pop());
                operators.Push(token);
            }
            else
            {
                Debug.Log("New Variable: " + token);
                expressions.Push(new Variable(token));
            }
        }
        while (operators.Count > 0)
            ApplyOperator(expressions, operators.Pop());

        return expressions.Pop();
    }

    private static int BinOpPrecedence(string op)
    {
        switch (op)
        {
            case "&&":
            case "||":
                return 0;
            case "<":
            case ">":
            case ">=":
            case "<=":
            case "==":
                return 1;
            case "+":
            case "-":
                return 2;
            case "*":
            case "/":
                return 3;
            case "^":
            case "@":
            case "@@":
                return 4;
            default:
                return 0;
        }
    }

    private static void ApplyOperator(Stack<Expression> expressions, string op)
    {
        Expression b = expressions.Pop(); // 2do operando
        Expression a = expressions.Pop(); // primer operando
        switch (op)
        {
            case "+":
                expressions.Push(new Add(a, b));
                break;
            case "-":
                expressions.Push(new Subtract(a, b));
                break;
            case "*":
                expressions.Push(new Multiply(a, b));
                break;
            case "/":
                expressions.Push(new Divide(a, b));
                break;
            case "^":
                expressions.Push(new Power(a, b));
                break;
            case "<":
                expressions.Push(new LessThan(a, b));
                break;
            case ">":
                expressions.Push(new GreaterThan(a, b));
                break;
            case ">=":
                expressions.Push(new GreaterThanOrEqual(a, b));
                break;
            case "<=":
                expressions.Push(new LessThanOrEqual(a, b));
                break;
            case "==":
                expressions.Push(new Equal(a, b));
                break;
            case "&&":
                expressions.Push(new And(a, b));
                break;
            case "||":
                expressions.Push(new Or(a, b));
                break;
            case "@":
                expressions.Push(new Concatenate(a, b));
                break;
            case "@@":
                expressions.Push(new ConcatenateWithSpace(a, b));
                break;
        }
    }

    public static Expression Parse(string[] tokens)
    {
        int index = 0;
        return ParseStatements(tokens, ref index);
    }

    public static Expression ParseStatements(string[] tokens, ref int index)
    {
        var result = new List<Expression>();
        while (index < tokens.Length)
        {
            if (tokens[index] == "for")
            {
                result.Add(ParseFor(tokens, ref index));
            }
            else if (tokens[index] == "}")
            {
                index++;
                break;
            }
            else if (index + 1 < tokens.Length && tokens[index + 1] == "=")
            {
                result.Add(ParseAssignment(tokens, ref index));
            }
            else if (tokens[index] == "while")
            {
                result.Add(ParseWhile(tokens, ref index));
            }
            else if (
                index + 1 < tokens.Length
                && (
                    tokens[index + 1] == "+="
                    || tokens[index + 1] == "-="
                    || tokens[index + 1] == "*="
                    || tokens[index + 1] == "/="
                )
            )
            {
                result.Add(ParseCompoundAssignment(tokens, ref index));
            }
            else if (
                index + 1 < tokens.Length
                && (tokens[index + 1] == "++" || tokens[index + 1] == "--")
            ) //limitacion de tener que aplicarse solo en una linea aparte. example: x++; o x--;
            {
                result.Add(ParseIncrementDecrement(tokens, ref index));
            }
            //saber si es una palabra
            else if (char.IsLetter(tokens[index][0]))
            {
                result.Add(ParseExpression(tokens, ref index));
            }
            else
            {
                index++;
            }
        }
        return new Block(result);
    }

    private static Expression ParseFor(string[] tokens, ref int index)
    {
        index++; // Skip "for"
        string variable = tokens[index++];
        index++; // Skip "in"
        Expression collection = new Variable(tokens[index++]);
        index++; // Skip "{"
        Expression body = ParseStatements(tokens, ref index);
        var debugProposite = body as Block;
        if (debugProposite != null)
            Debug.Log("el body del for in tiene " + debugProposite.statements.Count + " statments");
        return new ForIn(variable, collection, body);
    }

    private static Expression ParseAssignment(string[] tokens, ref int index)
    {
        string variable = tokens[index];
        index += 2; // Skip variable and "="
        Expression value = ParseExpression(tokens, ref index);
        return new Assignment(variable, value);
    }

    private static Expression ParseWhile(string[] tokens, ref int index)
    {
        index++; // Skip "while"
        Expression condition = ParseExpression(tokens, ref index, true);
        index++; // Skip "{"
        Expression body = ParseStatements(tokens, ref index);
        return new While(condition, body);
    }

    private static Expression ParseCompoundAssignment(string[] tokens, ref int index)
    {
        string variable = tokens[index];
        string op = tokens[index + 1];
        index += 2; // Skip variable and operator
        Expression value = ParseExpression(tokens, ref index);
        return new CompoundAssignment(variable, op, value);
    }

    private static Expression ParseIncrementDecrement(string[] tokens, ref int index)
    {
        string variable = tokens[index];
        string op = tokens[index + 1];
        index += 2; // Skip variable and operator
        Debug.Log("variable: " + variable + " op: " + op + "en ParseIncrementDecrement");
        return new IncrementDecrement(variable, op);
    }

    private static Expression ParseParam(string[] tokens, ref int index)
    {
        var expressions = new Stack<Expression>();

        while (index < tokens.Length)
        {
            string token = tokens[index];
            index++;

            if (token == ")")
            {
                break;
            }
            else if (token == ".")
            {
                // Handle dot operator for member access
                string memberName = tokens[index];
                index++; // Move to the next token after the member name

                // Check if the next token is '(' indicating a method call
                if (tokens[index] == "(")
                {
                    index++; // Move past '('
                    Expression paramExpression = null;
                    if (tokens[index] != ")")
                    {
                        paramExpression = ParseParam(tokens, ref index);
                    }
                    index++; // Move past ')'
                    Expression obj = expressions.Pop();
                    expressions.Push(new MemberAccess(obj, memberName, paramExpression));
                }
                else
                {
                    Expression obj = expressions.Pop();
                    expressions.Push(new MemberAccess(obj, memberName, null));
                }
            }
            else
            {
                expressions.Push(new Variable(token));
            }
        }

        return expressions.Pop();
    }
}
