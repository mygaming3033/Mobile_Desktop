<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "tenabaza";
  
$conn = new mysqli($servername, $username, $password, $dbname);
$sql = "SELECT * FROM nosnje";

$r = mysqli_query($conn,$sql);

$result = array();

while($row = mysqli_fetch_array($r)){
    array_push($result,array(
        'id'=>$row['id'],
        'Naziv'=>$row['Naziv'],
        'Kolicina'=>$row['Kolicina']
    ));
}

echo json_encode(array('result'=>$result));

mysqli_close($conn);
 
?>