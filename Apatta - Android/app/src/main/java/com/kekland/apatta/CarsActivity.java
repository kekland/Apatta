package com.kekland.apatta;

import android.app.Activity;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.support.design.widget.Snackbar;
import android.support.design.widget.TextInputLayout;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.helper.ItemTouchHelper;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.EditText;

import java.util.ArrayList;
import java.util.List;

import uk.co.samuelwall.materialtaptargetprompt.MaterialTapTargetPrompt;

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
        adapter = new CarsRecyclerAdapter(this);

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
                            editLayout.findViewById(R.id.layoutAddCarAdd).setEnabled(false);
                            ((TextInputLayout)editLayout.findViewById(R.id.layoutAddCarEditTextLayout))
                                    .setError("Name of car cannot be empty");
                        } else {
                            editLayout.findViewById(R.id.layoutAddCarAdd).setEnabled(true);
                            ((TextInputLayout)editLayout.findViewById(R.id.layoutAddCarEditTextLayout))
                                    .setError("");
                        }
                    }
                });

                // set title
                alertDialogBuilder.setTitle("Add new car");

                // set dialog message
                alertDialogBuilder
                        .setView(editLayout)
                        .setCancelable(true);

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

        ItemTouchHelper.Callback callback = new ItemTouchHelper.Callback() {
            @Override
            public int getMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
                return ItemTouchHelper.Callback.makeMovementFlags(0,
                        ItemTouchHelper.LEFT | ItemTouchHelper.RIGHT);
            }

            @Override
            public boolean onMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
                return false;
            }

            @Override
            public void onSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
                if(direction == ItemTouchHelper.LEFT) {
                    //Delete
                    adapter.RemoveCar(viewHolder.getAdapterPosition());
                }
                else {
                    adapter.notifyItemChanged(viewHolder.getAdapterPosition());
                    Intent intent = new Intent(CarsActivity.this, ARActivity.class);
                    startActivityForResult(intent, ARActivity.GET_DISTANCES);
                }
            }
        };

        ItemTouchHelper helper = new ItemTouchHelper(callback);

        helper.attachToRecyclerView(recycler);

        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
        if(prefs.getBoolean("ShowCarsTutorial", true)) {
            new MaterialTapTargetPrompt.Builder(this)
                    .setTarget(findViewById(R.id.carsFloatingActionButtonAdd))
                    .setPrimaryText("Добавить машину")
                    .setSecondaryText("Нажми чтобы добавить машину, попавшую в аварию")
                    .show();
            prefs.edit().putBoolean("ShowCarsTutorial", false).apply();
        }
    }

    public interface EditCarInterface {
        void OnEdited(int index, String name);
    }
    public void EditCar(final int index, final EditCarInterface callback) {
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
                    editLayout.findViewById(R.id.layoutAddCarAdd).setEnabled(false);
                    ((TextInputLayout)editLayout.findViewById(R.id.layoutAddCarEditTextLayout))
                            .setError("Name of car cannot be empty");
                } else {
                    editLayout.findViewById(R.id.layoutAddCarAdd).setEnabled(true);
                    ((TextInputLayout)editLayout.findViewById(R.id.layoutAddCarEditTextLayout))
                            .setError("");
                }
            }
        });

        // set title
        alertDialogBuilder.setTitle("Edit car name");

        // set dialog message
        alertDialogBuilder
                .setView(editLayout)
                .setCancelable(true);

        // create alert dialog
        final AlertDialog alertDialog = alertDialogBuilder.create();

        // show it
        alertDialog.show();

        editLayout.findViewById(R.id.layoutAddCarAdd).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            String name = ((EditText) editLayout.findViewById(R.id.layoutAddCarEditText)).getText().toString();
            callback.OnEdited(index, name);
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
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode == Activity.RESULT_OK) {
            String message = data.getStringExtra("message");
            if(message != null) {
                Snackbar.make(findViewById(android.R.id.content), message, Snackbar.LENGTH_INDEFINITE).show();
            }
        }
    }
}
