<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['cedulapaciente']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * paciente  where cedulapaciente=:cedulapaciente");
      $sql->bindValue(':cedulapaciente', $_GET['cedulapaciente']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
    }
    else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM paciente");
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
    $sql = "INSERT INTO paciente
          (cedulapaciente, nombrepaciente,apellidopaciente,correopaciente,telefonopaciente,direccionpaciente,estadopaciente)
          VALUES
          (:cedulapaciente, :nombrepaciente,:apellidopaciente,:correopaciente,:telefonopaciente,:direccionpaciente,:estadopaciente)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

 

    $postcedulapaciente = $dbConn->lastInsertId();
    if($postcedulapaciente)
    {
      $input['cedulapaciente'] = $postcedulapaciente;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}

 

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $cedulapaciente = $_GET['cedulapaciente'];
  $statement = $dbConn->prepare("DELETE FROM  paciente where cedulapaciente=:cedulapaciente");
  $statement->bindValue(':cedulapaciente', $cedulapaciente);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

 

if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postcedulapaciente = $input['cedulapaciente'];
    $fields = getParams($input);

 

    $sql = "
          UPDATE paciente
          SET $fields
          WHERE cedulapaciente='$postcedulapaciente'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

 

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>