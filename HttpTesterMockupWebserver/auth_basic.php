<?php

// TODO show a "restricted" page that requires USER & password login with popup. grant access & redir if correct


if ( !isset($_SERVER['PHP_AUTH_USER']) ) {
    //  Watch out for buggy Internet Explorer browsers out there. They seem very picky about the order of the headers. Sending the WWW-Authenticate header before the HTTP/1.0 401 header seems to do the trick for now. 
    header('WWW-Authenticate: Basic realm="You Shall Not Pass"');
    header('HTTP/1.0 401 Unauthorized');
    exit;
}
else {
    if ( $_SERVER['PHP_AUTH_USER'] == 'username1' && $_SERVER['PHP_AUTH_PW'] == 'password1' ) {
            echo "<p>Welcome, {$_SERVER['PHP_AUTH_USER']}.</p>";
        } else {
            echo "Wrong password, Balrog!";
        }
}

