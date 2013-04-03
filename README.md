azureftp-for-sharepoint
=======================

Interact with a SharePoint in a local intranet via the Service Bus.

The goal of this project is to create a way of interacting with a local SharePoint 2013 instance protected by a firewall, via a set of services exposed in the Azure Service Bus, using an ftp-like interface.

There will be a command line tool to run on the client to run the common ftp commands (open, get, cd, list, put, close). On the server side there will be a Windows Service to interact with a SharePoint 2013 instance located on the local network, using either ClientOM or REST.

The developments will support authentication and secure transfer of contents. Authentication will be done directly against SharePoint's permission model.

The base code structure and architecture document will be posted in the next few days.[2013.04.03]
