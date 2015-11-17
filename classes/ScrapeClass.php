<?php


class ScrapeClass
{
    /**
     * @param $url
     * @return mixed
     */
    public function curl($url)
    {
        $userAgent = $_SERVER['HTTP_USER_AGENT'];
        // Assigning cURL options to an array
        $options = array(
            CURLOPT_USERAGENT, $userAgent,
            CURLOPT_RETURNTRANSFER => TRUE,  // Setting cURL's option to NOT return the webpage data
            CURLOPT_URL => $url, // Setting cURL's URL option with the $url variable passed into the function
        );
        $ch = curl_init();  // Initialising cURL
        curl_setopt_array($ch, $options);   // Setting cURL's options using the previously assigned array data in $options
        $data = curl_exec($ch); // Executing the cURL request and assigning the returned data to the $data variable
        curl_close($ch);    // Closing cURL
        return $data;   // Returning the data from the function
    }

    /**
     * @param $url
     * @return DOMXPath
     */
    public function loadDOM($url)
    {
        $dom = new DOMDocument();
        libxml_use_internal_errors(TRUE); //disable libxml errors

        if (empty($url)) //if any html is actually returned
            die('No content found');

        $dom->loadHTML($url);

        return new DOMXPath($dom); // This allows us to do some queries with the DOM Document
    }

    /**
     * @param $xpath
     * @param $query
     * @param $nr
     * @return mixed
     */
    public function getHrefAttribute($xpath, $query, $nr)
    {
        $profileNav = $xpath->query($query);
        return $profileNav[$nr]->getAttribute('href');
    }

}