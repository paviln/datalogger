import RPi.GPIO as GPIO
import json
from json import JSONEncoder
import requests
import spidev
import os
import time
import datetime
import adafruit_am2320
import board
import busio
import enum
import http.client
import urllib
import sys
import csv
import ApiCalls
from dotenv import load_dotenv
from pathlib import Path
#Setup I2C and AM2320
i2c = busio.I2C(board.SCL, board.SDA)
am2320 = adafruit_am2320.AM2320(i2c)

#GPIO PIN Assigns
GREEN = 20
RED = 21
YELLOW = 16

#Global Variables
delay = 1
max_hum = 650.0 #Maximum value of Humidity, sensor calibrated. 
initial_time = time.monotonic()
warning = ""

#SPI Module Setup
spi = spidev.SpiDev() #New Object
spi.open(0,0)
spi.max_speed_hz = 1000000
    
def initialize_gpio():
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(GREEN,GPIO.OUT,initial=GPIO.LOW) #RED
    GPIO.setup(RED,GPIO.OUT,initial=GPIO.LOW) #GREEN
    GPIO.setup(YELLOW,GPIO.OUT,initial=GPIO.LOW) #YELLOW
    GPIO.setwarnings(False)
    
#Function to Read from YL69 (soil sensor) returns percentage humidity
def read_soil_humd(channel):
    val = spi.xfer2([1,(8+channel)<<4,0])
    if(0 <=val [1] <=3):
        data = 1023 - ((val[1] * 256) + val[2])
        percentage = ((data / max_hum) * 100)
        time.sleep(delay)
    return percentage

#Function to read air temperature and air humidity returns both values
def read_temp_humd():
    air_temperature = am2320.temperature
    air_humidity = am2320.relative_humidity
    now = datetime.datetime.now()
    print("Date and Time:",now.strftime("%Y-%m-%d %H:%M:%S"))
    print("Temperature:", air_temperature)
    print("Humidity:", air_humidity)
    return air_temperature, air_humidity

#Function that sends a GET Request to API to recieve a logger
def get_data(_idd):
    resp = ApiCalls.get_logger(_idd)
    httpcode = resp.status_code
    if httpcode != 200:
        # This means something went wrong.
        raise APIError('GET /logger/ {}'.format(httpcode))
    for logger in resp.json():
        print('{}'.format(logger['_id']))

#Function that sends a POST request to API to create a logger    
def create_logger():
    payload = {'minimumTemperature':'','soilType':'','logs':'','plants':''}
    resp = ApiCalls.post_logger(0,0,0,0)
    httpcode = resp.status_code
    if httpcode!= 200:
        raise APIError('POST /logger/ {}'.format(httpcode))
    else:
        print("Logger Created:"+ str(httpcode))

#Function that sends a POST request to API to create a log
def post_log():
    air_temp, air_hum = read_temp_humd()
    soil_hum = read_soil_humd(0)
    log_id = '60586f33b5943a427c675537'
    #print(soil_hum)
    resp = ApiCalls.post_log(air_temp,air_hum,soil_hum,log_id)
    #payload = {'temperature':air_temp,'air_humidity':air_hum, 'soil_humidity':soil_hum, 'loggerId':idd}
    #resp = requests.post(_url('log/'),json=payload)
    httpcode = resp.status_code
    if httpcode != 200:
        raise APIError('POST /log/ {}'.format(httpcode))
    else:
        print("Log posted: "+ str(resp.status_code))

#def _url(path):
 #   return 'http://192.168.87.133:3000/api/'+path

#this is main function
def main():
    try:
        initialize_gpio()
        #read_temp_humd()
        #get_data("60587949da056c57e4baa6ed")
        post_log()
        create_logger()
        
        while True:
            val = read_soil_humd(0)
            #read_temp_humd()
            if(val!=0):
                if(val > 50):
                    GPIO.output(GREEN,1)
                    GPIO.output(RED,0)
                    print("Soil is not dry %.2f" % val+"%")
                if(val < 50):
                    GPIO.output(RED,1)
                    GPIO.output(GREEN,0)
                    print("Soil is dry %.2f" % val +"%")
                time.sleep(delay)
    except KeyboardInterrupt:
        print("Program Terminated")
    finally:
        GPIO.cleanup()
        spi.close()

#Class for ENUM status'
class Status(enum.Enum):
   WAITING = 1
   LOGGING = 2
   STOPPED = 3
   DRY = 4
   WET = 5

#Class for APIError (Exception Handling)
class APIError(Exception):
    """An API Error Exception"""

    def __init__(self, status):
        self.status = status

    def __str__(self):
        return "APIError: status={}".format(self.status)


#Invokes Main Method
if __name__=="__main__":
    main()
