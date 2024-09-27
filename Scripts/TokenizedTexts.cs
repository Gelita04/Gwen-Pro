using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TokenizedTexts : MonoBehaviour
{

    public string[] TokenizarCards(string entrada)
    {
        char[] delimitadores = new char[] { ' ', '\n', '\r', ',', '!', '?', ':', '\"' };

        // Divide el texto en tokens
        string[] tokens = entrada.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);

        // Separa los símbolos de puntuación en tokens individuales
        List<string> tokensList = new List<string>();
        foreach (var token in tokens)
        {
            tokensList.AddRange(SepararPuntuacion(token));
        }
        return tokensList.ToArray();
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
            '{',
            '}',
            '[',
            ']'
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
    public string[] TokenizarEffects(string effect)
    {
        // Definimos los delimitadores
        char[] delimiters = new char[] { '{', '}', '[', ']', '(', ')', ';', '.', '"', ',', ':', '=', '+', '-', '*', '/', '^', '%', '!', '?', ' ', '\n', '\r', '\"', '@', '>', '<' };
        List<string> tokens = new List<string>();

        int startIndex = 0;

        // Recorremos cada carácter en el string
        for (int i = 0; i < effect.Length; i++)
        {
            // Comprobamos si es un delimitador o un espacio
            if (Array.Exists(delimiters, d => d == effect[i]) || char.IsWhiteSpace(effect[i]))
            {
                // Si hay texto antes del delimitador o espacio, lo añadimos como token
                if (i > startIndex)
                {
                    tokens.Add(effect.Substring(startIndex, i - startIndex).Trim());
                }

                // Añadimos el delimitador como token si no es un espacio
                if (!char.IsWhiteSpace(effect[i]))
                {
                    tokens.Add(effect[i].ToString());
                }

                // Actualizamos el índice de inicio
                startIndex = i + 1;
            }
        }

        // Añadimos el último token si hay texto restante
        if (startIndex < effect.Length)
        {
            tokens.Add(effect.Substring(startIndex).Trim());
        }

        return tokens.ToArray();
    }
}