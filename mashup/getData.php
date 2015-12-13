<?php
ini_set('display_startup_errors',1);
ini_set('display_errors',1);
error_reporting(-1);

$url = "http://api.sr.se/api/v2/traffic/messages?pagination=false&format=json";
$jsonData = "storage/SRdata.json";

if(intval(get_http_response_code($url)) == 200){

    if (time()-filemtime($jsonData) > 300) { // Hämta ny data om nuvarande är 5 minuter gammal
        $rawJason = file_get_contents($url);
        file_put_contents($jsonData, $rawJason);
    }
}
$storedJason = file_get_contents($jsonData);

echo $storedJason;


/**
 * return the 3-digit HTTP response code
 *
 * @param $url
 * @return string
 */
function get_http_response_code($url) {
    $headers = get_headers($url);
    return substr($headers[0], 9, 3);
}

?>