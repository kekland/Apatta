package com.kekland.apatta.Equation;

/**
 * Created by kkerz on 19-Dec-17.
 */

public class Equation {
    public Vector position1;
    public Vector position2;

    public float j, k, l;
    public float m, n, o;
    public float a, b, c, d;

    public float e, f;

    public Equation(Vector v1, Vector v2) {
        position1 = v1;
        position2 = v2;

        CalculateNecessaryValues();
    }

    public Equation() {}

    @Override
    public String toString() {
        String str = "";
        str += "y=" + Float.toString(e) + "x+" + Float.toString(f);
        return str;
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

    public Vector GetMidpoint()
    {
        return Vector.Multiply(Vector.Add(position1, position2), 0.5f);
    }
}
