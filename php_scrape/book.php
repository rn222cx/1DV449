<?php

$postUrl = $_COOKIE['postFormURL'];  // Setting URL to POST to

$tableValue = $_GET['tableValue'];
// Setting form input fields as 'name' => 'value'
$postFields = array(
    'group1' => $tableValue,
    'username' => 'zeke',
    'password' => 'coys'
);


echo postCurl($postUrl, $postFields);


function postCurl($url, $postFields)
{
    $ch = curl_init();  // Initialising cURL

    $userAgent = $_SERVER['HTTP_USER_AGENT'];

    // $cookie = 'cookie.txt';  // Setting a cookie file to store cookie
    // curl_setopt($ch, CURLOPT_COOKIEJAR, $cookie);  // Setting cookiejar
    // curl_setopt($ch, CURLOPT_COOKIEFILE, $cookie);  // Setting cookiefile
    curl_setopt($ch, CURLOPT_URL, $userAgent); // Show my user agent to the owner
    curl_setopt($ch, CURLOPT_URL, $url); // Setting URL to POST to
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);  // Setting cURL's option to NOT return the webpage data
    curl_setopt($ch, CURLOPT_POST, 1);  // Setting method as POST
    curl_setopt($ch, CURLOPT_POSTFIELDS, $postFields);
    curl_setopt($ch, CURLOPT_POSTFIELDS, http_build_query($postFields));  // Setting POST fields as array

    $data = curl_exec($ch); // Executing the cURL request and assigning the returned data to the $data variable
    curl_close($ch);    // Closing cURL

    return $data;   // Returning the data from the function
}