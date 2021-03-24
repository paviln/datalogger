#DATALOGGER

--Pi Software Configuration:
Update your Raspberry Pi with cmd: 

sudo apt update

sudo apt upgrade -y

Install python3.9
run this cmd:
sudo apt-get install build-essential tk-dev libncurses5-dev libncursesw5-dev libreadline6-dev libdb5.3-dev libgdbm-dev libsqlite3-dev libssl-dev libbz2-dev libexpat1-dev liblzma-dev zlib1g-dev python3 python3-dev python3-venv python3-pip libffi-dev libtiff-dev autoconf libopenjp2-7 -y

version=3.9.1
wget -O /tmp/Python-$version.tar.xz https://www.python.org/ftp/python/$version/Python-$version.tar.xz

cd /tmp

tar xf Python-$version.tar.xz

cd Python-$version

./configure --enable-optimizations

sudo make altinstall

sudo apt -y autoremove

To make YL69 work, first enable SPI Interface on the raspberry pi. 
Furthermore, you must install the spidev Library on the pi
use following commands: 

sudo apt-get install git python-dev

git clone git://github.com/doceme/py-spidev

cd py-spidev/

sudo python setup.py install

Install the dotenv module for enviroment handling

pip install python-dotenv

install apscheduler module for job scheduling

pip install apscheduler
