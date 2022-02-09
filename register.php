<?php
  
$servername = "localhost";
  

$username = "root";
  

$password = "";

$dbname = "tenabaza";
   

$conn = new mysqli($servername, $username, $password, $dbname);
  

 $response = array();

 if($_POST['ime'] && $_POST['prezime'] && $_POST['username'] && $_POST['password']){

     $ime = $_POST['ime'];
     $prezime = $_POST['prezime'];
     $username = $_POST['username'];
     $password = $_POST['password'];

     $stmt = $conn->prepare("INSERT INTO `korisnici`(`Ime`, `Prezime`, `Username`,`Password`) VALUES (?,?,?,?)");
     $stmt->bind_param("ssss",$ime,$prezime,$username,$password);

   if($stmt->execute() == TRUE){
   
         $response['error'] = false;
         $response['message'] = "Registracija uspješna";
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