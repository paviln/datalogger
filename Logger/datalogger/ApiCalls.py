import requests
import os
from dotenv import load_dotenv
from pathlib import Path

#dotenv start
env_path = Path('.') / 'config/connectiondata.env'
load_dotenv(dotenv_path=env_path)

SERVER_IP = os.getenv("SERVER_IP")
SERVER_PORT = os.getenv("SERVER_PORT")
#dotenv end

#Function to send a GET Request to API
def get_logger(logger_id):
    return requests.get(_url('/logger/{}'.format(logger_id)))

#Function to send a POST request to API
def post_logger():
    return requests.post(_url('/logger/'), json={
        
        })

#Function to send a POST request to API
def post_log(air_temp,air_humd,soil_hum,log_id):
    return requests.post(_url('/log/'), json={
        'temperature': air_temp,
        'air_humidity': air_humd,
        'soil_humidity':soil_hum,
        'loggerId':log_id
        })

#Function to get SERVER and PORT from .env file and create the URL for The API(Server)
def _url(path):
    return 'http://'+SERVER_IP+':'+SERVER_PORT+'/api'+path