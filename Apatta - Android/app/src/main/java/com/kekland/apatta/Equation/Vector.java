package com.kekland.apatta.Equation;

/**
 * Created by kkerz on 19-Dec-17.
 */

public class Vector {
    public float x;
    public float y;

    public Vector(float x, float y) {
        this.x = x;
        this.y = y;
    }
    public static Vector Add(Vector v1, Vector v2) {
        return new Vector(v1.x + v2.x, v1.y + v2.y);
    }
    public static Vector Subtract(Vector v1, Vector v2) {
        return new Vector(v1.x - v2.x, v1.y - v2.y);
    }

    public static Vector Multiply(Vector v1, float f) {
        return new Vector(v1.x * f, v1.y * f);
    }
}
