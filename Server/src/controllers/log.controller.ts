import {NextFunction, Request, Response} from 'express';

import Log, {ILog} from '../models/log';
import Plant from '../models/plant';

const create = async (req: Request, res: Response, next: NextFunction) => {
  const newLog: ILog = new Log(req.body);

  await Plant.exists({_id: newLog.plantId}, async (err: any, exists: Boolean) => {
    if (err) {
      res.status(404).send(err);
    } else if (exists) {
      await newLog.save((err, log) => {
        if (err) {
          res.status(404).send(err);
        } else {
          res.status(201).json(log);
        }
      });
    } else {
      res.status(404).send();
    }
  });
};

export {create};
