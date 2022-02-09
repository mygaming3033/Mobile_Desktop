<?php
  
$servername = "localhost";
  
$username = "root";
  
$password = "";

$dbname = "tenabaza";
   
$conn = new mysqli($servername, $username, $password, $dbname);
  
 $response = array();

 if($_POST['ime'] && $_POST['prezime'] && $_POST['nosnja'] && $_POST['obuca'] && $_POST['brObuce']){

     $ime = $_POST['ime'];
     $prezime = $_POST['prezime'];
     $nosnja = $_POST['nosnja'];
     $obuca = $_POST['obuca'];
     $brObuce = $_POST['brObuce'];
 
     $stmt = $conn->prepare("INSERT INTO `zahtjevi`(`Ime`, `Prezime`, `Nosnja`,`Obuca`,`BrObuce`) VALUES (?,?,?,?,?)");
     $stmt->bind_param("sssss",$ime,$prezime,$nosnja,$obuca,$brObuce);

   if($stmt->execute() == TRUE){

         $response['error'] = false;
         $response['message'] = "Zahtjev je uspješno poslan";
     } else{
         $response['error'] = true;
         $response['message'] = "failed\n ".$conn->error;
     }
 } else{

     $response['error'] = true;
     $response['message'] = "IError";
 }
 echo json_encode($response);
 ?>