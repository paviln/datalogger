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

api_url_base='http://localhost:3000/api/logger'
headers = {'Content-Type': 'application/json'}

#api_url = '{0}account'.format(api_url_base)
response = requests.get(api_url_base, headers=headers)
def get_data():
    response = requests.get(api_url_base, headers=headers)
    if response.status_code == 200:
        return json.loads(response.content.decode('utf-8'))
    else:
        return None

def main():
    get_data()




