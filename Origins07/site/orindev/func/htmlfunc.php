<?php
date_default_timezone_set("UTC"); 

function connectdb(){
	
//SQL DATABASE INFORMATION HERE
$servername = "localhost";
$username 	= "root";
$password 	= "";	
$dbname   	= "maindb";
//SQL DATABASE INFORMATION HERE

try {$conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
return $conn; }
catch(PDOException $e){
return false;}
}

function head() {	
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" id="pee">
<head>
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Lato">
<title>Origins07</title>
<link rel="stylesheet" href="func/styles.css?vers=9">
</head>
<body>
<div class="announce" style="text-align: center">
Welcome to Origins '07, grand opening!
</div><br>
<div class="memetext"><p style="position: fixed; bottom: 0; width:100%; text-align: left"> drslicendice wus here hail rbxbanland XD</p></div>
<?php
}

function footer() {
?>
<div class="gamediv">
Origins07 does not knowingly host copyrighted content. If you host a server, we store your IP address, but it is not publicly distributed to users on the site.
</div>
</body>
</html>
<?php		
}



?>