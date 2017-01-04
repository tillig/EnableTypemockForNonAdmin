# EnableTypemockForNonAdmin
Command-line utility for enabling Typemock Isolator to be used by someone who is not logged in as a local machine administrator.

Note that, since Typemock Isolator 6.0.3, Isolator was updated to allow this by default. However, for those using older versions or who find they need help still...

[Originally released on my blog here.](http://www.paraesthesia.com/archive/2010/05/04/enable-typemock-isolator-for-a-non-admin-user.aspx/)

Open a command prompt as an administrator and run the program, passing in the name of the non-admin user you want to have access to Typemock Isolator.

`EnableTypemockForNonAdmin.exe YOURDOMAIN\yourusername`