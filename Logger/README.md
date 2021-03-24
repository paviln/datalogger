#DATALOGGER
---------------------------------------------

--Pi Software Configuration:
-----------------------------------------
Update your Raspberry Pi with cmd: 

sudo apt update

sudo apt upgrade -y
--------------------------------------------

Install pyenv to manage Python versions and download/install python 3.7
Follow the instruction on the following link:
https://realpython.com/intro-to-pyenv/ 
----------------------------------------------

To make YL69/38 work, first enable SPI Interface on the raspberry pi. 
Furthermore, you must install the spidev Library on the pi
use following commands: 

sudo apt-get install git python-dev

git clone git://github.com/doceme/py-spidev

cd py-spidev/

sudo python setup.py install or sudo pip3 install spidev
----------------------------------------

Install the dotenv module for enviroment handling

sudo pip3 install python-dotenv
------------------------------------------------

install apscheduler module for job scheduling

sudo pip3 install apscheduler

-----------------------------------------
Change the SERVER_IP in connectiondata.env file in the config folder
to your PCs IP Address.

Change SERVER_PORT to your servers PORT. 

