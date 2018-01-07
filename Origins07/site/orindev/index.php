<?php
include 'func/htmlfunc.php';
session_start();

head();
function generateRandomString($length = 10) {
    $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    $charactersLength = strlen($characters);
    $randomString = '';
    for ($i = 0; $i < $length; $i++) {
        $randomString .= $characters[rand(0, $charactersLength - 1)];
    }
    return $randomString;
}
$alreadydone = array();
print_r($alreadydone);
?>
<div class="gamediv" style="text-align: center">
<div class="ourbiglogo"></div>
<gname>Welcome to Origins '07!</gname><br><br>
This is a fun side project between drslicendice and Bitl.<br>
This is meant to be a successor to Origins '06, another project with a working website but with a fake 2006 client.
<br>By the way, Origins '07 is <discriminator>open source</discriminator>! Including the website!
<br><br>
<button class="playbutton2" id="shrink" onclick="window.location.href='#host'">host server</button>
<button class="playbutton2" id="shrink" onclick="window.location.href='http://www.hyperlinkcode.com/button-links.php'">install</button>
<button class="playbutton2" id="shrink" onclick="window.location.href='https://github.com/Bitl/other-roblox-projects-src'">github</button>
</div>
<div class="gamediv" style="text-align: center">
<iframe src="http://www.youtube.com/embed/W2GAXfkFfJI"
width="400" height="225" frameborder="0" allowfullscreen></iframe>
</div>
<br>
<?php
$dbconn = false;
$dbconn = connectdb();
if($dbconn == false){	
?>
<div class="gamediv">
Could not connect to database! Be sure you configured this correctly and added login parameters to func/htmlfunc.php!
</div></body></html>	
<?php
}
else {
try {
$stmt = $dbconn->prepare("SELECT * FROM games ORDER BY players DESC;"); 
$stmt->execute();
$result = $stmt->fetchAll();
$serversup = 0;
if (count($result) == 0){
?><div class="gamediv">
No servers are currently online at the moment! Try <a href="#host">hosting a server</a>
</div><?php	
}else{	
foreach ($result as $row) {
$alreadydone[$row["randomid"]] = true;
if(($row["players"] != -1) && ($row["lastpingedunix"] >= time()-200)){
$serversup = $serversup + 1;
echo '<div class="gamediv" id = "nowrap"><div class="ourlogo"></div>
<div class="buttonsbelowimage">
<button class="normbutton" id="shrink">page</button><br>
<button class="playbutton" id="shrink" onclick="window.location.href='."'origins07://". base64_encode(base64_encode($row["ip"])."|".base64_encode($row["port"])) ."'".'">play game</button>
</div><div class="gameintext">
<gname>' . $row["nameofgame"] . '</gname>
<p><discriminator>Hosted by: </discriminator>' . $row["creatorname"] . '</p>
<p><discriminator>Date: </discriminator>' . $row["creationdate"] . '</p>
<p><discriminator>Players: </discriminator>' . $row["players"] . '/20</p>
</div></div>';
}}}
if(($serversup == 0)&&(count($result) != 0)){	
?><div class="gamediv">
No servers are currently online at the moment! Try <a href="#host">hosting a server</a>
</div><?php	
}} catch (Exception $e) {
try {
$sql = file_get_contents("database.sql");
$stmt = $dbconn->prepare($sql);
if ($stmt->execute()){
?><div class="gamediv">
Finished setting up SQL! No servers are currently online at the moment! Try <a href="#host">hosting a server</a>
</div><?php	
}else{
?>
<div class="gamediv">
Could not import table to database... You shouldn't see this error pop up.
</div>
<?php	
}} catch (Exception $e) {
?>
<div class="gamediv">
Weird SQL error.... <?php echo $e; ?>
</div>
<?php	
}}}
?>
<br>
<?php
$name = $servname = $ip = $port = "";
if ($_SERVER["REQUEST_METHOD"] == "POST") {
  $name 	= test_input($_POST["1"]);
  $servname = test_input($_POST["2"]);
  $ip 		= test_input($_POST["3"]);
  $port 	= test_input($_POST["4"]);
  if(($name != "")&&($servname != "")&&($ip != "")&&($port != "")){
	  
if (!empty($_SESSION['count']))
{ 
session_destroy();
}
else{
$_SESSION['count']=1;

  $ip = preg_replace('/|/', '', $ip );
  $port = preg_replace('/|/', '', $port );
  $name = substr($name, 0, 20);
  $servname = substr($servname, 0, 23);
  $ip = substr($ip, 0, 40);
  $port = substr($port, 0, 40);
  $today = date("Y/m/j, g:i A");
	if((strlen($name)>=3) && (strlen($servname)>=3)){
		$newRandom = "";
		do {
		$newRandom = generateRandomString();
		} while (isset($alreadydone[$newRandom]));
			
		if ($newRandom != ""){
			
		$stmt = $dbconn->prepare("INSERT INTO games (ip,port,creationdate,nameofgame,creatorname,randomid) 
		VALUES (:ip,:port,:creationdate,:nameofgame,:creatorname,:randomid)");
		$stmt->bindParam(':ip', $ip);
		$stmt->bindParam(':port', $port);
		$stmt->bindParam(':creationdate', $today);
		$stmt->bindParam(':nameofgame', $servname);
		$stmt->bindParam(':creatorname', $name);
		$stmt->bindParam(':randomid', $newRandom);
		$stmt->execute();	
		
		$url = $_SERVER['REQUEST_URI']; 
		$parts = explode('/',$url);
		$dir = $_SERVER['SERVER_NAME'];
		for ($i = 0; $i < count($parts) - 1; $i++) {
		$dir .= $parts[$i] . "/";
		}
		$dir = "http://" . $dir;
		
		?>
		<div class="gamediv">
		<b><?php echo $servname; ?></b>
		<br>
		<b><?php echo $name; ?>,</b> if you would like to host this new server, press the button below to launch the Dedicated Server. If it doesn't launch, use the Origins07 Installer.<br>
		<?php echo '<br><button class="playbutton" id="shrink" onclick="window.location.href='."'origins07server://". base64_encode(base64_encode($port)."|".base64_encode($dir)) ."'".'">launch dedicated server</button><br><br>'; ?>
		If you would like to go to the link of your server page, <a>go here.</a>
		</div>
<?php
}}}}} else { session_destroy(); }
function test_input($data) {
  $data = trim($data);
  $data = stripslashes($data);
  $data = htmlspecialchars($data);
  return $data;
}
?>
<div class="gamediv" id="host" style="text-align: center">
<form method="post" action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>">
    <input type="text" id="1" maxlength="20" pattern=".{3,20}" name="1" placeholder="Username.. (3-20 characters)">
    <input type="text" id="2" maxlength="23" pattern=".{3,23}" name="2" placeholder="Server name.. (3-23 characters)">
    <input type="text" id="3" maxlength="39" name="3" placeholder="IP..">
    <input type="text" id="4" maxlength="6" name="4" onkeypress='return event.charCode >= 48 && event.charCode <= 57' placeholder="Port..">
    <input class= "playbutton2"type="submit" value="Host Server">
  </form>
</div>
<?php
footer();
?>
