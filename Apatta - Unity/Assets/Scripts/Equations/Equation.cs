using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Equation
{
    public Vector2 position1;
    public Vector2 position2;

    public float
        j, // y1+y2=j
        k, //y1-y2=k
        l; //y1*y2=l
    public float
        m, // x1+x2=m
        n, // x1-x2=n
        o; // x1*x2=o
    public float
        a, // x1*y1=a
        b, // x2*y2=b
        c, // x1*y2=c
        d; // x2*y1=d

    public float e, f; // Analog of k and l in real equations

    public Equation(Vector2 pos1, Vector2 pos2)
    {
        position1 = pos1;
        position2 = pos2;

        CalculateNecessaryValues();
    }
    public Equation() { }

    public string GetFormula()
    {
        return "y=" + e.ToString() + "x+" + f.ToString();
    }

    void CalculateNecessaryValues()
    {
        j = position1.y + position2.y;
        k = position1.y - position2.y;
        l = position1.y * position2.y;

        m = position1.x + position2.x;
        n = position1.x - position2.x;
        o = position1.x * position2.x;

        a = position1.x * position1.y;
        b = position2.x * position2.y;
        c = position1.x * position2.y;
        d = position2.x * position1.y;

        e = (k / n);
        f = (c - d) / n;
    }

    public Vector2 GetMidpoint()
    {
        return (position1 + position2) / 2f;
    }
}