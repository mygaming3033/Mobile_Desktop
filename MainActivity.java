package com.example.teninagarderobamobile;

import androidx.appcompat.app.AppCompatActivity;
import android.content.Intent;
import android.os.Bundle;
import android.app.Activity;
import android.os.Bundle;

import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.app.Activity;
import android.view.Window;
import android.widget.TextView;
import android.widget.Toast;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;
import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;
public class MainActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE); 
        getSupportActionBar().hide();
        setContentView(R.layout.activity_main);
    }
    public static String messs;
    public static String username;
    public static String ime_k;
    public static String prezime_k;
    public static int id_k;
    public void login(View v)
    {
        EditText editText=findViewById(R.id.user);

         username=editText. getText(). toString();

        provjera(username);

    }
    public String getSomeVariable() {
        return username;
    }
    public void onregclick(View v){
        Intent intent = new Intent(this, Registration.class);
        startActivity(intent);
    }
    private void provjera(String usr) {
        String url="http://192.168.1.3/login.php";
        RequestQueue queue = Volley.newRequestQueue(MainActivity.this);
        Intent intent1 = new Intent(this, Main.class);
        StringRequest request = new StringRequest(Request.Method.POST, url, new com.android.volley.Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONObject jsonObject = new JSONObject(response);
                    if (jsonObject.getString("korIme") == null) {
                        Toast.makeText(MainActivity.this, "Please enter valid id.", Toast.LENGTH_SHORT).show();
                    } else {
                        messs=jsonObject.getString("korIme");
                       id_k=jsonObject.getInt("ajdi");
                       ime_k=jsonObject.getString("ime");
                       prezime_k=jsonObject.getString("prezime");
                        EditText pass=findViewById(R.id.pass);
                        String password=pass. getText(). toString();
                        if(messs.equals(password)){
                            Toast.makeText(MainActivity.this, "Uspješna prijava", Toast.LENGTH_SHORT).show();

                            startActivity(intent1);
                        }else{
                            Toast.makeText(MainActivity.this, "Nespješna prijava", Toast.LENGTH_SHORT).show();
                        }

                    }

                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new com.android.volley.Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(MainActivity.this, "Fail to get data: " + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            @Override
            public String getBodyContentType() {
                return "application/x-www-form-urlencoded; charset=UTF-8";
            }

            @Override
            protected Map<String, String> getParams() {

                Map<String, String> params = new HashMap<String, String>();

                params.put("id", usr);

                return params;
            }
        };
        queue.add(request);
    }

        }