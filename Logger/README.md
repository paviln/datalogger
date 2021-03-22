#DATALOGGER

To make YL69 work, first enable SPI Interface on the raspberry pi. 
Furthermore, you must install the spidev Library on the pi
use following commands: 

sudo apt-get install git python-dev
git clone git://github.com/doceme/py-spidev
cd py-spidev/
sudo python setup.py install

Install dotenv module

sudo install python3-dotenv