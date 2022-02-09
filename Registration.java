package com.example.teninagarderobamobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;

import android.view.View;
import android.widget.EditText;
import android.view.Window;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import com.android.volley.RequestQueue;
import com.android.volley.toolbox.Volley;


public class Registration extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE); 
        getSupportActionBar().hide();
        setContentView(R.layout.activity_registration);
    }
    public static String test="e";
    public void reg(View v){

        EditText ime=findViewById(R.id.upIme);
        EditText prezime=findViewById(R.id.upPrezime);
        EditText username=findViewById(R.id.upUsername);
        EditText password1=findViewById(R.id.upPass1);
        EditText password2=findViewById(R.id.upPass2);
        String sime,sprezime,spassword1,spassword2,susername;
        sime=ime.getText().toString();
        sprezime=prezime.getText().toString();
        susername=username.getText().toString();
        spassword1=password1.getText().toString();
        spassword2=password2.getText().toString();
        try{
            if(sime.isEmpty() || sprezime.isEmpty() || susername.isEmpty()|| spassword1.isEmpty() || spassword2.isEmpty()){
                throw new Exception("Nisu uneseni svi podaci");
            }
            if(!(spassword1.equals(spassword2))){
                throw new Exception("Unesene lozinke se ne podudaraju");
            }
            Registracija(sime,sprezime,susername,spassword1);
        }catch (Exception ex){
            Toast.makeText(Registration.this, ex.getMessage(), Toast.LENGTH_SHORT).show();
        }
    }
    private void Registracija(String ime, String prezime, String usr,String pass) {


        String url="http://192.168.1.3/register.php";


        RequestQueue queue = Volley.newRequestQueue(Registration.this);

        StringRequest request = new StringRequest(Request.Method.POST, url, new com.android.volley.Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONObject jsonObject = new JSONObject(response);
  
                    Toast.makeText(Registration.this, jsonObject.getString("message"), Toast.LENGTH_SHORT).show();
                } catch (JSONException e) {
                    e.printStackTrace();
                }

            }
        }, new com.android.volley.Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(Registration.this, "Fail to get response = " + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            @Override
            public String getBodyContentType() {
                return "application/x-www-form-urlencoded; charset=UTF-8";
            }

            @Override
            protected Map<String, String> getParams() {

                Map<String, String> params = new HashMap<String, String>();


                params.put("ime", ime);
                params.put("prezime", prezime);
                params.put("username", usr);
                params.put("password", pass);

                return params;
            }
        };
        queue.add(request);
    }
}