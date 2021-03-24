import RPi.GPIO as GPIO
import json
from json import JSONEncoder
import requests
import spidev
import os
import time
from datetime import datetime
import adafruit_am2320
import board
import busio
import enum
import urllib
import sys
import ApiCalls
from pytz import timezone
from apscheduler.schedulers.background import BackgroundScheduler
from apscheduler.schedulers.blocking import BlockingScheduler
#Setup I2C and AM2320
i2c = busio.I2C(board.SCL, board.SDA)
am2320 = adafruit_am2320.AM2320(i2c)
#apscheduler setup
sched = BlockingScheduler()
scheduler = BackgroundScheduler()
cet = datetime.now(timezone('Europe/Amsterdam'))
#GPIO PIN Assigns
GREEN = 20
RED = 21
YELLOW = 16

#Global Variables
delay = 1
max_hum = 650.0 #Maximum value of Humidity, sensor calibrated. 
initial_time = time.monotonic()
globaltime = 0
minimumtemp = 0
plantid = ""
loggerid = ""

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
        raise APIError('GET /logger/{}'.format(httpcode))
    else:
        jprint(resp.json())

#Function that sends a POST request to API to create a logger  
def post_logger():
    global loggerid
    resp = ApiCalls.post_logger()
    httpcode = resp.status_code
    if httpcode!= 201:
        raise APIError('POST /logger/ {}'.format(httpcode))
    else:
        print("Logger Created:"+ str(httpcode))
        rawtext = json.dumps(resp.json(),sort_keys=True,indent=4)
        json_plant_object = json.loads(rawtext)
        loggerid = json_plant_object["_id"]
        print("Logger posted and ID recieved: "+ str(resp.status_code))

#Function that sends a POST request to API to create a log
def post_log():
    air_temp, air_hum = read_temp_humd()
    soil_hum = read_soil_humd(0)
    plant_id = plantid
    print(plant_id)
    resp = ApiCalls.post_log(air_temp,air_hum,soil_hum,plant_id)
    httpcode = resp.status_code
    if httpcode != 201:
        raise APIError('POST /log/ {}'.format(httpcode))
    else:
        print("Log posted: "+ str(resp.status_code))
        jprint(resp.json())
    
def get_plantid_by_loggerid(_loggerid):
    global plantid
    resp = ApiCalls.get_plantby_loggerid(_loggerid)
    httpcode = resp.status_code
    if httpcode != 200:
        raise APIError('GET /logger/{}'.format(httpcode))
    else:
        rawtext = json.dumps(resp.json(),sort_keys=True,indent=4)
        json_plant_object = json.loads(rawtext)
        plantid = json_plant_object["_id"]
        print("Plant ID recieved: "+ str(resp.status_code))

@sched.scheduled_job('interval', seconds=10)
def testprint():
    print("Im executed every 10th second")

#Function for sorting/printing JSON objects
def jprint(obj):
    text = json.dumps(obj,sort_keys=True,indent=4)
    print(text)

#This is main function
def main():
    try:
        initialize_gpio()
        testprint()
        while True:
            
            val = read_soil_humd(0)
            #read_temp_humd()
            #post_logger()
            #get_plantid_by_loggerid(loggerid)
            #post_log()
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
        #GPIO.cleanup()
        spi.close()

#Class for ENUM status
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
