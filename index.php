<!DOCTYPE html>
<html>
<head>
    <title>Laboration 1</title>
</head>

<body>

<h1>Ange url</h1>
<form method="post">
    <input type="text" name="url" />
    <input type="submit" value="send" />
</form>

</body>

</html>

<?php

if($_POST['url']) {
    require_once 'Booking.php';

    error_reporting(E_ALL);
    ini_set('display_errors', 'On');

    $booking = new Booking();
    $movies = $booking->index();

    $ret = '<h1>Följande filmer hittades</h1>';

    foreach ($movies as $movie) {
        $ret .= '<ul>';
        $ret .= '<li> Filmen ' . $movie['title'] . ' klockan ' . $movie['time'] . ' på ' . $movie['day'] . '</li>';
        $ret .= '</ul>';
    }
    echo $ret;
}