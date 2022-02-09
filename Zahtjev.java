package com.example.teninagarderobamobile;

public class Zahtjev {
    private String id;
    private String nosnja;
    private String obuca;
    private String status;
    private String datum;
    private String brObuce;

    public Zahtjev(String id, String nosnja, String obuca, String status, String brObuce, String datum) {
        this.id = id;
        this.nosnja = nosnja;
        this.obuca = obuca;
        this.status = status;
        this.brObuce = brObuce;
        this.datum=datum;
    }

    public String getId() {
        return id;
    }

    public String getNosnja() {
        return nosnja;
    }

    public String getObuca() {
        return obuca;
    }

    public String getStatus() {
        return status;
    }

    public String getDatum() {
        return datum;
    }
    public String getBrObuce() {
        return brObuce;
    }
}
