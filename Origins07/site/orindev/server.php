<?php
function is_decimal( $val )
{
    return is_numeric( $val ) && floor( $val ) != $val;
}
include 'func/htmlfunc.php';
$dbconn = false;
$dbconn = connectdb();
if(isset($dbconn)){	
if(isset($_GET["i"])){
try {
$stmt = $dbconn->prepare("SELECT * FROM games WHERE randomid = :i"); 
$stmt->bindParam(':i', $_GET["i"]);
$stmt->execute();
$result = $stmt->fetchAll(); 
if(isset($result[0])){
$newplayers = 0;
$newunixtime = time();
$sql = "UPDATE games SET players=:b, lastpingedunix=:c WHERE gameid=:i";
$stmt2 =$dbconn->prepare($sql);
$stmt2->bindParam(':i', $result[0]["gameid"]);
$stmt2->bindParam(':c', $newunixtime);
$stmt2->bindParam(':b', $newplayers);
$stmt2->execute();
}echo '';}catch  (Exception $e) {echo '';}}
elseif((isset($_GET["update"]))&&(isset($_GET["players"]))){
if(($_GET["players"]<=20)&&($_GET["players"]>=-1)&&(!is_decimal($_GET["players"]))){
	
$newplayers = $_GET["players"];
$newunixtime = time();
$sql = "UPDATE games SET players=:b, lastpingedunix=:c WHERE gameid=:i";
$stmt2 =$dbconn->prepare($sql);
$stmt2->bindParam(':i', $_GET["update"]);
$stmt2->bindParam(':c', $newunixtime);
$stmt2->bindParam(':b', $newplayers);
$stmt2->execute();

}}}else {echo '';}
?>