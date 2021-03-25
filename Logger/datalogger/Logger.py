##-----------------------------imports START-----------------------------------------##
import RPi.GPIO as GPIO
import json
import requests
import spidev
import os
import time
from datetime import datetime
import adafruit_am2320
import board
import busio
import enum
import sys
import ApiCalls
import asyncio
import schedule
##-----------------------------imports END-----------------------------------------##
loop = asyncio.get_event_loop()
#Setup I2C and AM2320
i2c = busio.I2C(board.SCL, board.SDA)
am2320 = adafruit_am2320.AM2320(i2c)

#GPIO PIN Assigns
GREEN = 21
RED = 20

#Global Variables
delay = 1
max_hum = 520.0 #Maximum value of Humidity, sensor calibrated. 
minimumtemp = 0
plant_id = ""
logger_id = ""

#SPI Module Setup
spi = spidev.SpiDev() #New Object
spi.open(0,0)
spi.max_speed_hz = 1000000

#Function to initialize gpio
def initialize_gpio():
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(GREEN,GPIO.OUT,initial=GPIO.LOW)
    GPIO.setup(RED,GPIO.OUT,initial=GPIO.LOW) 
    GPIO.setwarnings(False)

#Function that sends a GET Request to API to recieve a logger to check whether the logger exists so the
#Program should not post/create a new logger. To be implemented. 
async def get_data(_idd):
    exists_logger_id = ""
    resp = await ApiCalls.get_logger(_idd)
    httpcode = resp.status_code
    if httpcode != 200:
        raise APIError('GET /logger/{}'.format(httpcode))
    else:
        print("Logger exists: "+ str(httpcode))
        rawtext = json.dumps(resp.json(),sort_keys=True,indent=4)
        jprint(resp.json())
        json_logger_object = json.loads(rawtext)
        exists_logger_id = json_logger_object["_id"]
    return exists_logger_id

#Function to Read from YL38/69 (soil sensor) returns percentage humidity
async def read_soil_humd(channel):
    val = spi.xfer2([1,(8+channel)<<4,0])
    if(0 <=val [1] <=3):
        data = 1023 - ((val[1] * 256) + val[2])
        percentage = ((data / max_hum) * 100)
        time.sleep(delay)
    return percentage

#Function to read air temperature and air humidity returns both values
async def read_temp_humd():
    air_temperature = am2320.temperature
    air_humidity = am2320.relative_humidity
    #now = datetime.datetime.now()
    #print("Date and Time:",now.strftime("%Y-%m-%d %H:%M:%S"))
    print("Temperature:", air_temperature)
    print("Humidity:", air_humidity)
    time.sleep(delay)
    return air_temperature, air_humidity

#Function that sends a POST request to API to create a logger  
async def post_logger():
    global logger_id
    resp = await ApiCalls.post_logger()
    httpcode = resp.status_code
    if httpcode!= 201:
        raise APIError('POST /logger/ {}'.format(httpcode))
    else:
        print("Logger Created:"+ str(httpcode))
        rawtext = json.dumps(resp.json(),sort_keys=True,indent=4)
        json_logger_object = json.loads(rawtext)
        logger_id = json_logger_object["_id"]
        print("Logger posted and ID recieved: "+ logger_id +' with status code: '+ str(resp.status_code))
    return logger_id

#Function that sends a POST request to API to create a log
async def post_log():
    air_temp, air_hum = await read_temp_humd()
    soil_hum = await read_soil_humd(0)
    #plant_id = "605c5d1201df280824ecb377"
    plant_id = await get_plant_id()
    resp = await ApiCalls.post_log(air_temp,air_hum,soil_hum,plant_id)
    httpcode = resp.status_code
    if httpcode != 201:
        raise APIError('POST /log/ {}'.format(httpcode))
    else:
        print("Log posted: "+ str(resp.status_code))
        jprint(resp.json())
    
#Function thats send a GET request to API to GET a plant_ID by LoggerID    
async def get_plant_id():
    global plant_id
    global logger_id
    #TODO Implement a verification to check whether the logger_id exists
    #if it exsists, dont create new logger and use old logger_id instead
    resp = await ApiCalls.get_plantby_loggerid(logger_id)
    httpcode = resp.status_code
    if httpcode != 200:
        raise APIError('GET /logger/{}'.format(httpcode))
    else:
        rawtext = json.dumps(resp.json(),sort_keys=True,indent=4)
        json_plant_object = json.loads(rawtext)
        plant_id = json_plant_object["_id"]
        print("Plant ID recieved: "+ str(resp.status_code))
    return plant_id

#TODO Implement warning flag - Read the minimumtemperature for the specific plant
#and indicate a warning with an yellow -> red led
def raise_warning():
    GPIO.output(RED,1)
    print("warning temperature is lower than minimum temperature")

#Function for sorting/printing JSON objects
def jprint(obj):
    text = json.dumps(obj,sort_keys=True,indent=4)
    print(text)

#This is main function
async def main():
    try:
        initialize_gpio()
        #schedule.every().hour.do(post_log) The Scheduler assigns that "post_logger" should be done every hour.
        while True:
            newinput = input("Enter to Start program")
            await post_logger()
            inputdd = input("enter to continue.")
            val = await read_soil_humd(0)
            schedule.run_pending() #Runs the scheduled jobs.
            await post_log()
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

#Class for APIError (Exception Handling)
class APIError(Exception):
    """An API Error Exception"""

    def __init__(self, status):
        self.status = status

    def __str__(self):
        return "APIError: status={}".format(self.status)

#Class for ENUM status
class Status(enum.Enum):
   WAITING = 1
   LOGGING = 2
   STOPPED = 3
   DRY = 4
   WET = 5

#Starts the Program and invokes main function
asyncio.ensure_future(main())
loop.run_forever()