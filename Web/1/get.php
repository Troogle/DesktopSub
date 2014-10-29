<?php
$mysql = new SaeMysql();
$sql = "SELECT * FROM `Count`";
$count1 = count($mysql->getData( $sql ));
$sql = "SELECT * FROM `data`";
$count2 = count($mysql->getData( $sql ));

while ($count1<$count2){
$sql = "SELECT data FROM `data` where ID=".$mysql->escape($count1+1);
$data = $mysql->getData( $sql );
echo $data[0]['data'];
echo "\r\n";
$sql = "INSERT  INTO `Count` ( `Count`) VALUES ('"  .$mysql->escape( $count1+1 ). "') ";
$mysql->runSql($sql);
$sql = "SELECT * FROM `Count`";
$count1 = count($mysql->getData( $sql ));
$sql = "SELECT * FROM `data`";
$count2 = count($mysql->getData( $sql ));
}
 
if ($mysql->errno() != 0)
{
    die("Error:" . $mysql->errmsg());
}
$mysql->closeDb();
?> 