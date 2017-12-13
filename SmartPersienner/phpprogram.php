<?php


$uri ="http://dbtophpservice.azurewebsites.net/service1.svc/lightvalues";
$uri2 ="http://dbtophpservice.azurewebsites.net/service1.svc/weatherDate";
$uri3 ="http://dbtophpservice.azurewebsites.net/service1.svc/temperature";
$uri4 ="http://dbtophpservice.azurewebsites.net/service1.svc/cloudiness";
$uri5 ="http://dbtophpservice.azurewebsites.net/service1.svc/sunrise";

$searched = true;


$jsondata = file_get_contents($uri);
$jsondata2 = file_get_contents($uri2);
$jsondata3 = file_get_contents($uri3);
$jsondata4 = file_get_contents($uri4);
$jsondata5 = file_get_contents($uri5);

$convertToAssociateArray = true;
$lightvalues = json_decode($jsondata, $convertToAssociateArray);
$weatherdata = json_decode($jsondata2, $convertToAssociateArray);
$temperature = json_decode($jsondata3, $convertToAssociateArray);
$cloudiness = json_decode($jsondata4, $convertToAssociateArray);
$sunrise = json_decode($jsondata5, $convertToAssociateArray);

//$lightvalue = $lightvalues['Lightvalue'];

//foreach($lightvalues as $element){
//    echo $element['Lightvalue'], '<br>';
//}

//print_r($lightvalues);
//var_dump($jsondata);
//print_r($weatherdata);




require_once 'vendor/autoload.php';
Twig_Autoloader::register();
$loader = new Twig_Loader_Filesystem('views');
$twig = new Twig_Environment($loader, array(
    // 'cache' => '/path/to/compilation_cache',
    'auto_reload' => true
));
$template = $twig->loadTemplate('index.html.twig');
$parametersToTwig = array("lightvalues" => $lightvalues, "weatherdata" => $weatherdata, "temperature" => $temperature, "cloudiness" => $cloudiness, "sunrise" => $sunrise);
echo $template->render($parametersToTwig);
?>


