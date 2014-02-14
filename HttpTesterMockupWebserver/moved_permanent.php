<?php

// tells client that this resource has moved permanently

header('HTTP/1.0 301 Moved Permanently');
header('Location: normal.php');
