<?php


class DinnerClass
{

    public function index()
    {
        $scrape = new ScrapeClass();

        $baseURL = $_COOKIE['dinnerURL'];

//        $data = $scrape->curl($baseURL);
//        $xpath = $scrape->loadDOM($data);
//        $dinnerPath = $scrape->getHrefAttribute($xpath, '//li/a', 2);

        $dinnerURL = $scrape->curl($baseURL);
        $xpath = $scrape->loadDOM($dinnerURL);

        $availableTime = $xpath->query('//input[@type="radio"]');

        $selectedTime = $_GET['time'];
        $selectedDay = $_GET['day'];
        $movieName = $_GET['title'];

        if ($selectedDay == 'Fredag')
            $selectedDay = 'fre';
        elseif ($selectedDay == 'Lördag')
            $selectedDay = 'lor';
        elseif ($selectedDay == 'Söndag')
            $selectedDay = 'son';

        $availableTables = [];
        $i=0;
        foreach ($availableTime as $at) {
            $day = substr($at->getAttribute('value'), 0, 3); // Get the 3 first letter in value that is the day
            $time = substr($at->getAttribute('value'), 3, 2);
            $endTime = substr($at->getAttribute('value'), 5, 2);

            /**
             * If day and time is suitable after choosen movie save it in an array
             */
            if ($day == $selectedDay && $time >= $selectedTime + 2){
                $availableTables[$i]['time'] = $time;
                $availableTables[$i]['endTime'] = $endTime;
                $availableTables[$i]['movie'] = $movieName;
                $availableTables[$i]['movieTime'] = $selectedTime;
                $i++;
            }

        }

        return $availableTables;
    }
}