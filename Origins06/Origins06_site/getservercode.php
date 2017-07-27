<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" id="pee">
<head>
<title>Origins06</title>
<link rel="stylesheet" type="text/css" href="css/Origins06_normal.css" title="normal"/>
<link rel="stylesheet" type="text/css" href="css/Origins06_dark.css" title="dark">
<link rel="stylesheet" type="text/css" href="css/Origins06_classic.css" title="classic">
<link rel="stylesheet" type="text/css" href="css/Origins06_classicdark.css" title="classicdark"> 
<link rel="Shortcut Icon" type="image/ico" href="images/icon.ico"/>
<script type="text/javascript" src="js/js_funcs.js"></script>
</head>
<body onload="set_style_from_cookie()">
<?php
$con = new mysqli("localhost", "origins0_root", "", "origins0_games");

if (mysqli_connect_errno()) 
{
    printf("Connect failed: %s\n", mysqli_connect_error());
}

$namefixed = mysqli_real_escape_string($con,$_POST['name']);
$ipcrypt = base64_encode($_POST['ip']);

$sql="INSERT INTO games (name, map, ip, port, playerlimit)

VALUES

('$namefixed','$_POST[map]','$ipcrypt','$_POST[port]','$_POST[playerlimit]')";

if (!$con->query($sql))
{
  printf("Error: %s\n", $con->error);
}

$con->close();

$port=$_POST['port'];
$playerlimit=$_POST['playerlimit'];
?>
<div id="Container">
				<div id="Header">
					<div id="Banner">
						<center><div id="Logo"><a id="logo" title="Origins06" href="index.php" style="display:inline-block;cursor:pointer;"><img src="images/Logo.png" border="0" id="img" alt="Origins06"/></a></div></center>
					</div>
					<div class="Navigation">
						<span><a id="Games" class="MenuItem" href="games.php">Games</a></span>
						<span class="Separator"> | </span>
						<span><a id="HostServer" class="MenuItem" href="hostserver.php">Host Server</a></span>
 					</div>
				</div>
				<div id="Body">
					
	<div id="SplashContainer">
		<div id="MainPanel">
			<center>
			<h2>Host Server</h2>
				<div id="ElementInsert">
					<center>
						<h3>Put this URL into your command bar. If your server works, it'll show up in the Games page.</h3>
						<div id="genlink"><b>dofile('localhost/Origins06_site/functions.php'); _G.CSR06Server(<?php echo $port ?>,<?php echo $playerlimit ?>);</b></div>
					</center>
				</div>
			</center>
		</div>
	</div>
	</div>
	
	<div id="Footer">
	Origins06 does not knowingly host copyrighted content. If you host a server, we store your IP address, but it is not publicly distributed to users on the site.
	</div>
			</div>
	</body>
</html>
