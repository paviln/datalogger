import {NextFunction, Request, Response} from 'express';

import Plant, {IPlant} from '../models/plant';
import Logger from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction) => {
  const newPlant: IPlant = new Plant(req.body);

  const doesLoggerExsist: Boolean = await Logger.exists({_id: newPlant.loggerId});

  if (doesLoggerExsist) {
    newPlant.save((err: any, log: any) => {
      if (err) {
        res.send(err);
      }
      res.status(201).json(log);
    });
  } else {
    res.status(404).send();
  }
};

export {create};
