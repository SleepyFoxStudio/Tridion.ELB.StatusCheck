# Tridion.ELB.StatusCheck

## A page for use by load balancers to check all tridion services are running
Point your load balancer test page at the URL of this page and then when any Tridion service is stopped or not running it will take it out of the load balancer until the service is running again.


## Getting started

* Clone this repo
* Open in visual studio 
* Click _Build/Publish_ and publish to local file system
* Take the published contents and copy them to a folder on each CME server
* In IIS right click the Tridion CME site and select _Add Application_
* Give it a meaningful name such as AwsPing and select the folder where you copied the published contents in the previous step
* Open https://{{CMEURL}}/AwsPing/tridionstatus.ashx
* You should see a page returned with the machine name and a list of all Tridion services.
  * If all the services set to automatic are running then the page returns a 200
  * If any of the services that are set to automatic are not running the page returns a 500
* Use this page in your Load Balancer configurtation in order to mark this node us unhealhty and diver load to other CME servers.

