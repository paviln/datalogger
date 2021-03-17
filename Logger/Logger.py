import RPi.GPIO as GPIO
import requests
import spidev
import os
import time
import datetime
#import adafruit_am2320
#import board
#import busio

#Setup I2C and AM2320
#i2c = busio.I2C(board.SCL, board.SDA)
#am2320 = adafruit_am2320.AM2320(i2c)
#GPIO Assigns
GREEN = 20
RED = 21
#Setup GPIO PINS
GPIO.setmode(GPIO.BCM)
GPIO.setup(GREEN,GPIO.OUT,initial=GPIO.LOW) #RED
GPIO.setup(RED,GPIO.OUT,initial=GPIO.LOW) #GREEN
GPIO.setwarnings(False)

#Global Variables
delay = 1
max_hum = 450.0 #Maximum value of Humidity, sensor calibrated. 

#initial_time = time.monotonic()
#SPI Module Setup
spi = spidev.SpiDev() #New Object
spi.open(0,0)
spi.max_speed_hz = 1000000

#Function to Read from YL69 (soil sensor) returns percantage humidity
def read_soil_humd(channel):
    val = spi.xfer2([1,(8+channel)<<4,0])
    if(0 <=val [1] <=3):
        data = 1023 - ((val[1] * 256) + val[2])
        percentage = ((data / max_hum) * 100)
    return percentage

#def read_temp_humd():
    #temperature = am2320.temperature
    #humidity = am2320.relative_humidity
    #current_time = time.monotonic()

#this is main function
def main():
    try:
        while True:
            val = read_soil_humd(0)
            if(val!=0):
               # print("Soil Moisture:", val, "%")
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




#Runs Main Function
if __name__=="__main__":
    main()
