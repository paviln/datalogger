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
#from dotenv import load_dotenv

#Setup I2C and AM2320
i2c = busio.I2C(board.SCL, board.SDA)
am2320 = adafruit_am2320.AM2320(i2c)
#GPIO PIN Assigns
GREEN = 20
RED = 21

#Global Variables
delay = 1
max_hum = 650.0 #Maximum value of Humidity, sensor calibrated. 
initial_time = time.monotonic()
warning = ""

#SPI Module Setup
spi = spidev.SpiDev() #New Object
spi.open(0,0)
spi.max_speed_hz = 1000000

#def callback(channel):
    #read_soil_humd(0)
    
    
def initialize_gpio():
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(GREEN,GPIO.OUT,initial=GPIO.LOW) #RED
    GPIO.setup(RED,GPIO.OUT,initial=GPIO.LOW) #GREEN
    GPIO.setwarnings(False)
    
#Function to Read from YL69 (soil sensor) returns percantage humidity
def read_soil_humd(channel):
    val = spi.xfer2([1,(8+channel)<<4,0])
    if(0 <=val [1] <=3):
        data = 1023 - ((val[1] * 256) + val[2])
        percentage = ((data / max_hum) * 100)
        time.sleep(delay)
    return percentage

def read_temp_humd():
    temperature = am2320.temperature
    humidity = am2320.relative_humidity
    now = datetime.datetime.now()
    print("Date and Time:",now.strftime("%Y-%m-%d %H:%M:%S"))
    print("Temperature:", temperature)
    print("Humidity:", humidity)
    payload = {"'temperature'":temperature,"'air_humidity'":humidity}
    if(temperature and humidity != 0):
        #r = requests.post('localhost:3000/logger', params = payload)
        #r = requests.get('http://127.0.0.1:3000')
        #print(r.text)
        #print(r.status_code)
        print(payload)
    else:
        print("Data empty")
    time.sleep(delay)
    return temperature, humidity

#def get_warning():
  # global warning
  #  warning = 

#def led_turn_on():
    

def write_to_file():
    #send data
    file_name = datetime.now().strftime("%m.%d.%Y, %H:%M:%S") + ".txt"
    with open(file_name, 'w+') as outfile:
            json.dump(log, outfile, indent=4, cls=LogEncoder)



#this is main function
def main():
    try:
        initialize_gpio()
        while True:
            val = read_soil_humd(0)
            read_temp_humd()
            #r = requests.get('https://api.github.com/events')
            #print(r.url)
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

#GPIO.add_event_detect(channel, GPIO.BOTH, bouncetime=300)
#GPIO.add_event_callback(channel, callback)


#Class for ENUM status'
class Status(enum.Enum):
   WAITING = 1
   LOGGING = 2
   STOPPED = 3
   DRY = 4
   WET = 5




#Invokes Main Method
if __name__=="__main__":
    main()
