import requests
import os
from dotenv import load_dotenv
from pathlib import Path

env_path = Path('.') / 'config/connectiondata.env'
load_dotenv(dotenv_path=env_path)

SERVER_IP = os.getenv("SERVER_IP")
SERVER_PORT = os.getenv("SERVER_PORT")

def get_logger(logger_id):
    return requests.get(_url('/logger/{}'.format(logger_id)))

def post_logger(minimumtemp,soiltype,logs,plants):
    return requests.post(_url('/logger/'), json={
        'minimumtemperature': minimumtemp,
        'soilType': soiltype,
        'logs':logs,
        'plants':plants
        })

def post_log(air_temp,air_humd,soil_hum,log_id):
    return requests.post(_url('/log/'), json={
        'air_temperature': air_temp,
        'air_humidity': air_humd,
        'soil_humidity':soil_hum,
        'loggerId':log_id
        })

def _url(path):
    return 'http://'+SERVER_IP+':'+SERVER_PORT+'/api'+path