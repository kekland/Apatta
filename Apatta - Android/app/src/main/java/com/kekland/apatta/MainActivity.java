package com.kekland.apatta;

import android.content.Intent;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import com.google.ar.core.Anchor;
import com.google.ar.core.Session;

import uk.co.samuelwall.materialtaptargetprompt.MaterialTapTargetPrompt;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        findViewById(R.id.welcomeFloatingActionButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, CarsActivity.class);
                startActivity(intent);
            }
        });

        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
        if(prefs.getBoolean("FirstLaunchTutorial", true)) {
            new MaterialTapTargetPrompt.Builder(this)
                    .setTarget(findViewById(R.id.welcomeFloatingActionButton))
                    .setPrimaryText("Привет!")
                    .setSecondaryText("Нажми, если попал в аварию")
                    .show();
            prefs.edit().putBoolean("FirstLaunchTutorial", false).apply();
        }
    }
}
