package com.kekland.apatta;

import android.content.DialogInterface;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.EditText;

import java.util.ArrayList;
import java.util.List;

public class CarsActivity extends AppCompatActivity {

    CarsRecyclerAdapter adapter;
    RecyclerView recycler;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cars);

        recycler = findViewById(R.id.carsRecycler);

        LinearLayoutManager manager = new LinearLayoutManager(CarsActivity.this);
        recycler.setLayoutManager(manager);
        adapter = new CarsRecyclerAdapter();

        recycler.setAdapter(adapter);

        findViewById(R.id.carsFloatingActionButtonAdd).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(
                        CarsActivity.this);

                final View editLayout = CarsActivity.this.getLayoutInflater().inflate(R.layout.layout_add_car_dialog, null);

                ((EditText) editLayout.findViewById(R.id.layoutAddCarEditText)).addTextChangedListener(new TextWatcher() {
                    @Override
                    public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

                    }

                    @Override
                    public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

                    }

                    @Override
                    public void afterTextChanged(Editable editable) {
                        if (editable.length() == 0) {
                            editLayout.findViewById(R.id.layoutAddCarAdd).setClickable(false);
                        } else {
                            editLayout.findViewById(R.id.layoutAddCarAdd).setClickable(true);
                        }
                    }
                });

                // set title
                alertDialogBuilder.setTitle("Add new car");

                // set dialog message
                alertDialogBuilder
                        .setView(editLayout)
                        .setCancelable(false);

                // create alert dialog
                final AlertDialog alertDialog = alertDialogBuilder.create();

                // show it
                alertDialog.show();

                editLayout.findViewById(R.id.layoutAddCarAdd).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        String name =
                                ((EditText) editLayout.findViewById(R.id.layoutAddCarEditText)).getText().toString();
                        adapter.AddCar(new Car(name,
                                0f, 0f, 0f, 0f));
                        adapter.notifyDataSetChanged();
                        alertDialog.dismiss();
                    }
                });

                editLayout.findViewById(R.id.layoutAddCarCancel).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        alertDialog.dismiss();
                    }
                });
            }
        });

    }
}
