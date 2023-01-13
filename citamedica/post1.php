<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idcita']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * citamedica  where idcita=:idcita");
      $sql->bindValue(':idcita', $_GET['idcita']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
    }
    else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM citamedica");
      $sql->execute();
      $sql->setFetchMode(PDO::FETCH_ASSOC);
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
    }
}

 

if ($_SERVER['REQUEST_METHOD'] == 'POST')
{
    $input = $_POST;
    $sql = "INSERT INTO citamedica
          (idcita, cedulapaciente,nombremedico,especialidad,fechacita,horacita)
          VALUES
          (:idcita, :cedulapaciente,:nombremedico,:especialidad,:fechacita,:horacita)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

 

    $postidcita = $dbConn->lastInsertId();
    if($postidcita)
    {
      $input['idcita'] = $postidcita;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}

 

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $idcita = $_GET['idcita'];
  $statement = $dbConn->prepare("DELETE FROM  citamedica where idcita=:idcita");
  $statement->bindValue(':idcita', $idcita);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

 

if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postidcita = $input['idcita'];
    $fields = getParams($input);

 

    $sql = "
          UPDATE citamedica
          SET $fields
          WHERE idcita='$postidcita'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

 

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>