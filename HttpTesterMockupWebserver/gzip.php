<?php

// test to allow "black box" testing of gzip compression capability of the web server

ini_set("zlib.output_compression", "1");

echo "compressed data";    