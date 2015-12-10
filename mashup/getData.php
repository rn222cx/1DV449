<?php
ini_set('display_startup_errors',1);
ini_set('display_errors',1);
error_reporting(-1);

$url = "http://api.sr.se/api/v2/traffic/messages?pagination=false&format=json";

$storage = "storage/SRdata.json";

if (time()-filemtime($storage) > 600) { // 10 minuter
	$rawJason = file_get_contents($url);
	file_put_contents($storage, $rawJason);
  	// echo "$storage was last modified: " . date ("F d Y H:i:s.", filemtime($storage));
}

$storedJason = file_get_contents($storage);

echo $storedJason;
?>