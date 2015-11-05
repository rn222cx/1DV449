<?php


class Booking
{

    private $availableDays = array('', '', '');

    public function index()
    {
        $postURL = rtrim($_POST['url'], '/');
        $url = $this->curl($postURL);
        /**
         * Get links in start page
         */
        $xpath = $this->loadDOM($url);
        $contactURL = $this->getHrefAttribute($xpath, '//li/a', 0);
        $cinemaURL = $this->getHrefAttribute($xpath, '//li/a', 1);

        /**
         * Get friends link to their profiles
         */
        $url = $this->curl($postURL . $contactURL . '/');

        $xpath = $this->loadDOM($url);
        $paulURL = $this->getHrefAttribute($xpath, '//li/a', 0);
        $peterURL = $this->getHrefAttribute($xpath, '//li/a', 1);
        $maryURL = $this->getHrefAttribute($xpath, '//li/a', 2);

        $linkToPaul = $postURL . $contactURL . '/' . $paulURL;
        $linkToPeter = $postURL . $contactURL . '/' . $peterURL;
        $linkToMary = $postURL . $contactURL . '/' . $maryURL;

        /**
         * Check when friends are available
         */
        $url = $this->curl($linkToPaul);
        $xpath = $this->loadDOM($url);
        $this->isDayAvailable($xpath);

        $url = $this->curl($linkToPeter);
        $xpath = $this->loadDOM($url);
        $this->isDayAvailable($xpath);

        $url = $this->curl($linkToMary);
        $xpath = $this->loadDOM($url);
        $this->isDayAvailable($xpath);


        if (str_word_count($this->availableDays[0]) == 3)
            $this->availableDays[0] = 'Fredag';
        if (str_word_count($this->availableDays[1]) == 3)
            $this->availableDays[1] = 'Lördag';
        if (str_word_count($this->availableDays[2]) == 3)
            $this->availableDays[2] = 'Söndag';

        /**
         * Get data from cinema page
         */
        $cinemaPage = $this->curl($postURL . $cinemaURL);
        $xpath = $this->loadDOM($cinemaPage);
        $getDays = $xpath->query('//select[@id = "day"]/option[not(@disabled)]');
        $getMovies = $xpath->query('//select[@id = "movie"]/option[not(@disabled)]');

        $movieArray = [];

        foreach ($getDays as $day) {
            if (in_array($day->nodeValue, $this->availableDays)) {
                foreach ($getMovies as $movie) {

                    $jsonURL = $this->curl($postURL . $cinemaURL . "/check?day=" . $day->getAttribute('value') . "&movie=" . $movie->getAttribute('value'));
                    $movies = json_decode($jsonURL, true);

                    foreach ($movies as $key => $value) {
                        $movies[$key]['title'] = $movie->nodeValue;
                        $movies[$key]['day'] = $day->nodeValue;
                        if ($movies[$key]['status'] == '0')
                            unset($movies[$key]);
                    }

                    foreach ($movies as $mov)
                        $movieArray[] = $mov;

                }
            }
        }
        return $movieArray;
    }

    public function isDayAvailable($xpath)
    {
        $calenderRow = $xpath->query('//td');

        for ($x = 0; $x < 3; $x++) {
            if (strtolower($calenderRow[$x]->nodeValue) == 'ok') {
                $this->availableDays[$x] .= 'ok ';
            }
        }
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

    /**
     * @param $url
     * @return DOMXPath
     */
    public function loadDOM($url)
    {
        $dom = new DOMDocument();
        if (empty($url)) //if any html is actually returned
            die('No content found');

        $dom->loadHTML($url);
        return new DOMXPath($dom); // This allows us to do some queries with the DOM Document
    }

    /**
     * @param $url
     * @return mixed
     */
    public function curl($url)
    {
        // Assigning cURL options to an array
        $options = Array(
            CURLOPT_RETURNTRANSFER => TRUE,  // Setting cURL's option to NOT return the webpage data
            CURLOPT_URL => $url, // Setting cURL's URL option with the $url variable passed into the function
        );
        $ch = curl_init();  // Initialising cURL
        curl_setopt_array($ch, $options);   // Setting cURL's options using the previously assigned array data in $options
        $data = curl_exec($ch); // Executing the cURL request and assigning the returned data to the $data variable
        curl_close($ch);    // Closing cURL
        return $data;   // Returning the data from the function
    }

}