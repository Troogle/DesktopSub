<?php
$mysql = new SaeMysql();
$id =is_numeric($_GET['id']) ? $_GET['id'] :'0';
$sql = "SELECT data FROM `data` where ID>".$mysql->escape($id);
$data = $mysql->getData( $sql );
if (is_array($data)) {
foreach ($data as $value) {
  echo $value['data'];
  echo "\r";
}
}
if ($mysql->errno() != 0)
{
    die("Error:" . $mysql->errmsg());
}
$mysql->closeDb();
?> 