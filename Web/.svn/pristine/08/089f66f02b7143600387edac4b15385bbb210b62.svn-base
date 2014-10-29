<?php
if ($_POST["Data"]!=""){
$mysql = new SaeMysql();
$sql = "INSERT  INTO `data` ( `data`) VALUES ('"  . $mysql->escape( $_POST["Data"] ) . "') ";
$mysql->runSql($sql);
if ($mysql->errno() != 0)
{
    die("Error:" . $mysql->errmsg());
}

    $mysql->closeDb();}
?> 