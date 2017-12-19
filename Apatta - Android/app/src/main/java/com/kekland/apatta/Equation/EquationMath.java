package com.kekland.apatta.Equation;

/**
 * Created by kkerz on 19-Dec-17.
 */

public class EquationMath {
    public static Equation GetPerpendicularEquationFromMidpoint(Equation eq)
    {
        Equation perpendicular = new Equation();

        perpendicular.e = (-(eq.n / eq.k));
        perpendicular.f = ((-eq.k * eq.j) + (-eq.n * eq.m)) / (-2 * eq.k);

        return perpendicular;
    }

    public static Vector IntersectionBetweenEquations(Equation eq1, Equation eq2)
    {
        Vector intersection = new Vector(0f, 0f);

        intersection.x = (eq2.f - eq1.f) / (eq1.e - eq2.e);
        intersection.y = (eq1.e * intersection.x + eq1.f);

        return intersection;
    }

    public static Vector GetCenterBetweenTwoEquations(Equation eq1, Equation eq2)
    {
        return Vector.Multiply(Vector.Add(eq1.GetMidpoint(), eq2.GetMidpoint()), 0.5f);
    }

    public static float GetDistanceBetweenWheelAndBorder(Vector wheelMidpoint, Equation border)
    {
        float distance = 0f;

        distance = (float)(Math.abs(border.e * wheelMidpoint.x - wheelMidpoint.y + border.f)) / (float)(Math.sqrt(border.e * border.e + 1));

        return distance;
    }

    public static Vector GetMidpointOfTwoVectors(Vector v1, Vector v2)
    {
        return Vector.Multiply(Vector.Add(v1, v2), 0.5f);
    }
}
