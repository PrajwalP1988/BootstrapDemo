 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<title>Sendemail Script</title>
</head>
<body>
 <?php

 
 if ($_POST['submit'])
 {
  $name = $_POST["author"];  
  $email = $_POST["email"]; 
  $subject = "Test Email";
  $contactNo=$_POST["ContactNo"];
  $message = $_POST["text"];
  
 $enquiry = "This Email is from \n
  name: $name \n 
  email address : $email \n
  subject : $subject \n
  message : $text.\r\n-----------\r\n "; 
  $enquiry = $enquiry . $_POST["enquiry"]; 
  $enquiry = wordwrap($enquiry, 70); 
  	if( mail('bhanujdy@gmail.com', $subject, $enquiry)!==true)
    {
        die('Fail to send');
	}
    die('Sucess');

   
    //mail('nuthan2008@gmail.com', $subject, $enquiry);
	//first argument = your email address; second argument = subject of email (make it very catchy so you won't miss it); third argument = the content (DO NOT CHANGE THIS!!)

print '<script type="text/javascript">'; 
print 'alert("Your message has been sent, and We will get back to you very soon. Thank you for your request.")'; 
print '</script>'; 


 }
 ?>
 <br /><br />
 Your message has been sent, and We will get back to you very soon. Thank you for your request.
<meta HTTP-EQUIV="REFRESH" content="5; url=./index.html">
</p> 

</body>
</html>