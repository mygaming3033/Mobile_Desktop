<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "tenabaza";
  

$conn = new mysqli($servername, $username, $password, $dbname);
$id = $_POST['id'];

 $response = array();

 if($id){

     $stmt = $conn->prepare("SELECT Password,id,Ime,Prezime FROM korisnici WHERE Username = ?");
     $stmt->bind_param("s",$id);
     $result = $stmt->execute();
 
   if($result == TRUE){

         $response['error'] = false;
         $response['message'] = "Retrieval Successful!";
    
         $stmt->store_result();

         $stmt->bind_result($korIme,$ajdi,$ime,$prezime);

         $stmt->fetch();

         $response['korIme'] = $korIme;
         $response['ajdi'] = $ajdi;
         $response['ime'] = $ime;
         $response['prezime'] = $prezime;
     } else{

         $response['error'] = true;
         $response['message'] = "Incorrect id";
     }
 } else{

      $response['error'] = true;
      $response['message'] = "Insufficient Parameters";
 }

 echo json_encode($response);
?>