using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TokenizedTexts : MonoBehaviour
{

    public List<string> Tokenizar(string entrada)
    {
        char[] delimitadores = new char[] { ' ', '\n', '\r', ',', '!', '?', ':', '\"', '{', '}','[',
            ']' };

        // Divide el texto en tokens
        string[] tokens = entrada.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

        // Separa los símbolos de puntuación en tokens individuales
        List<string> tokensList = new List<string>();
        foreach (var token in tokens)
        {
            tokensList.AddRange(SepararPuntuacion(token));

        }
        return tokensList;
    }

    public string[] SepararPuntuacion(string input)
    {
        // Define los símbolos de puntuación a separar
        char[] puntuacion = new char[]
        {
            ',',
            '.',
            '!',
            '?',
            ';',
            ':',
            '(',
            ')',

        };

        // Crea una lista para almacenar los resultados
        List<string> resultado = new List<string>();

        // Separa el token en subtokens
        string[] subtokens = input.Split(puntuacion, StringSplitOptions.RemoveEmptyEntries);

        // Agrega los subtokens a la lista de resultados
        foreach (var subtoken in subtokens)
        {
            resultado.Add(subtoken.Trim()); // Agregar el subtoken
        }

        // Agrega los símbolos de puntuación que estaban en el input original
        foreach (var c in input)
        {
            if (puntuacion.Contains(c))
            {
                resultado.Add(c.ToString()); // Agrega el símbolo de puntuación como token
            }
        }

        return resultado.ToArray();
    }
    public List<List<string>> TokenizarCards(List<string> listTokens)
    {
        List<List<string>> result = new List<List<string>>();
        List<string> temp = null;


        for (int i = 0; i < listTokens.Count; i++)
        {
            if (listTokens[i] == "card")
            {
                if (temp != null)
                {
                    result.Add(temp);
                }
                temp = new List<string>();
            }

            if (temp != null)
            {
                temp.Add(listTokens[i]); // Agregar el token actual a la lista temporal
            }
        }

        if (temp != null)
        {
            result.Add(temp); // Agregar la última lista temporal a la lista de listas
        }

        return result;
    }
    public List<Tuple<string, List<string>>> TokenizarEffects(string effects)
    {
        // Split the input string into individual effects based on the keyword "effect"
        // string[] individualEffects = effects.Split(
        //     new[] { "effect" },
        //     StringSplitOptions.RemoveEmptyEntries
        // );
        string[] individualEffects = effects
        .Split(new[] { "effect" }, StringSplitOptions.RemoveEmptyEntries)
        .Select(e => e.Trim())
        .Where(e => !string.IsNullOrWhiteSpace(e))
        .ToArray();

        // Define the delimiters
        char[] delimiters = new char[]
        {
            '{',
            '}',
            '[',
            ']',
            '(',
            ')',
            ';',
            '.',
            '"',
            ',',
            ':',
            '*',
            '/',
            '^',
            '%',
            '!',
            '?',
            ' ',
            '\n',
            '\r',
            '\"',
            '+',
            '-',
            '<',
            '>',
            '=',
            '@',
        };

        // Define the multi-character tokens
        string[] multiCharTokens = new string[]
        {
            "++",
            "--",
            ">=",
            "<=",
            "==",
            "||",
            "&&",
            "@@",
            "+=",
            "-=",
            "*=",
            "/=",
            "=>"
        };

        // List to hold the list of tokens for each effect
        List<Tuple<string, List<string>>> tokensList = new List<Tuple<string, List<string>>>();

        Debug.Log("Cantidad de efectos: " + individualEffects.Length);
        //Debug.Log("los efectos que hay son " + string.Join("\\EFFECT\\ ", individualEffects));
        // Process each effect
        foreach (var effect in individualEffects)
        {
            List<string> tokens = new List<string>();
            int startIndex = 0;

            // Tokenize the effect
            for (int i = 0; i < effect.Length; i++)
            {
                // Check for multi-character tokens
                bool isMultiCharToken = false;
                foreach (var token in multiCharTokens)
                {
                    if (
                        i + token.Length <= effect.Length
                        && effect.Substring(i, token.Length) == token
                    )
                    {
                        // If there is text before the multi-character token, add it as a token
                        if (i > startIndex)
                        {
                            tokens.Add(effect.Substring(startIndex, i - startIndex).Trim());
                        }

                        // Add the multi-character token
                        tokens.Add(token);
                        i += token.Length - 1;
                        startIndex = i + 1;
                        isMultiCharToken = true;
                        break;
                    }
                }

                if (isMultiCharToken)
                {
                    continue;
                }

                // Check if the character is a delimiter or whitespace
                if (Array.Exists(delimiters, d => d == effect[i]) || char.IsWhiteSpace(effect[i]))
                {
                    // If there is text before the delimiter or whitespace, add it as a token
                    if (i > startIndex)
                    {
                        tokens.Add(effect.Substring(startIndex, i - startIndex).Trim());
                    }

                    // Add the delimiter as a token if it is not whitespace
                    if (!char.IsWhiteSpace(effect[i]))
                    {
                        tokens.Add(effect[i].ToString());
                    }

                    // Update the start index
                    startIndex = i + 1;
                }
            }

            // Add the last token if there is remaining text
            if (startIndex < effect.Length)
            {
                tokens.Add(effect.Substring(startIndex).Trim());
            }

            // Extract the name of the effect
            int nameIndex = tokens.IndexOf("Name");
            string effectName = "";
            if (nameIndex != -1 && nameIndex + 3 < tokens.Count)
            {
                effectName = tokens[nameIndex + 3].Trim(' ');
                Debug.Log("el nombre del efecto es " + effectName);
            }

            // Remove tokens from the start to the second quote
            int firstQuoteIndex = tokens.IndexOf("\"");
            int secondQuoteIndex = tokens.IndexOf("\"", firstQuoteIndex + 1);
            if (firstQuoteIndex != -1 && secondQuoteIndex != -1)
            {
                tokens.RemoveRange(0, secondQuoteIndex + 1);
            }

            // Remove tokens from "Params" to "Action" (excluding "Action")
            int paramsIndex = tokens.IndexOf("Params");
            int actionIndex = tokens.IndexOf("Action");

            if (paramsIndex != -1 && actionIndex != -1 && paramsIndex < actionIndex)
            {
                tokens.RemoveRange(paramsIndex, actionIndex - paramsIndex);
            }

            // Remove tokens from "Action" to "=>" (including "=>")
            actionIndex = tokens.IndexOf("Action");
            int arrowIndex = tokens.IndexOf("=>");

            if (actionIndex != -1 && arrowIndex != -1 && actionIndex < arrowIndex)
            {
                tokens.RemoveRange(actionIndex, arrowIndex - actionIndex + 1);
            }

            // Remove the "Action" token itself if it exists
            actionIndex = tokens.IndexOf("Action");
            if (actionIndex != -1)
            {
                tokens.RemoveAt(actionIndex);
            }

            // Add the tuple of effect name and tokens for this effect to the main list
            tokensList.Add(new Tuple<string, List<string>>(effectName, tokens));
        }

        return tokensList;
    }
    public Dictionary<string, string> GetEffectsWithPredicate(string text)
    {
        Dictionary<string, string> effectsDictionary = new Dictionary<string, string>();
        throw new NotImplementedException();

    }
    public string[] TokenizarPredicate(string text)
    {
        throw new NotImplementedException();
    }

}

