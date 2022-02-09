package com.example.teninagarderobamobile;
import android.os.Bundle;
import java.lang.annotation.Annotation;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

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

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
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


public class HomeFragment extends Fragment {
    List<Zahtjev> zahtjevi;
    RecyclerView recyclerView;
    private JSONArray result1;
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view =  inflater.inflate(R.layout.fragment_home,
                container, false);
        TextView ime = view.findViewById(R.id.textViewIme);
        TextView prezime = view.findViewById(R.id.textViewPrezime);
        TextView kb = view.findViewById(R.id.textViewID);
        ime.setText(MainActivity.ime_k);
        prezime.setText(MainActivity.prezime_k);
        kb.setText(String.valueOf(MainActivity.id_k));
        recyclerView = view.findViewById(R.id.tabela);
        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        zahtjevi = new ArrayList<>();

        loadZahtjevi();
        return view;
    }

    private void loadZahtjevi() {
        String url = "http://192.168.1.3/lzahtjev.php";
        StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new com.android.volley.Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                JSONObject j = null;
                try {
                    j = new JSONObject(response);

                    result1 = j.getJSONArray("result");

                    dothat(result1);


                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new com.android.volley.Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getContext(), "Fail to get data: " + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            @Override
            public String getBodyContentType() {

                return "application/x-www-form-urlencoded; charset=UTF-8";
            }

            @Override
            protected Map<String, String> getParams() {

                Map<String, String> params = new HashMap<String, String>();

                params.put("ime", MainActivity.ime_k);
                params.put("prezime", MainActivity.prezime_k);

                return params;
            }
        };

        Volley.newRequestQueue(getContext()).add(stringRequest);

    }
    private void dothat(JSONArray j){

        for(int i=0;i<j.length();i++){
            try {

                JSONObject zah = j.getJSONObject(i);
                zahtjevi.add(new Zahtjev(
                        zah.getString("id"),
                        zah.getString("Nosnja"),
                        zah.getString("Obuca"),
                        zah.getString("Status"),
                        zah.getString("BrObuce"),
                        zah.getString("Datum_Zaduzen")
                ));
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }

        ZahtjeviAdapter adapter = new ZahtjeviAdapter(getContext(), zahtjevi);
        recyclerView.setAdapter(adapter);
    }

}
