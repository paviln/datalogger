#----------import start --------------#
import requests
import os
from dotenv import load_dotenv
from pathlib import Path
import asyncio
#-----------import end ---------------#
#---------dotenv start------------------#
env_path = Path('.') / 'config/server.env'
load_dotenv(dotenv_path=env_path)

SERVER_IP = os.getenv("SERVER_IP")
SERVER_PORT = os.getenv("SERVER_PORT")
#-----------dotenv end-------------------#

#Function to send a GET Request to API
async def get_plantby_loggerid(logger_id):
    return requests.get(_url('/logger/active/{}'.format(logger_id)))

#Function to send a GET Request to API
async def get_logger(logger_id):
    return requests.get(_url('/logger/{}'.format(logger_id)))

#Function to send a POST request to API
async def post_logger():
    return requests.post(_url('/logger/'), json={})

#Function to send a POST request to API
async def post_log(air_temp,air_humd,soil_hum,plant_id):
    return requests.post(_url('/log/'), json={
        'temperature': air_temp,
        'air_humidity': air_humd,
        'soil_humidity':soil_hum,
        'plantId':plant_id
        })

#Function to get SERVER and PORT from .env file and create the URL for The API(Server)
def _url(path):
    return 'http://'+SERVER_IP+':'+SERVER_PORT+'/api'+path