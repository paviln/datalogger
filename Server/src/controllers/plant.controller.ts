import {NextFunction, Request, Response} from 'express';

import Plant, {IPlant} from '../models/plant';
import Logger from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction) => {
  let doesLoggerExsist: Boolean = false;
  try {
    doesLoggerExsist = await Logger.exists({_id: req.body.loggerId});
  } catch (error) {
    console.log(error);
  }
  if (doesLoggerExsist) {
    var plant: any = 
    {
      name: req.body.name,
      img: {
        data: req.file.buffer,
        contentType: 'image/png'
      },
      loggerId: req.body.loggerId,
    } 
    console.log(plant)
    Plant.create(plant, (err: any, plant: any) => {
      if (err) {
        res.send(err);
      }
      res.status(201).json(plant);
    });
  } else {
    res.status(404).send();
  }
};

export {create};
