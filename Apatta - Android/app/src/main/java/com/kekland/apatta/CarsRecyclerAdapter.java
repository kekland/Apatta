package com.kekland.apatta;

import android.support.v7.widget.RecyclerView;

/**
 * Created by kkerz on 19-Dec-17.
 */

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

public class CarsRecyclerAdapter extends RecyclerView.Adapter<CarsRecyclerAdapter.CarViewHolder> {

    public List<Car> carsList;

    public class CarViewHolder extends RecyclerView.ViewHolder {
        public TextView carName;
        public TextView topLeft, topRight, bottomLeft, bottomRight;
        public CarViewHolder(View view) {
            super(view);

            carName = view.findViewById(R.id.carNameText);
            topLeft = view.findViewById(R.id.topLeftDistanceText);
            topRight = view.findViewById(R.id.topRightDistanceText);
            bottomLeft = view.findViewById(R.id.bottomLeftDistanceText);
            bottomRight = view.findViewById(R.id.bottomRightDistanceText);
        }
    }

    public void RemoveCar(int index) {
        carsList.remove(index);
        notifyItemRemoved(index);
    }

    public CarsRecyclerAdapter() {
        this.carsList = new ArrayList<>();
        this.carsList.add(new Car("abcd", 0f, 0f, 0f, 0f));
    }

    public void AddCar(Car car) {
        carsList.add(car);
    }
    @Override
    public CarViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.layout_car, parent, false);

        return new CarViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(CarViewHolder holder, int position) {
        Car car = carsList.get(position);
        holder.carName.setText(car.getName());
        holder.topLeft.setText(car.getDistanceTopLeft().toString());
        holder.topRight.setText(car.getDistanceTopRight().toString());
        holder.bottomRight.setText(car.getDistanceBottomRight().toString());
        holder.bottomLeft.setText(car.getDistanceBottomLeft().toString());
    }

    @Override
    public int getItemCount() {
        return carsList.size();
    }
}