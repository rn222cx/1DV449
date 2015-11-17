<?php


class BookingClass
{

    private $availableDays = ['','','']; // add sections in array to remove Undefined offset error

    public function index()
    {
        $scrape = new ScrapeClass();

        $postURL = rtrim($_POST['url'], '/');

        /**
         * Save full path to dinner page for later
         */
        setcookie('baseURL', $postURL, time() + (86400 * 30)); // 86400 = 1 day

        $baseURL = $scrape->curl($postURL);

        /**
         * Get links in start page
         */
        $xpath = $scrape->loadDOM($baseURL);
        $contactURL = $scrape->getHrefAttribute($xpath, '//li/a', 0);
        $cinemaURL = $scrape->getHrefAttribute($xpath, '//li/a', 1);

        /**
         * Get friends link to their profiles
         */
        $url = $scrape->curl($postURL . $contactURL . '/');

        $xpath = $scrape->loadDOM($url);
        $paulURL = $scrape->getHrefAttribute($xpath, '//li/a', 0);
        $peterURL = $scrape->getHrefAttribute($xpath, '//li/a', 1);
        $maryURL = $scrape->getHrefAttribute($xpath, '//li/a', 2);

        $linkToPaul = $postURL . $contactURL . '/' . $paulURL;
        $linkToPeter = $postURL . $contactURL . '/' . $peterURL;
        $linkToMary = $postURL . $contactURL . '/' . $maryURL;

        /**
         * Check when friends are available
         */
        $url = $scrape->curl($linkToPaul);
        $xpath = $scrape->loadDOM($url);
        $this->isDayAvailable($xpath);

        $url = $scrape->curl($linkToPeter);
        $xpath = $scrape->loadDOM($url);
        $this->isDayAvailable($xpath);

        $url = $scrape->curl($linkToMary);
        $xpath = $scrape->loadDOM($url);
        $this->isDayAvailable($xpath);


        if (str_word_count($this->availableDays[0]) == 3)
            $this->availableDays[0] = '01';
        if (str_word_count($this->availableDays[1]) == 3)
            $this->availableDays[1] = '02';
        if (str_word_count($this->availableDays[2]) == 3)
            $this->availableDays[2] = '03';

        /**
         * Get data from cinema page
         */
        $cinemaPage = $scrape->curl($postURL . $cinemaURL);
        $xpath = $scrape->loadDOM($cinemaPage);
        $getDays = $xpath->query('//select[@id = "day"]/option[not(@disabled)]');
        $getMovies = $xpath->query('//select[@id = "movie"]/option[not(@disabled)]');

        $movieArray = [];

        foreach ($getDays as $day) {
            if (in_array($day->getAttribute('value'), $this->availableDays)) {
                foreach ($getMovies as $movie) {

                    $jsonURL = $scrape->curl($postURL . $cinemaURL . "/check?day=" . $day->getAttribute('value') . "&movie=" . $movie->getAttribute('value'));
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

        foreach ($calenderRow as $key => $value) {
            if (strtolower($calenderRow[$key]->nodeValue) == 'ok') {
                $this->availableDays[$key] .= 'ok ';
            }
        }
    }

}