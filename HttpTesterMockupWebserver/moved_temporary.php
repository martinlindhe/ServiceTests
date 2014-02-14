<?php

// tells client that this resource has moved temporary

header('HTTP/1.0 302 Found');
header('Location: normal.php');
