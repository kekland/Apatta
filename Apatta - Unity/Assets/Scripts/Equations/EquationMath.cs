using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class EquationMath
{
    public static Equation GetPerpendicularEquationFromMidpoint(Equation eq)
    {
        Equation perpendicular = new Equation();

        perpendicular.e = (-(eq.n / eq.k));
        perpendicular.f = ((-eq.k * eq.j) + (-eq.n * eq.m)) / (-2 * eq.k);

        return perpendicular;
    }

    public static Vector2 IntersectionBetweenEquations(Equation eq1, Equation eq2)
    {
        Vector2 intersection = new Vector2();

        intersection.x = (eq2.f - eq1.f) / (eq1.e - eq2.e);
        intersection.y = (eq1.e * intersection.x + eq1.f);

        return intersection;
    }

    public static Vector2 GetCenterBetweenTwoEquations(Equation eq1, Equation eq2)
    {
        return (eq1.GetMidpoint() + eq2.GetMidpoint()) / 2f;
    }

	public static float GetDistanceBetweenWheelAndBorder(Vector2 wheelMidpoint, Equation border)
	{
		float distance = 0f;

		distance = (Mathf.Abs(border.e * wheelMidpoint.x - wheelMidpoint.y + border.f)) / (Mathf.Sqrt(border.e * border.e + 1));

		return distance;
	}

	public static Vector2 GetMidpointOfTwoVectors(Vector2 v1, Vector2 v2)
	{
		return (v1 + v2) / 2f;
	}
}

