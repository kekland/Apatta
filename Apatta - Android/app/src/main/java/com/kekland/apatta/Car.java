package com.kekland.apatta;

/**
 * Created by kkerz on 19-Dec-17.
 */

public class Car {
    String Name;
    Float DistanceTopLeft;
    Float DistanceTopRight;
    Float DistanceBottomLeft;

    Boolean edited;

    public Boolean getEdited() {
        return edited;
    }

    public void setEdited(Boolean edited) {
        this.edited = edited;
    }

    public Car(String name, Float distanceTopLeft, Float distanceTopRight, Float distanceBottomLeft, Float distanceBottomRight) {
        Name = name;
        DistanceTopLeft = distanceTopLeft;
        DistanceTopRight = distanceTopRight;
        DistanceBottomLeft = distanceBottomLeft;
        DistanceBottomRight = distanceBottomRight;
        setEdited(false);
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public Float getDistanceTopLeft() {
        return DistanceTopLeft;
    }

    public void setDistanceTopLeft(Float distanceTopLeft) {
        DistanceTopLeft = distanceTopLeft;
    }

    public Float getDistanceTopRight() {
        return DistanceTopRight;
    }

    public void setDistanceTopRight(Float distanceTopRight) {
        DistanceTopRight = distanceTopRight;
    }

    public Float getDistanceBottomLeft() {
        return DistanceBottomLeft;
    }

    public void setDistanceBottomLeft(Float distanceBottomLeft) {
        DistanceBottomLeft = distanceBottomLeft;
    }

    public Float getDistanceBottomRight() {
        return DistanceBottomRight;
    }

    public void setDistanceBottomRight(Float distanceBottomRight) {
        DistanceBottomRight = distanceBottomRight;
    }

    Float DistanceBottomRight;
}
