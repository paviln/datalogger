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
#from dotenv import load_dotenv

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

base_url = "http://192.168.87.133:3000/api/"

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
    soil_humidity = read_soil_humd(0)
    idd = "60587949da056c57e4baa6ed"
    payload = {'temperature':air_temperature,'air_humidity':air_humidity, 'soil_humidity':soil_humidity, 'loggerId':idd}
    payload2 = {'minimumtemperature':'null','soilType':'null', 'logs':'', 'plants':''}
    return air_temperature, air_humidity

#Function that sends a GET Request to API to recieve a logger
def get_data(id):
    resp = requests.get(base_url+'logger/'+id)
    if resp.status_code != 200:
        # This means something went wrong.
        raise APIError('GET /logger/ {}'.format(resp.status_code))
    for logger in resp.json():
        print('{}'.format(logger['_id']))
#Function that sends a POST request to API to create a logger    
def create_logger():
    resp = requests.post(base_url+'logger/')
    if resp.status_code != 200 or 201:
        raise APIError('POST /logger/ {}'.format(resp.status_code))
    else:
        print("Logger Created")
#Function that sends a POST request to API to create a log
def post_log():
    air_temp, air_hum = read_temp_humd()
    soil_hum = read_soil_humd(0)
    print(air_temp)
    print(air_hum)
    print(soil_hum)
    payload = {'temperature':air_temp,'air_humidity':air_hum, 'soil_humidity':soil_hum, 'loggerId':idd}
    resp = requests.post(base_url'log/',json=payload)
    if resp.status_code != 200:
        raise APIError('POST /log/ {}'.format(resp.status_code))
    else:
        print("Log posted")

#def get_warning():
  # global warning
  #  warning = 

#def led_turn_on():
    
#Function that writes every DATA collected to a local file
def write_to_file():
    #send data
    file_name = datetime.now().strftime("%m.%d.%Y, %H:%M:%S") + ".txt"
    with open(file_name, 'w+') as outfile:
            json.dump(log, outfile, indent=4, cls=LogEncoder)

#this is main function
def main():
    try:
        initialize_gpio()
        read_temp_humd()
        #get_data()
        post_log()
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
