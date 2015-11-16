<?php


class LayoutClass
{
    public function render()
    {
        echo '
<!DOCTYPE html>
<html>
<head>
    <title>Laboration 1</title>
</head>

    <body>
        '. $this->postForm() .'
        '. $this->scrape() .'
        '. $this->dinnerTables() .'
    </body>

</html>
';
    }

    public function postForm()
    {
        if(!isset($_GET['day'])) {
            return '
        <form method="post">
            <label for="url">Ange url: </label>
            <input type="text" name="url" />
            <input type="submit" value="Start!" />
        </form>
        ';
        }
    }

    public function scrape()
    {
        if (isset($_POST['url'])) {
            $booking = new BookingClass();
            $movies = $booking->index();

            $ret = '<h1>Följande filmer hittades</h1>';
            $ret .= '<ul>';
            foreach ($movies as $movie) {
                $ret .= "<li> Filmen {$movie['title']} klockan {$movie['time']} på {$movie['day']}
                     <a href='?day={$movie['day']}&time={$movie['time']}&title={$movie['title']} '>Välj denna dag och Boka Bord</a>
                    </li>";
            }
            $ret .= '</ul>';
            return $ret;
        }
    }

    public function dinnerTables()
    {
        if(isset($_GET['day'])){
            $dinner = new DinnerClass();
            $availableTables = $dinner->index();

            if(empty($availableTables))
                return 'Det fanns inga lediga tider :(';

            $ret = '<h1>Följande tider är lediga att boka på zekes restaurang</h1>';
            $ret .= '<ul>';

            foreach ($availableTables as $at) {
                $ret .= "<li> Det finns ett ledigt bord mellan klockan {$at['time']} och {$at['endTime']} efter att sett filmen {$at['movie']} klockan {$at['movieTime']}
                     <a href='book.php?tableValue={$at['tableValue']}'> Boka detta bord</a>
                    </li>";
            }
            $ret .= '</ul>';
            return $ret;
        }

    }
}