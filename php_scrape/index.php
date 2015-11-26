<?php

error_reporting(E_ALL);
ini_set('display_errors', 'On');

require_once 'classes/LayoutClass.php';
require_once 'classes/BookingClass.php';
require_once 'classes/ScrapeClass.php';
require_once 'classes/DinnerClass.php';

$layout = new LayoutClass();
$layout->render();