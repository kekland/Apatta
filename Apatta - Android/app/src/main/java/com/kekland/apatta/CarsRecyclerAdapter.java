package com.kekland.apatta;

import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
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
import java.util.prefs.PreferenceChangeEvent;

import uk.co.samuelwall.materialtaptargetprompt.MaterialTapTargetPrompt;
import uk.co.samuelwall.materialtaptargetprompt.extras.PromptFocal;
import uk.co.samuelwall.materialtaptargetprompt.extras.backgrounds.RectanglePromptBackground;
import uk.co.samuelwall.materialtaptargetprompt.extras.focals.RectanglePromptFocal;

public class CarsRecyclerAdapter extends RecyclerView.Adapter<CarsRecyclerAdapter.CarViewHolder> {

    final public List<Car> carsList;

    Context mCtx;
    public class CarViewHolder extends RecyclerView.ViewHolder {
        public TextView carName;
        public TextView topLeft, topRight, bottomLeft, bottomRight;
        public View editButton;
        public View view;
        public CarViewHolder(View view) {
            super(view);
            this.view = view;
            editButton = view.findViewById(R.id.carLayoutEditName);
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

    public CarsRecyclerAdapter(Context ctx) {
        mCtx = ctx;
        this.carsList = new ArrayList<>();
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
    public void onBindViewHolder(CarViewHolder holder, final int position) {
        Car car = carsList.get(position);
        holder.carName.setText(car.getName());
        holder.topLeft.setText(car.getDistanceTopLeft().toString());
        holder.topRight.setText(car.getDistanceTopRight().toString());
        holder.bottomRight.setText(car.getDistanceBottomRight().toString());
        holder.bottomLeft.setText(car.getDistanceBottomLeft().toString());

        holder.editButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ((CarsActivity)mCtx).EditCar(position, new CarsActivity.EditCarInterface() {
                    @Override
                    public void OnEdited(int index, String name) {
                        carsList.get(index).setName(name);
                        notifyDataSetChanged();
                    }
                });
            }
        });


        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(mCtx);
        if(prefs.getBoolean("ShowCardTutorial", true)) {
            new MaterialTapTargetPrompt.Builder((CarsActivity) mCtx)
                    .setTarget(holder.view)
                    .setPrimaryText("Управление")
                    .setSecondaryText("Проведите пальцем налево, чтобы удалить, или направо, чтобы составить протокол")
                    .setPromptFocal(new RectanglePromptFocal())
                    .setPromptBackground(new RectanglePromptBackground())
                    .show();
            prefs.edit().putBoolean("ShowCardTutorial", false).apply();
        }
    }

    @Override
    public int getItemCount() {
        return carsList.size();
    }
}