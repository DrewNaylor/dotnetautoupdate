

## Signing and Update Settings ##

![http://dotnetautoupdate.googlecode.com/hg/web/guide/Settings.png](http://dotnetautoupdate.googlecode.com/hg/web/guide/Settings.png)

## Manifest File ##

The manifest file contains the details of the update; it is also signed with the private key for security. It is automatically generated once the update settings are filled out:

![http://dotnetautoupdate.googlecode.com/hg/web/guide/Manifest.png](http://dotnetautoupdate.googlecode.com/hg/web/guide/Manifest.png)

Saving the manifest will also save a ".signature" file in the same directory. Both need to be copied to the update web server.


## Update Settings ##

The update settings are also automatically generated:

![http://dotnetautoupdate.googlecode.com/hg/web/guide/ClientCode.png](http://dotnetautoupdate.googlecode.com/hg/web/guide/ClientCode.png)