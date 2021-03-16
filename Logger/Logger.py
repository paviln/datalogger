import RPi.GPIO as GPIO
import requests
import spidev
import os
import time
import datetime
#import adafruit_am2320
#import board
#import busio
import RPi.GPIO as GPIO

#Setup I2C and AM2320
#i2c = busio.I2C(board.SCL, board.SDA)
#am2320 = adafruit_am2320.AM2320(i2c)

#Setup GPIO PINS
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)

#Global Variables
delay = 0.2
#initial_time = time.monotonic()
#SPI Setup
spi = spidev.SpiDev()
spi.open(0,0)
spi.max_speed_hz = 1000000

#Function to Read from YL69 (soil sensor) returns analogue value
def read_soil_humd(channel):
	val = spi.xfer2([1,(8+channel)<<4,0])
	data = ((val[1]&3) << 8) + val[2]
	return data

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
			print(val)
		time.sleep(delay)
    except KeyboardInterrupt:
        print("Program Terminated")
    finally:
        GPIO.cleanup()




#Runs Main Function
if __name__=="__main__":
    main()
