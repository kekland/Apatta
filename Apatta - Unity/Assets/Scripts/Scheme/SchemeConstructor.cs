using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class SchemeConstructor
{
    // Front left
    public Equation frontLeftEquation;
    public Equation frontLeftPerpendicularEquation;
     
    // Front right
    public Equation frontRightEquation;
    public Equation frontRightPerpendicularEquation;
     
    // Rear right
    public Equation rearRightEquation;
    public Equation rearRightPerpendicularEquation;
     
    // Rear left
    public Equation rearLeftEquation;
    public Equation rearLeftPerpendicularEquation;
     
    // Line
    public Equation lineEquation;
    public Equation linePerpendicularEquation;
     
    // Border
    public Equation borderEquation;
    public Equation borderPerpendicularEquation;

    public void SetEquation(int position, Vector2 p1, Vector2 p2)
    {
        Equation equation = new Equation(p1, p2);
        Equation perpendicular = EquationMath.GetPerpendicularEquationFromMidpoint(equation);

        if(position == 0)
        {
            rearLeftEquation = equation;
            rearLeftPerpendicularEquation = perpendicular;
        }
        else if(position == 1)
        {
            rearRightEquation = equation;
            rearRightPerpendicularEquation = perpendicular;
        }
        else if (position == 2)
        {
            frontRightEquation = equation;
            frontRightPerpendicularEquation = perpendicular;
        }
        else if (position == 3)
        {
            frontLeftEquation = equation;
            frontLeftPerpendicularEquation = perpendicular;
        }
        else if (position == 4)
        {
            lineEquation = equation;
            linePerpendicularEquation = perpendicular;
        }
        else if (position == 5)
        {
            borderEquation = equation;
            borderPerpendicularEquation = perpendicular;
        }
    }

    public Scheme Calculate()
    {
        //Determine which wheel side is closest to the border or/ line

        //Centers of left and right sides of car
        Vector2 leftSideCenter = EquationMath.GetCenterBetweenTwoEquations(rearLeftEquation, frontLeftEquation);
        Vector2 rightSideCenter = EquationMath.GetCenterBetweenTwoEquations(rearRightEquation, frontRightEquation);
        
        //Centers of border and lines
        Vector2 borderCenter = borderEquation.GetMidpoint();
        Vector2 lineCenter = lineEquation.GetMidpoint();

        //Distances from midpoints (not final distances)
        float distanceLeftToBorder = Vector2.SqrMagnitude(leftSideCenter - borderCenter);
        float distanceLeftToLine = Vector2.SqrMagnitude(leftSideCenter - lineCenter);
        float distanceRightToBorder = Vector2.SqrMagnitude(rightSideCenter - borderCenter);
        float distanceRightToLine = Vector2.SqrMagnitude(rightSideCenter - lineCenter);

        //Distance values
        float distanceRearLeft;
        float distanceRearRight;
        float distanceFrontRight;
        float distanceFrontLeft;

        if(distanceLeftToBorder > distanceRightToBorder && distanceLeftToLine < distanceRightToLine)
        {
            //Left is Line, Right is Border
            Debug.Log("Left is Line, Right is Border");
			//Calculate distances
			distanceRearLeft = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(rearLeftEquation.position1, rearLeftEquation.position2),
				lineEquation);

            distanceFrontLeft = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(frontLeftEquation.position1, frontLeftEquation.position2),
				lineEquation);

			distanceRearRight = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(rearRightEquation.position1, rearRightEquation.position2),
				borderEquation);

			distanceFrontRight = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(frontRightEquation.position1, frontRightEquation.position2),
				borderEquation);
		}
        else
        {
            //Right is Line, Left is Border
            Debug.Log("Right is Line, Left is Border");
			//Calculate distances
			distanceRearLeft = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(rearLeftEquation.position1, rearLeftEquation.position2),
				borderEquation);

			distanceFrontLeft = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(frontLeftEquation.position1, frontLeftEquation.position2),
				borderEquation);

			distanceRearRight = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(rearRightEquation.position1, rearRightEquation.position2),
				lineEquation);

			distanceFrontRight = EquationMath.GetDistanceBetweenWheelAndBorder(
				EquationMath.GetMidpointOfTwoVectors(frontRightEquation.position1, frontRightEquation.position2),
				lineEquation);
		}

        //Set up scheme object
        Scheme scheme = new Scheme();

        // Set distances
        scheme.distanceFrontLeft = distanceFrontLeft;
        scheme.distanceFrontRight = distanceFrontRight;
        scheme.distanceRearRight = distanceRearRight;
        scheme.distanceRearLeft = distanceRearLeft;

        // Set wheel points
        scheme.frontLeftWheel = frontLeftEquation.GetMidpoint();
        scheme.frontRightWheel = frontRightEquation.GetMidpoint();
        scheme.rearRightWheel = rearRightEquation.GetMidpoint();
        scheme.rearLeftWheel = rearLeftEquation.GetMidpoint();

        scheme.frontLeftWheelRotation = Mathf.Atan((frontLeftEquation.position2.y - frontLeftEquation.position1.y) /
                                                    (frontLeftEquation.position2.x - frontLeftEquation.position1.x)) * Mathf.Rad2Deg;

        scheme.frontRightWheelRotation = Mathf.Atan((frontRightEquation.position2.y - frontRightEquation.position1.y) /
                                                    (frontRightEquation.position2.x - frontRightEquation.position1.x)) * Mathf.Rad2Deg;

        scheme.rearRightWheelRotation = Mathf.Atan((rearRightEquation.position2.y - rearRightEquation.position1.y) /
                                                    (rearRightEquation.position2.x - rearRightEquation.position1.x)) * Mathf.Rad2Deg;

        scheme.rearLeftWheelRotation = Mathf.Atan((rearLeftEquation.position2.y - rearLeftEquation.position1.y) /
                                                    (rearLeftEquation.position2.x - rearLeftEquation.position1.x)) * Mathf.Rad2Deg;
        return scheme;
    }
}
